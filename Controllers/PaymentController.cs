using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Stripe;

namespace StripeTest.Controllers
{
    public class PaymentController : Controller
    {
        private int amount = 100;
        public IActionResult Index()
        {
            ViewBag.PaymentAmount = amount;
            return View();
        }

        [HttpPost]
        public IActionResult Processing(string stripeToken, string stripeEmail)
        {
            Dictionary<string, string> Metadata = new Dictionary<string, string>();
            Metadata.Add("Product", "RubberDuck");
            Metadata.Add("Quantity", "10");
            var options = new ChargeCreateOptions
            {
                Amount = amount,
                Currency = "USD",
                Description = "Buying 10 rubber ducks",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
                Metadata = Metadata
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            return View();
        }
    }
}
