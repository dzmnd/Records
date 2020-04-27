using Business.Models.Enums;

namespace Business.Models.Db
{
    public class Record
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Type Type { get; set; }
        public Status Status { get; set; }
        public int? IndexNumber { get; set; }
    }
}
