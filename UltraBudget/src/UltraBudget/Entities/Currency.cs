using System.Collections.Generic;

namespace UltraBudget.Entities
{
    public class Currency
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ExchangeRate> ExchangeRate { get; set; }

        public bool MainCurrency { get; set; }
    }
}
