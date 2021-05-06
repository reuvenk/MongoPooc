namespace MongoPocWebApplication1
{
    public class MongoCluster
    {
        public const string SectionName = "Mongo:Cluster";
        public string Host { get; set; }
        public int Port { get; set; }
        public int MaxPoolSize { get; set; }
    }

    public class MongoInstance
    {
        public const string SectionName = "Mongo:Instance";
        public string Environment { get; set; }
        public string Team { get; set; }
        public string Username { get; set; }
        public string Domain { get; set; }
    }
}