using Microsoft.AspNetCore.Mvc;
using UltraBudget.Services;
using Microsoft.AspNetCore.Identity;
using UltraBudget.Entities;
using UltraBudget.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UltraBudget.Controllers
{
    public class WalletsController : Controller
    {
        private IGreeter _greeter;
        private ITransactionData _transactionData;
        private UserManager<User> _userManager;
        private static string _currentUserId;

        public WalletsController(ITransactionData transactionData, IGreeter greeter, UserManager<User> userManager)
        {
            _transactionData = transactionData;
            _greeter = greeter;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var model = new HomePageViewModel();
            _currentUserId = _userManager.GetUserId(HttpContext.User);
            
            model.Transactions = id == 0 ? 
                _transactionData.GetTransactionsForUser(_currentUserId) :
                model.Transactions = _transactionData.GetTransactionsForUser(_currentUserId, id);
            
            model.Greeting = _greeter.GetGreeting();
            model.Wallets = _transactionData.GetWalletsForUser(_currentUserId);
            model.Categories = _transactionData.GetCategoriesForUser(_currentUserId);

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _transactionData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Wallets = new SelectList(_transactionData.GetWalletNamesForUser(_currentUserId));
            ViewBag.Categories = new SelectList(_transactionData.GetCategoryNamesForUser(_currentUserId));
            var model = _transactionData.Get(id);
            if (model == null)
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
            if (ModelState.IsValid)
            {
                transaction.Amount = model.Amount;
                transaction.Date = model.Date;
                transaction.Type = model.Type;
                transaction.Category = _transactionData.GetCategoryBasedOnName(model.Category);
                transaction.Wallet = _transactionData.GetWalletBasedOnName(model.Wallet);
                if (transaction.Type == TransactionType.Invest)
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
            ViewBag.Currencies = new SelectList(_transactionData.GetCurrencieNamesForUser(_currentUserId));
            ViewBag.Wallets = new SelectList(_transactionData.GetWalletNamesForUser(_currentUserId));
            ViewBag.Categories = new SelectList(_transactionData.GetCategoryNamesForUser(_currentUserId));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransactionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newTransaction = new Transaction();
                newTransaction.Amount = model.Amount;
                newTransaction.Date = model.Date;
                newTransaction.Type = model.Type;
                newTransaction.UserId = _currentUserId;
                newTransaction.Wallet = _transactionData.GetWalletBasedOnName(model.Wallet);
                newTransaction.Category = _transactionData.GetCategoryBasedOnName(model.Category);
                newTransaction = _transactionData.Add(newTransaction);
                _transactionData.Commit();
                return RedirectToAction("Details", new { id = newTransaction.Id });
            }

            return View();
        }
    }
}
