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
            model.Transactions = _transactionData.GetTransactions();
            model.Greeting = _greeter.GetGreeting();
            model.Wallets = _transactionData.GetWalletsForUser(_currentUserId);

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _transactionData.GetWallet(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Currencies = new SelectList(_transactionData.GetCurrencieNamesForUser(_currentUserId));
            var model = _transactionData.GetWallet(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, WalletEditViewModel model)
        {
            var wallet = _transactionData.GetWallet(id);
            if (ModelState.IsValid)
            {
                wallet.Currency = _transactionData.GetCurrencyBasedOnName(model.Currency);
                wallet.Name = model.Name;
                wallet.UserId = _currentUserId;
                _transactionData.Commit();
                return RedirectToAction("Details", new { id = wallet.Id });
            }
            return View(wallet);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Currencies = new SelectList(_transactionData.GetCurrencieNamesForUser(_currentUserId));

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
