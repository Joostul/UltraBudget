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
        private Greeting _greeting;

        public Greeter(UltraBudgetDbContext context)
        {
            Random rand = new Random();
            int amountToSkip = rand.Next(0, context.Greetings.Count());
            _greeting = context.Greetings.OrderBy(r => Guid.NewGuid()).Skip(amountToSkip).Take(1).FirstOrDefault();
        }

        public string GetGreeting()
        {
            return _greeting.Message;
        }
    }
}
