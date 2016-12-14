using System;
using System.ComponentModel.DataAnnotations;
using UltraBudget.Entities;

namespace UltraBudget.ViewModels
{
    public class TransactionEditViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public string Wallet { get; set; }
        
        public double? Price { get; set; }
    }
}
