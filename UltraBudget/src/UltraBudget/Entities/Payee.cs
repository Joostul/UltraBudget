﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltraBudget.Entities
{
    public class Payee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}