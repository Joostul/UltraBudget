using System.Collections.Generic;
using UltraBudget.Entities;

namespace UltraBudget.ViewModels
{
    public class HomePageViewModel
    {
        public string Greeting { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Wallet> Wallets { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
