using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UltraBudget.Entities;

namespace UltraBudget.ViewModels
{
    public class WalletEditViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Currency Currency { get; set; }
    }
}
