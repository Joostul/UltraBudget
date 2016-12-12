using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
