using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using DotNetServerless.Application.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DotNetServerless.Lambda.Functions
{
    public class UpdateNoteFunction
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateNoteFunction() : this(Startup
          .BuildContainer()
        .BuildServiceProvider())
        {
        }

        public UpdateNoteFunction(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<UpdateNoteRequest>(request.Body);
            //requestModel.userId = request.RequestContext.Identity.CognitoIdentityId;
            var mediator = _serviceProvider.GetService<IMediator>();
            var result = await mediator.Send(requestModel);

            return new APIGatewayProxyResponse { StatusCode = 200, Body = JsonConvert.SerializeObject(result) };
        }
    }
}
