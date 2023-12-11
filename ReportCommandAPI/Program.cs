using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Cassandra;
using ReportCommandAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration
ConfigurationManager configuration = builder.Configuration;

// Cassandra configuration
var cassandraSettings = configuration.GetSection("CassandraSettings");

// Cassandra setup
var session = Cluster.Builder()
        .WithCloudSecureConnectionBundle(cassandraSettings["SecureConnectionBundlePath"])
        .WithCredentials(cassandraSettings["ClientId"], cassandraSettings["ClientSecret"])
        .Build()
        .Connect(cassandraSettings["Keyspace"]);

// Register Cassandra Session as a Singleton
builder.Services.AddSingleton(session);

// Additional configurations...

var app = builder.Build();

app.MapGrpcService<GPReportCommandService>();
app.MapGet("/", () => "health check");

app.Run();
