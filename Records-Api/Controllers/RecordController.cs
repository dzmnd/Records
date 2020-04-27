using Business.Interfaces;
using Business.Models.Db;
using Business.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Records_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IHostedService _hostedService;
        private readonly IRecordService _recordService;

        public RecordController(IHostedService hostedService, IRecordService recordService)
        {
            _hostedService = hostedService;
            _recordService = recordService;
        }

        public void Get()
        {
            _hostedService.StartAutomaticUpdateRecords();
        }

        [Route("GetRecords")]
        public async Task<List<DTORecord>> GetRecords()
        {
            return await _recordService.GetRecords();
        }

        [Route("UpdateRecords")]
        [HttpPost]
        public async Task<List<DTORecord>> UpdateRecords(List<DTORecord> records)
        {
            return await _recordService.UpdateRecords(records);
        }
    }
}
