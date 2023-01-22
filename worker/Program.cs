using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SQSSampleAPI.Configuration;

namespace MTWorker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(h =>
                {
                    h.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration.GetSection("AWS:SQS");
                    
                    Debug.WriteLine(config["QueueName"]);
                    
                    services.Configure<SQS>(config);

                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();

                        // By default, sagas are in-memory, but should be changed to a durable
                        // saga repository.
                        x.SetInMemorySagaRepositoryProvider();

                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumers(entryAssembly);
                        x.AddSagaStateMachines(entryAssembly);
                        x.AddSagas(entryAssembly);
                        x.AddActivities(entryAssembly);

                        x.UsingAmazonSqs((context, cfg) =>
                        {
                            var options = context.GetRequiredService<IOptions<SQS>>();

                            var sqs = options.Value;
                            
                            Debug.WriteLine(sqs.QueueName);
                            
                            cfg.Host("us-west-2", h =>
                            {
                                h.AccessKey(sqs.AccessKey);
                                h.SecretKey(sqs.SecretKey);
                            });
                            
                            cfg.ConfigureEndpoints(context);
                        });
                        // x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
                    });

                    services.AddHostedService<Worker>();
                });
    }
}