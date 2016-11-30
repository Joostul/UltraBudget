using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltraBudget.Entities
{
    public class Currency
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ExchangeRate ExchangeRate { get; set; }

        public bool MainCurrency { get; set; }
    }
}
