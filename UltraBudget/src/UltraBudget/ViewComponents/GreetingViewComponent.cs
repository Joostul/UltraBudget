﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UltraBudget.Services;

namespace UltraBudget.ViewComponents
{
    public class GreetingViewComponent : ViewComponent
    {
        private IGreeter _greeter;
        public GreetingViewComponent(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public IViewComponentResult Invoke()
        {
            var model = _greeter.GetGreeting();
            return View("Default", model);
        }
    }
}