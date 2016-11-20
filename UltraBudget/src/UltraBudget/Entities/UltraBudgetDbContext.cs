using Microsoft.EntityFrameworkCore;

namespace UltraBudget.Entities
{
    public class UltraBudgetDbContext: DbContext
    {
        public UltraBudgetDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
