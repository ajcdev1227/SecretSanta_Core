using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class PaymentModel
    {
        public string StripePublishableKey { get; set; }
        public string StripeToken { get; set; }
        public decimal Amount { get; set; }
    }
}