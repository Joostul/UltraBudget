using System.Collections.Generic;
using UltraBudget.Entities;

namespace UltraBudget.ViewModels
{
    public class HomePageViewModel
    {
        // Needs to be set to something else later
        public string Greeting { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Wallet> Wallets { get; set; }
    }
}
