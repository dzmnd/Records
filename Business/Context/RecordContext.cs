using Business.Models.Db;
using Business.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Type = Business.Models.Db.Type;

namespace Business.Context
{
    public class RecordContext : DbContext
    {
        public RecordContext(DbContextOptions options) : base(options) 
        {
            Database.OpenConnection();
            //Database.EnsureCreated();
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<Type> Types { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Type>().HasData(
                new Type { Id = 1, Name = "First Type" },
                new Type { Id = 2, Name = "Second Type" },
                new Type { Id = 3, Name = "Third Type" },
                new Type { Id = 4, Name = "Fourth Type" },
                new Type { Id = 5, Name = "Fifth Type" });

            modelBuilder.Entity<Record>().HasData(
                new { Id = 1, Status = Status.Done, IndexNumber = 0, Text = "One Record", TypeId = 1 },
                new { Id = 2, Status = Status.Done, IndexNumber = 0, Text = "Two Record", TypeId = 2 },
                new { Id = 3, Status = Status.Done, IndexNumber = 0, Text = "Three Record", TypeId = 3 },
                new { Id = 4, Status = Status.Done, IndexNumber = 0, Text = "Four Record", TypeId = 4 },
                new { Id = 5, Status = Status.Done, IndexNumber = 0, Text = "Five Record", TypeId = 5 });
        }
    }
}
