using Business.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRecordService
    {
        Task<List<DTORecord>> GetRecords();
        Task<List<DTORecord>> UpdateRecords(List<DTORecord> records);
    }
}
