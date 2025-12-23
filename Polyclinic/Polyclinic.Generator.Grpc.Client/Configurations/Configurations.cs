namespace Polyclinic.Generator.Grpc.Client.Configurations;

public class GeneratorOptions
{
    public int BatchSize { get; set; } = 15;
    public int PayloadLimit { get; set; } = 100;
    public int WaitTime { get; set; } = 10;
    public int MaxRetries { get; set; } = 3;
    public int RetryDelaySeconds { get; set; } = 5;
    public int GrpcTimeoutSeconds { get; set; } = 30;
}