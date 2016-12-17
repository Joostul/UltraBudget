using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UltraBudget.Entities;
using UltraBudget.Services;

namespace UltraBudget.ViewComponents
{
    public class WalletMenuViewComponent: ViewComponent
    {

        private ITransactionData _transactionData;
        private UserManager<User> _userManager;
        private static string _currentUserId;

        public WalletMenuViewComponent(ITransactionData transactionData, UserManager<User> userManager)
        {
            _transactionData = transactionData;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            _currentUserId = _userManager.GetUserId(HttpContext.User);
            var wallets = _transactionData.GetWalletsForUser(_currentUserId);

            return View(wallets);
        }
    }
}
