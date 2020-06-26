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
    public class DeleteNoteFunction
    {
        private readonly IServiceProvider _serviceProvider;

        public DeleteNoteFunction() : this(Startup
          .BuildContainer()
        .BuildServiceProvider())
        {
        }

        public DeleteNoteFunction(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var requestModel = new DeleteNoteRequest { noteId = new Guid(request.PathParameters["noteId"]), userId = request.RequestContext.Identity.CognitoIdentityId };

            var mediator = _serviceProvider.GetService<IMediator>();
            var result = await mediator.Send(requestModel);

            return result == null ?
              new APIGatewayProxyResponse { StatusCode = 404, Headers = CommonHeaders.corsHeaders } :
              new APIGatewayProxyResponse { StatusCode = 200, Body = JsonConvert.SerializeObject(result), Headers = CommonHeaders.corsHeaders };
        }
    }
}
