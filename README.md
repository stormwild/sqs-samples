# SQS Sample

[Build an AWS SQS Background Service with .NET 5 — Part 1](https://medium.com/nuages-org/aws-sqs-background-service-net-5-part-1-63e3e730e3a2)

## Notes

```shell
dotnet user-secrets init
dotnet user-secrets set "AWS:SQS:QueueName" SQSSample
dotnet user-secrets set "AWS:SQS:AccessKey" KEY
dotnet user-secrets set "AWS:SQS:SecretKey" SECRET
```

```shell
curl https://raw.githubusercontent.com/github/gitignore/main/VisualStudio.gitignore -o .gitignore
curl https://raw.githubusercontent.com/JetBrains/resharper-rider-samples/master/.gitignore -o rider.ignore && cat rider.ignore >> .gitignore && rm rider.ignore
```
or 

```shell
RIDER_IGNORE=$(curl https://raw.githubusercontent.com/JetBrains/resharper-rider-samples/master/.gitignore) && echo "$RIDER_IGNORE" >> .gitignore
```

[Configuration in ASP.NET Core | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0)

[Safe storage of app secrets in development in ASP.NET Core | Microsoft Learn'](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows)

[Preserve Linebreaks When Storing Command Output to a Variable | Baeldung on Linux](https://www.baeldung.com/linux/variable-preserve-linebreaks)