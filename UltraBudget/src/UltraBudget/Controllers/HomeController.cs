using Microsoft.AspNetCore.Mvc;
using System;
using UltraBudget.Models;
using UltraBudget.Services;

namespace UltraBudget.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController: Controller
    {
        private ITransactionData _transactionData;

        public HomeController(ITransactionData transactionData)
        {
            _transactionData = transactionData;
        }
        public IActionResult Index()
        {
            var model = _transactionData.GetAll();

            return View(model);
        }
    }
}
