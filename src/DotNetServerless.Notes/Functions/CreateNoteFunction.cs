using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using DotNetServerless.Application.Common;
using DotNetServerless.Application.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DotNetServerless.Notes.Functions
{
    public class CreateNoteFunction
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateNoteFunction() : this(Startup
          .BuildContainer()
          .BuildServiceProvider())
        {
        }

        public CreateNoteFunction(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<CreateNoteRequest>(request.Body);
            requestModel.userId = request.RequestContext.Identity.CognitoIdentityId;

            var mediator = _serviceProvider.GetService<IMediator>();
            var result = await mediator.Send(requestModel);

            return new APIGatewayProxyResponse { StatusCode = 201, Body = JsonConvert.SerializeObject(result), Headers = CommonHeaders.corsHeaders };
        }
    }
}
