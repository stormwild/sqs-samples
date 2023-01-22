namespace SQSSampleAPI.Configuration;

public class AWS
{
    public SQS SQS { get; set; } = new SQS();
}

public class SQS
{
    public string QueueName { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string QueueUrl { get; set; } = string.Empty;
}