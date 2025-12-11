var builder = DistributedApplication.CreateBuilder(args);

//var password = builder.AddParameter("DatabasePassword");
//var dbName = "polyclinic";
//var polyclinicDb = builder
//    .AddPostgres("polyclinic-postgres", password: password)
//    .AddDatabase(dbName);

//builder.AddProject<Projects.Polyclinic_Api_Host>("polyclinic-api-host")
//    .WithReference(polyclinicDb, "Database")
//    .WaitFor(polyclinicDb);

//var postgres = builder.AddPostgres("postgres");

//var postgresDb = postgres.AddDatabase("polyclinicdb");

//var api = builder.AddProject<Projects.Polyclinic_Api_Host>("polyclinic-api-host")
//    .WithReference(postgresDb, "postgresDb")
//    .WaitFor(postgresDb);


builder.Build().Run();
