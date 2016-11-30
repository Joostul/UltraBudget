using System;
using System.ComponentModel.DataAnnotations;

namespace UltraBudget.Entities
{
    public enum TransactionType
    {
        [Display(Name = "Credit")]
        Credit = 1,
        [Display(Name = "Debit")]
        Debit = 2,
        [Display(Name = "Investment")]
        Invest = 3
    }

    public class Transaction
    {
        public int Id { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Transaction Amount")]
        public double Amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Transaction Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Transaction Type")]
        public TransactionType Type { get; set; }

        [Display(Name = "Wallet")]
        public Wallet Wallet { get; set; }

        [Display(Name = "Price of the transaction")]
        public double? Price { get; set; }

        [Display(Name = "UserId")]
        public string UserId { get; set; }
    }
}
