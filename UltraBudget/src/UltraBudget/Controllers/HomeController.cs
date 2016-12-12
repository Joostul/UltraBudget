using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using UltraBudget.Entities;
using UltraBudget.Services;
using UltraBudget.ViewModels;

namespace UltraBudget.Controllers
{
    [Authorize]
    public class HomeController: Controller
    {
        private IGreeter _greeter;
        private ITransactionData _transactionData;
        private UserManager<User> _userManager;
        private static string _currentUserId;

        public HomeController(ITransactionData transactionData, IGreeter greeter, UserManager<User> userManager)
        {
            _transactionData = transactionData;
            _greeter = greeter;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            _currentUserId = _userManager.GetUserId(HttpContext.User);
            var model = new HomePageViewModel();
            model.Greeting = _greeter.GetGreeting();
            model.Wallets = _transactionData.GetWalletsForCurrentUser(_currentUserId);

            return View(model);
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TransactionEditViewModel model)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Currencies = new SelectList(_transactionData.GetCurrencieNamesForCurrentUser(_currentUserId));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Wallet model)
        {
            if(ModelState.IsValid)
            {
                var newWallet = new Wallet();
                newWallet.Currency = model.Currency;
                newWallet.Name = model.Name;
                newWallet.UserId = _currentUserId;
                newWallet.Transactions = new List<Transaction>();

                newWallet = _transactionData.Add(newWallet);
                _transactionData.Commit();
                //return RedirectToAction("Details", new { id = newWallet.Id });
                return View();
            }

            return View();
        }
    }
}
