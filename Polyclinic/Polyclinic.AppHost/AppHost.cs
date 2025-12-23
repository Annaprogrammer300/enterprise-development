var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume();
var postgresDb = postgres.AddDatabase("postgresDb");

var api = builder
    .AddProject<Projects.Polyclinic_Api_Host>("api")
    .WithReference(postgresDb)
    .WaitFor(postgresDb);

var grpcServer = builder.AddProject<Projects.Polyclinic_Grpc_Host>("polyclinic-grpc-host")
    .WithHttpsEndpoint(port: 7002, name: "grpc-https")
    .WithHttpEndpoint(port: 5002, name: "grpc-http")
    .WithReference(postgresDb)
    .WaitFor(postgresDb)
    .WaitFor(api);

builder.AddProject<Projects.Polyclinic_Generator_Grpc_Client>("polyclinic-generator-grpc-client")
    .WithReference(grpcServer)
    .WaitFor(postgresDb)
    .WaitFor(grpcServer);

builder.Build().Run();