using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraBudget.Entities;

namespace UltraBudget.Services
{
    public interface IGreeter
    {
        string GetGreeting();
    }

    public class Greeter : IGreeter
    {
        private IEnumerable<Greeting> _greetings;
        private int _amountOfGreetings;

        public Greeter(UltraBudgetDbContext context)
        {
            _greetings = context.Greetings;
            _amountOfGreetings = context.Greetings.Count();
        }

        public string GetGreeting()
        {
            Random rand = new Random();
            int amountToSkip = rand.Next(0, _amountOfGreetings);
            return _greetings.OrderBy(r => Guid.NewGuid()).Skip(amountToSkip).Take(1).FirstOrDefault().Message;
        }
    }
}
