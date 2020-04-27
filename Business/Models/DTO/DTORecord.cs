using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models.DTO
{
    public class DTORecord
    {
        public int RecordId { get; set; }
        public string Text { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int? IndexNumber { get; set; }
    }
}
