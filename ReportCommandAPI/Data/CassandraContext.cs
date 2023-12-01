using Cassandra;

namespace ReportCommandAPI.Data
{
    public class CassandraContext
    {
        public Cassandra.ISession Session { get; private set; }

        public CassandraContext(Cluster cluster, string keyspace)
        {
            Session = cluster.Connect(keyspace);
        }
    }
}
