using System;

namespace UltraBudget.Entities
{
    public class ExchangeRate
    {
        public int Id { get; set; }

        public Currency Currency { get; set; }

        public DateTime Date { get; set; }

        public double BitcoinExchangeRate { get; set; }
    }
}
