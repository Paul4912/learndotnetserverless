using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using DotNetServerless.Application.Common;
using DotNetServerless.Application.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DotNetServerless.Lambda.Functions
{
    public class ListNotesFunction
    {
        private readonly IServiceProvider _serviceProvider;

        public ListNotesFunction() : this(Startup
          .BuildContainer()
        .BuildServiceProvider())
        {
        }

        public ListNotesFunction(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var requestModel = new ListNotesRequest { userId = request.RequestContext.Identity.CognitoIdentityId };

            var mediator = _serviceProvider.GetService<IMediator>();
            var result = await mediator.Send(requestModel);

            return result == null ?
              new APIGatewayProxyResponse { StatusCode = 404, Headers = CommonHeaders.corsHeaders } :
              new APIGatewayProxyResponse { StatusCode = 200, Body = JsonConvert.SerializeObject(result) , Headers = CommonHeaders.corsHeaders };
        }
    }
}
