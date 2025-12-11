var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var postgresDb = postgres.AddDatabase("polyclinicdb");

var _ = builder.AddProject<Projects.Polyclinic_Api_Host>("polyclinic-api-host")
    .WithReference(postgresDb, "postgresDb")
    .WaitFor(postgresDb);


builder.Build().Run();
