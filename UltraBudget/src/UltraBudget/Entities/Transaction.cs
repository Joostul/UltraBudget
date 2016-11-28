using System;
using System.ComponentModel.DataAnnotations;

namespace UltraBudget.Entities
{
    public enum TransactionType
    {
        [Display(Name = "Credit")]
        Credit = 1,
        [Display(Name = "Debit")]
        Debit = 2
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
        
        public string UserId { get; set; }
    }
}
