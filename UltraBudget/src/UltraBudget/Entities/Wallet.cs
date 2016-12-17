using System.Collections.Generic;

namespace UltraBudget.Entities
{
    public class Wallet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }

        public string UserId { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
