using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UltraBudget.Entities
{
    public class UltraBudgetDbContext: IdentityDbContext<User>
    {
        public UltraBudgetDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
