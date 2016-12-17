using System.ComponentModel.DataAnnotations;

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
