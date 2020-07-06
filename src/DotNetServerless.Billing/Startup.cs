using DotNetServerless.Application.Infrastructure;
using DotNetServerless.Application.Infrastructure.Configs;
using DotNetServerless.Billing.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace DotNetServerless.Billing
{
    public class Startup
    {
        public static IServiceCollection BuildContainer()
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddEnvironmentVariables()
              .Build();

            return ConfigureServices(configuration);
        }


        private static IServiceCollection ConfigureServices(IConfigurationRoot configurationRoot)
        {
            var services = new ServiceCollection();

            services
              .AddMediatR(AppDomain.CurrentDomain.Load("DotNetServerless.Application"))
              .AddTransient(typeof(IAwsClientFactory<>), typeof(AwsClientFactory<>))
              .BindAndConfigure(configurationRoot.GetSection("AwsBasicConfiguration"), new AwsBasicConfiguration());

            return services;
        }
    }
}
