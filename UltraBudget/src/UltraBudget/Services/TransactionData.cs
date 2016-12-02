using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UltraBudget.Entities;

namespace UltraBudget.Services
{
    public interface ITransactionData
    {
        IEnumerable<Transaction> GetTransactions();
        IEnumerable<Transaction> GetTransactionsForCurrentUser(string userId);
        Transaction Get(int id);
        Transaction Add(Transaction newTransaction);
        void Commit();
        IEnumerable<Wallet> GetWalletsForCurrentUser(string userId);
        IEnumerable<string> GetWalletNamesForCurrentUser(string userId);

        Wallet GetWalletBasedOnName(string WalletName);
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
            return _context.Transactions.Include(w => w.Wallet).ToArray().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _context.Transactions.Include(w => w.Wallet).ToArray();
        }

        public IEnumerable<Transaction> GetTransactionsForCurrentUser(string userId)
        {
            return _context.Transactions.Where(u => u.UserId == userId).Include(w => w.Wallet).ToArray();
        }

        public IEnumerable<Wallet> GetWalletsForCurrentUser(string userId)
        {
            var wallets = _context.Wallets.Where(u => u.UserId == userId).Include(c => c.Currency).ToArray();

            return wallets;
        }

        public IEnumerable<string> GetWalletNamesForCurrentUser(string userId)
        {
            var wallets = _context.Wallets.Where(u => u.UserId == userId).Include(c => c.Currency).ToArray();

            List<string> walletNames = new List<string>();

            foreach(var wallet in wallets)
            {
                walletNames.Add(wallet.Name);
            }

            return walletNames;
        }

        public Wallet GetWalletBasedOnName(string walletName)
        {
            var wallet = _context.Wallets.Include(c => c.Currency).ToArray().FirstOrDefault(u => u.Name == walletName);

            return wallet;
        }
    }
}
