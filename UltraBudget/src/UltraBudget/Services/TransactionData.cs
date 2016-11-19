using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraBudget.Models;

namespace UltraBudget.Services
{
    public interface ITransactionData
    {
        IEnumerable<Transaction> GetAll();
    }

    public class InMemoryTransactionData : ITransactionData
    {
        public InMemoryTransactionData()
        {
            _transactions = new List<Transaction>()
            {
                new Transaction
                {
                    Id = 1,
                    Amount = 1,
                    Date = DateTime.Now
                },
                new Transaction
                {
                    Id = 2,
                    Amount = 1,
                    Date = DateTime.Now
                },
                new Transaction
                {
                    Id = 3,
                    Amount = 2,
                    Date = DateTime.Now
                }

            };
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _transactions;
        }

        List<Transaction> _transactions;
    }
}
