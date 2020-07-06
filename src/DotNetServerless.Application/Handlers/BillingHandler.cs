using Amazon.DynamoDBv2;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using DotNetServerless.Application.Infrastructure;
using DotNetServerless.Application.Requests;
using DotNetServerless.Application.Responses;
using MediatR;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetServerless.Application.Handlers
{
    public class BillingHandler : IRequestHandler<BillingRequest, BillingResponse>
    {
        private readonly AmazonSimpleSystemsManagementClient _client;

        public BillingHandler(IAwsClientFactory<AmazonSimpleSystemsManagementClient> clientFactory)
        {
            _client = clientFactory.GetAwsClient();
        }

        public async Task<BillingResponse> Handle(BillingRequest request, CancellationToken cancellationToken)
        {
            var stripeChargeRequest = request.MapToStripeRequest();

            var client = new AmazonSimpleSystemsManagementClient();
            var ssmRequest = new GetParameterRequest
            {
                //differentiate between dev and test later
                Name = "/stripeSecretKey/test",
                WithDecryption = true
            };
            var ssmResponse = client.GetParameterAsync(ssmRequest).Result;
            var stripeKey = ssmResponse.Parameter.Value;

            StripeConfiguration.ApiKey = stripeKey;
            var stripeParameters = new ChargeCreateOptions
            {
                Amount = stripeChargeRequest.Amount,
                Currency = "usd",
                Source = stripeChargeRequest.Source,
                Description = "Scratch Charge",
            };
            var service = new ChargeService();
            var response = service.Create(stripeParameters);

            return response == null ? new BillingResponse { Status = false } : new BillingResponse { Status = true };
        }
    }
}
