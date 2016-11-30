using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltraBudget.Entities
{
    public class Wallet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }
    }
}
