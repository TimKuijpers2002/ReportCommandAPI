using Cassandra;

namespace ReportCommandAPI.Data
{
    public class ReportCommandDbConnector
    {
        private readonly Cluster _cluster;

        public ReportCommandDbConnector(string contactPoint, int port)
        {
            _cluster = Cluster.Builder()
                .AddContactPoint(contactPoint)
                .WithPort(port)
                .Build();
        }

        public Cluster GetCluster()
        {
            return _cluster;
        }

        public void Dispose()
        {
            _cluster?.Dispose();
        }
    }
}
