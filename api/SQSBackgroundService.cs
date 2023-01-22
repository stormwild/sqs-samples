using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using SQSSampleAPI.Configuration;

namespace SQSSampleAPI;

public class SQSBackgroundService : BackgroundService
{
    private readonly SQS _sqs;

    private readonly IAmazonSQS _client;

    public SQSBackgroundService(IOptions<SQS> config)
    {
        _sqs = config.Value;
        Console.WriteLine(_sqs.AccessKey);
        Console.WriteLine(_sqs.SecretKey);
        var credentials = new BasicAWSCredentials(_sqs.AccessKey, _sqs.SecretKey);
        _client = new AmazonSQSClient(credentials, RegionEndpoint.USWest2);
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine(_sqs.QueueName);

        await Start(_client, _sqs, stoppingToken);
    }

    private static async Task Start(IAmazonSQS client, SQS _sqs, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = _sqs.QueueUrl,
                MaxNumberOfMessages = 10
            };
            
            var messages = (await client.ReceiveMessageAsync(request, token)).Messages;

            if (messages.Any())
            {
                foreach (var message in messages)
                {
                    Console.WriteLine(message.MessageId);
                    Console.WriteLine(message.Body);
                }
            }
            else
            {
                Console.WriteLine("No message");
                await Task.Delay(TimeSpan.FromSeconds(5), token);
            }
            
        }
    }

}