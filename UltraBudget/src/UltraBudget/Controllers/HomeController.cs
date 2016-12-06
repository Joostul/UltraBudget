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
    //[Route("[controller]/[action]")]
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
            model.Transactions = _transactionData.GetTransactionsForCurrentUser(_currentUserId);
            model.Greeting = _greeter.GetGreeting();
            model.Wallets = _transactionData.GetWalletsForCurrentUser(_currentUserId);

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _transactionData.Get(id);
            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Wallets = new SelectList(_transactionData.GetWalletNamesForCurrentUser(_currentUserId));
            var model = _transactionData.Get(id);
            if(model == null)
            {
                return RedirectToAction("Index");                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TransactionEditViewModel model)
        {
            var transaction = _transactionData.Get(id);
            if(ModelState.IsValid)
            {
                transaction.Amount = model.Amount;
                transaction.Date = model.Date;
                transaction.Type = model.Type;
                transaction.Wallet = _transactionData.GetWalletBasedOnName(model.Wallet);
                if(transaction.Type == TransactionType.Invest)
                {
                    transaction.Price = model.Price;
                }
                else
                {
                    transaction.Price = null;
                }
                transaction.UserId = _currentUserId;
                _transactionData.Commit();
                return RedirectToAction("Details", new { id = transaction.Id });
            }
            return View(transaction);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Wallets = new SelectList(_transactionData.GetWalletNamesForCurrentUser(_currentUserId));

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

                //var newTransaction = new Transaction();
                //newTransaction.Amount = model.Amount;
                //newTransaction.Date = model.Date;
                //newTransaction.Type = model.Type;
                //newTransaction.UserId = _currentUserId;
                //newTransaction.Wallet = _transactionData.GetWalletBasedOnName(model.Wallet);
                //newTransaction = _transactionData.Add(newTransaction);
                //_transactionData.Commit();
                return RedirectToAction("Details", new { id = newWallet.Id });
            }

            return View();
        }
    }
}
