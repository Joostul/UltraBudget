using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UltraBudget.Entities;
using System;

namespace UltraBudget.Services
{
    public interface ITransactionData
    {
        IEnumerable<Transaction> GetTransactions();
        IEnumerable<Transaction> GetTransactionsForUser(string userId);
        IEnumerable<Transaction> GetTransactionsForUser(string userId, int walletId);
        Transaction Get(int id);
        Wallet GetWallet(int id);
        Category GetCategory(int id);
        Transaction Add(Transaction newTransaction);
        Wallet Add(Wallet newWallet);
        Category Add(Category newCategory);
        List<Wallet> GetWalletsForUser(string userId);
        List<Category> GetCategoriesForUser(string userId);
        List<Payee> GetPayeesForUser(string userId);
        IEnumerable<string> GetWalletNamesForUser(string userId);
        IEnumerable<string> GetCurrencieNamesForUser(string userId);
        IEnumerable<string> GetCategoryNamesForUser(string userId);
        IEnumerable<string> GetPayeeNamesForUser(string userId);
        Wallet GetWalletBasedOnName(string WalletName);
        Currency GetCurrencyBasedOnName(string CurrencyName);
        Category GetCategoryBasedOnName(string CategoryName);
        Payee GetPayeeBasedOnName(string PayeeName);
        void Commit();
    }

    public class SqlTransactionData : ITransactionData
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

        public Wallet Add(Wallet newWallet)
        {
            _context.Add(newWallet);
            return newWallet;
        }

        public Category Add(Category newCategory)
        {
            _context.Add(newCategory);
            return newCategory;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Transaction Get(int id)
        {
            return _context.Transactions
                .Include(w => w.Wallet)
                .Include(c => c.Category)
                .ToArray()
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _context.Transactions
                .Include(w => w.Wallet)
                .ToArray();
        }

        public IEnumerable<Transaction> GetTransactionsForUser(string userId)
        {
            return _context.Transactions
                .Where(u => u.UserId == userId)
                .Include(w => w.Wallet)
                .Include(c => c.Category)
                .ToArray();
        }

        public IEnumerable<Transaction> GetTransactionsForUser(string userId, int walletId)
        {
            return _context.Transactions
                .Where(u => u.UserId == userId)
                .Where(w => w.Wallet.Id == walletId)
                .Include(w => w.Wallet)
                .Include(c => c.Category)
                .ToArray();
        }

        public List<Wallet> GetWalletsForUser(string userId)
        {
            return _context.Wallets
                .Where(u => u.UserId == userId)
                .Include(c => c.Currency)
                .ToList();
        }

        public List<Payee> GetPayeesForUser(string userId)
        {
            return _context.Payees
                .Where(u => u.UserId == userId)
                .ToList();
        }
        
        public IEnumerable<string> GetPayeeNamesForUser(string userId)
        {
            var payees = _context.Payees
                .Where(u => u.UserId == userId)
                .ToArray();

            List<string> payeeNames = new List<string>();

            foreach (var payee in payees)
            {
                payeeNames.Add(payee.Name);
            }

            return payeeNames;
        }

        public IEnumerable<string> GetWalletNamesForUser(string userId)
        {
            var wallets = _context.Wallets
                .Where(u => u.UserId == userId)
                .Include(c => c.Currency)
                .ToArray();

            List<string> walletNames = new List<string>();

            foreach (var wallet in wallets)
            {
                walletNames.Add(wallet.Name);
            }

            return walletNames;
        }

        public Wallet GetWalletBasedOnName(string walletName)
        {
            return _context.Wallets
                .Include(c => c.Currency)
                .ToArray()
                .FirstOrDefault(u => u.Name == walletName);
        }

        public IEnumerable<string> GetCurrencieNamesForUser(string userId)
        {
            var currencies = _context.Currencies.ToArray();

            List<string> currencyNames = new List<string>();

            foreach (var currency in currencies)
            {
                currencyNames.Add(currency.Name);
            }
            return currencyNames;
        }

        public Wallet GetWallet(int id)
        {
            return _context.Wallets
                   .Include(c => c.Currency)
                   .ToArray()
                   .FirstOrDefault(u => u.Id == id);
        }

        public Currency GetCurrencyBasedOnName(string CurrencyName)
        {
            return _context.Currencies
                .Include(e => e.ExchangeRate)
                .ToArray()
                .FirstOrDefault(u => u.Name == CurrencyName);
        }

        public Payee GetPayeeBasedOnName(string PayeeName)
        {
            return _context.Payees
                .ToArray()
                .FirstOrDefault(p => p.Name == PayeeName);
        }

        public List<Category> GetCategoriesForUser(string userId)
        {
            return _context.Categories.Where(u => u.UserId == userId).ToList();
        }

        public Category GetCategoryBasedOnName(string CategoryName)
        {
            return _context.Categories
                .ToArray()
                .FirstOrDefault(c => c.Name == CategoryName);
        }

        public IEnumerable<string> GetCategoryNamesForUser(string userId)
        {
            var categories = _context.Categories.ToArray();

            List<string> categoryNames = new List<string>();

            foreach (var category in categories)
            {
                categoryNames.Add(category.Name);
            }
            return categoryNames;
        }

        public Category GetCategory(int id)
        {
            return _context.Categories
                .ToArray()
                .FirstOrDefault(t => t.Id == id);
        }
    }
}
