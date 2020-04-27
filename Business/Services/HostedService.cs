using Business.Context;
using Business.Interfaces;
using Business.Models.Db;
using Business.Models.DTO;
using Business.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Records_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Records_Api.Services
{
    public class HostedService : IHostedService
    {
        private readonly IHubContext<ModifyRecordHub> _hub;
        private readonly IConfiguration _configuration;
        private const int MILLISECOND_IN_SECOND = 1000;
        private const int SECOND_IN_MINUTE = 60;
        private const int MINUTE = 1;

        public HostedService(IHubContext<ModifyRecordHub> hub, IConfiguration configuration)
        {
            _hub = hub;
            _configuration = configuration;
        }

        public void StartAutomaticUpdateRecords()
        {
            TimerCallback tc = new TimerCallback(AutomaticUpdateRecords);
            Timer timer = new Timer(tc, null, 0, MINUTE * SECOND_IN_MINUTE * MILLISECOND_IN_SECOND);
        }

        public void AutomaticUpdateRecords(object o)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlite(_configuration["ConnectionString_RecordDb"]);

            RecordContext newRecordContext = new RecordContext(optionsBuilder.Options);
            newRecordContext.Database.EnsureCreated();

            var index = 1;

            var dbRecords = newRecordContext.Records.Where(r => r.Status == Status.InPending).ToList();
            foreach (var record in dbRecords)
            {
                record.Status = Status.InWork;
                var textArr = record.Text.Split(' '); 
                Array.Reverse(textArr);
                record.Text = string.Join(" ", textArr);
                record.IndexNumber = index++;
            }
            newRecordContext.SaveChanges();

            if (dbRecords.Count() > 0)
            {
                Task sendUpdateRecords = _hub.Clients.All.SendAsync("transferdata", GetRecords(newRecordContext));
            }
        }

        private List<DTORecord> GetRecords(RecordContext newRecordContext)
        {
            return newRecordContext.Records.Include(c => c.Type).Select(r =>
                                                                            new DTORecord
                                                                            {
                                                                                RecordId = r.Id,
                                                                                IndexNumber = r.IndexNumber,
                                                                                Status = r.Status.ToString(),
                                                                                Text = r.Text,
                                                                                Type = r.Type.Name,
                                                                                TypeId = r.Type.Id
                                                                            }
                                                                        ).ToList();
        }
    }
}
