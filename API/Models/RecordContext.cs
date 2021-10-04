using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class RecordContext : DbContext
    {
        public RecordContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Record> Records { get; set; }
    }
}
