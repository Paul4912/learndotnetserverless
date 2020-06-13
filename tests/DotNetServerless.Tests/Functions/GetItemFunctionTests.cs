using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Infrastructure.Repositories;
using DotNetServerless.Lambda;
using DotNetServerless.Lambda.Functions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace DotNetServerless.Tests.Functions
{
    public class GetItemFunctionTests
    {
        public GetItemFunctionTests()
        {
            _mockRepository = new Mock<INoteRepository>();
            _mockRepository.Setup(_ => _.GetById<Note>(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<Note> { new Note { noteId = Guid.NewGuid().ToString() } });

            var serviceCollection = Startup.BuildContainer();

            serviceCollection.Replace(new ServiceDescriptor(typeof(INoteRepository), _ => _mockRepository.Object,
              ServiceLifetime.Transient));

            _sut = new GetNoteFunction(serviceCollection.BuildServiceProvider());
        }

        private readonly GetNoteFunction _sut;
        private readonly Mock<INoteRepository> _mockRepository;

        [Fact]
        public async Task run_should_trigger_mediator_handler_and_repository()
        {
            await _sut.Run(new APIGatewayProxyRequest
            {
                PathParameters = new Dictionary<string, string>
                {
                  { "id", Guid.NewGuid().ToString()}
                }
            });
            _mockRepository.Verify(_ => _.GetById<Note>(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(200)]
        public async Task run_should_return_200_when_find_the_record(int statusCode)
        {
            var result = await _sut.Run(new APIGatewayProxyRequest
            {
                PathParameters = new Dictionary<string, string>
                {
                    { "id", Guid.NewGuid().ToString()}
                }
            });

            Assert.Equal(result.StatusCode, statusCode);
        }

        [Theory]
        [InlineData(404)]
        public async Task run_should_return_404_when_NOT_find_the_record(int statusCode)
        {
            _mockRepository.Setup(_ => _.GetById<Note>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(new List<Note>());

            var result = await _sut.Run(new APIGatewayProxyRequest
            {
                PathParameters = new Dictionary<string, string>
                {
                  { "id", Guid.NewGuid().ToString()}
                }
            });

            Assert.Equal(result.StatusCode, statusCode);
        }
    }
}
