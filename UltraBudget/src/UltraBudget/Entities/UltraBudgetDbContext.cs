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

        public DbSet<Greeting> Greetings { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Currency> Currencies { get; set; }
    }
}
