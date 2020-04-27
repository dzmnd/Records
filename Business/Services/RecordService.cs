using Business.Context;
using Business.Interfaces;
using Business.Models.DTO;
using Business.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class RecordService : IRecordService
    {
        private readonly RecordContext _recordContext;
        public RecordService(RecordContext recordContext)
        {
            _recordContext = recordContext;
        }

        public async Task<List<DTORecord>> GetRecords()
        {
            return _recordContext.Records.Include(c => c.Type).Select(r => 
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

        public async Task<List<DTORecord>> UpdateRecords(List<DTORecord> records)
        {
            foreach (var record in records)
            {
                var dbRecord = _recordContext.Records.Where(r => r.Id == record.RecordId).FirstOrDefault();
                dbRecord.Status = Status.InPending;
                _recordContext.SaveChanges();
            }

            return await GetRecords();
        }
    }
}
