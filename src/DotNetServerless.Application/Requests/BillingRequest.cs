using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Responses;
using MediatR;

namespace DotNetServerless.Application.Requests
{
    public class BillingRequest : IRequest<BillingResponse>
    {
        public int Storage { get; set; }
        public string Source { get; set; }

        //types may be wrong fix later
        public StripeCharge MapToStripeRequest()
        {
            return new StripeCharge
            {
                Source = Source,
                Amount = CalculateCost(Storage),
                Description = "Scratch Charge",
                Currency = "usd"
            };
        }

        private static int CalculateCost(int storage)
        {
            int rate = 1;
            rate = storage <= 100 ? 2 : rate;
            rate = storage <= 10 ? 4 : rate;
            return storage * rate * 100;
        }
    }
}
