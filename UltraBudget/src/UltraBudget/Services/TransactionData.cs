using System;
using System.Collections.Generic;
using System.Linq;
using UltraBudget.Entities;

namespace UltraBudget.Services
{
    public interface ITransactionData
    {
        IEnumerable<Transaction> GetAll();
        IEnumerable<Transaction> GetAllForCurrentUser(string userId);
        Transaction Get(int id);
        Transaction Add(Transaction newTransaction);
        void Commit();
        IEnumerable<Wallet> GetAllWalletsForCurrentUser(string userId);
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

        public IEnumerable<Transaction> GetAllForCurrentUser(string userId)
        {
            return _context.Transactions.Where(u => u.UserId == userId);
        }

        public IEnumerable<Wallet> GetAllWalletsForCurrentUser(string userId)
        {
            var wallets = _context.Wallets;

            return wallets;
        }
    }
}
