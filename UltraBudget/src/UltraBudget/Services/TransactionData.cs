﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UltraBudget.Entities;
using System;

namespace UltraBudget.Services
{
    public interface ITransactionData
    {
        IEnumerable<Transaction> GetTransactions();
        IEnumerable<Transaction> GetTransactionsForCurrentUser(string userId);
        IEnumerable<Transaction> GetTransactionsForCurrentUser(string userId, int walletId);
        Transaction Get(int id);
        Transaction Add(Transaction newTransaction);
        Wallet Add(Wallet newWallet);
        void Commit();
        List<Wallet> GetWalletsForCurrentUser(string userId);
        IEnumerable<string> GetWalletNamesForCurrentUser(string userId);
        IEnumerable<string> GetCurrencieNamesForCurrentUser(string userId);
        Wallet GetWalletBasedOnName(string WalletName);
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
            return _context.Transactions
                .Where(u => u.UserId == userId)
                .Include(w => w.Wallet)
                .ToArray();
        }

        public IEnumerable<Transaction> GetTransactionsForCurrentUser(string userId, int walletId)
        {
            return _context.Transactions
                .Where(u => u.UserId == userId)
                .Where(w => w.Wallet.Id == walletId)
                .Include(w => w.Wallet)
                .ToArray();
        }

        public List<Wallet> GetWalletsForCurrentUser(string userId)
        {
            var wallets = _context.Wallets
                .Where(u => u.UserId == userId)
                .Include(c => c.Currency)
                .ToList();

            return wallets;
        }

        public IEnumerable<string> GetWalletNamesForCurrentUser(string userId)
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
            var wallet = _context.Wallets
                .Include(c => c.Currency)
                .ToArray()
                .FirstOrDefault(u => u.Name == walletName);

            return wallet;
        }

        public IEnumerable<string> GetCurrencieNamesForCurrentUser(string userId)
        {
            var currencies = _context.Currencies.ToArray();

            List<string> currencyNames = new List<string>();

            foreach (var currency in currencies)
            {
                currencyNames.Add(currency.Name);
            }
            return currencyNames;
        }
    }
}
