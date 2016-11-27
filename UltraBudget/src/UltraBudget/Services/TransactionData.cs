using System;
using System.Collections.Generic;
using System.Linq;
using UltraBudget.Entities;

namespace UltraBudget.Services
{
    public interface ITransactionData
    {
        IEnumerable<Transaction> GetAll();
        Transaction Get(int id);
        Transaction Add(Transaction newTransaction);
        void Commit();
    }

    public class SqlTransactionData: ITransactionData
    {
        private UltraBudgetDbContext _context;

        public SqlTransactionData(UltraBudgetDbContext context)
        {
            _context = context;
        }

        public Transaction Add(Transaction newTransaction)
        {
            _context.Add(newTransaction);
            return newTransaction;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Transaction Get(int id)
        {
            return _context.Transactions.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions;
        }
    }

    public class InMemoryTransactionData : ITransactionData
    {
        static InMemoryTransactionData()
        {
            _transactions = new List<Transaction>()
            {
                new Transaction
                {
                    Id = 1,
                    Amount = 1,
                    Date = new DateTime(2016, 11, 19),
                    Type = TransactionType.Credit
                },
                new Transaction
                {
                    Id = 2,
                    Amount = 1,
                    Date = new DateTime(2016, 11, 20),
                    Type = TransactionType.Credit
                },
                new Transaction
                {
                    Id = 3,
                    Amount = 2,
                    Date = new DateTime(2016, 11, 20),
                    Type = TransactionType.Credit
                }
            };
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _transactions;
        }

        public Transaction Get(int id)
        {
            return _transactions.FirstOrDefault(t => t.Id == id);
        }

        public Transaction Add(Transaction newTransaction)
        {
            newTransaction.Id = _transactions.Max(t => t.Id) + 1;
            _transactions.Add(newTransaction);

            return newTransaction;
        }

        public void Commit()
        {
            // Not needed to save in memory datasource
        }

        static List<Transaction> _transactions;
    }
}
