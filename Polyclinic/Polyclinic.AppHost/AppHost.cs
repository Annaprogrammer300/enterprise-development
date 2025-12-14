var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume();

var postgresDb = postgres.AddDatabase("postgresDb");
builder
    .AddProject<Projects.Polyclinic_Api_Host>("api")
    .WithReference(postgresDb)
    .WaitFor(postgresDb);

builder.Build().Run();
