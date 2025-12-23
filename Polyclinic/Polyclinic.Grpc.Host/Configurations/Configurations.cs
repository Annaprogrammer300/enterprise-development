namespace Polyclinic.Grpc.Host.Configurations;
public class GrpcClientConfigurations
{
    public string ServerUrl { get; set; } = "http://localhost:5051";
    public int TimeoutSeconds { get; set; } = 60;
    public int MaxRetries { get; set; } = 3;
    public bool EnableStatistics { get; set; } = true;
    public int StatisticsInterval { get; set; } = 5;
    public bool ShowPatientDetails { get; set; } = false;
    public bool SaveToFile { get; set; } = false;
    public string OutputFilePath { get; set; } = "patients.json";
}
