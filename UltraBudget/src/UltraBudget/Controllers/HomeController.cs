using Microsoft.AspNetCore.Mvc;
using UltraBudget.Entities;
using UltraBudget.Services;
using UltraBudget.ViewModels;

namespace UltraBudget.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController: Controller
    {
        private IGreeter _greeter;
        private ITransactionData _transactionData;

        public HomeController(ITransactionData transactionData, IGreeter greeter)
        {
            _transactionData = transactionData;
            _greeter = greeter;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Transactions = _transactionData.GetAll();
            model.Greeting = _greeter.GetGreeting();

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
                _transactionData.Commit();
                return RedirectToAction("Details", new { id = transaction.Id });
            }
            return View(transaction);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransactionEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var newTransaction = new Transaction();
                newTransaction.Amount = model.Amount;
                newTransaction.Date = model.Date;
                newTransaction.Type = model.Type;
                newTransaction = _transactionData.Add(newTransaction);
                _transactionData.Commit();
                return RedirectToAction("Details", new { id = newTransaction.Id });
            }

            return View();
        }
    }
}
