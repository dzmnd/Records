using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models.Db
{
    public class Type
    {
        [Key]
        [ForeignKey("Record")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
