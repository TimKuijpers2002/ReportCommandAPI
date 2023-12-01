using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Cassandra;
using ReportCommandAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Cassandra configuration
var cassandraSettings = builder.Configuration.GetSection("CassandraSettings");
var contactPoint = cassandraSettings["ContactPoint"];
var port = cassandraSettings["Port"];
var keyspace = cassandraSettings["Keyspace"];

// Cassandra setup
var cluster = Cluster.Builder()
    .AddContactPoint(contactPoint)
    .WithPort(int.Parse(port))
    .Build();
var session = cluster.Connect(keyspace);

// Register Cassandra Session as a Singleton
builder.Services.AddSingleton(session);

// Additional configurations...

var app = builder.Build();

app.MapGrpcService<GPReportCommandService>();
app.MapGet("/", () => "health check");

app.Run();
