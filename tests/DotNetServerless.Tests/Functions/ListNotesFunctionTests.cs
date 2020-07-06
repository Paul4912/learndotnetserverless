using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Infrastructure.Repositories;
using DotNetServerless.Notes;
using DotNetServerless.Notes.Functions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace DotNetServerless.Tests.Functions
{
    public class ListNotesFunctionTests
    {
        private readonly ListNotesFunction _sut;
        private readonly Mock<INoteRepository> _mockRepository;

        public ListNotesFunctionTests()
        {
            _mockRepository = new Mock<INoteRepository>();
            _mockRepository.Setup(_ => _.ListByUserId<Note>(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<Note> { new Note { noteId = Guid.NewGuid().ToString() } });

            var serviceCollection = Startup.BuildContainer();

            serviceCollection.Replace(new ServiceDescriptor(typeof(INoteRepository), _ => _mockRepository.Object,
              ServiceLifetime.Transient));

            _sut = new ListNotesFunction(serviceCollection.BuildServiceProvider());
        }

        [Fact]
        public async Task run_should_trigger_mediator_handler_and_repository()
        {
            await _sut.Run(new APIGatewayProxyRequest
            {
                RequestContext = new APIGatewayProxyRequest.ProxyRequestContext
                {
                    Identity = new APIGatewayProxyRequest.RequestIdentity
                    {
                        CognitoIdentityId = "21123123213123"
                    }
                }
            });
            _mockRepository.Verify(_ => _.ListByUserId<Note>(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(200)]
        public async Task run_should_return_200_when_find_the_record(int statusCode)
        {
            var result = await _sut.Run(new APIGatewayProxyRequest
            {
                RequestContext = new APIGatewayProxyRequest.ProxyRequestContext
                {
                    Identity = new APIGatewayProxyRequest.RequestIdentity
                    {
                        CognitoIdentityId = "21123123213123"
                    }
                }
            });

            Assert.Equal(result.StatusCode, statusCode);
        }
    }
}
