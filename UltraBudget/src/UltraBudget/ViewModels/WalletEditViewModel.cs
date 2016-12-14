using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UltraBudget.ViewModels
{
    public class WalletEditViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Currency { get; set; }
    }
}
