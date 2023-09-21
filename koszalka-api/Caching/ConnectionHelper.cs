using StackExchange.Redis;

namespace koszalka_api.Caching
{
    public class ConnectionHelper
    {
        public static IConfiguration _iConfiguration { get; }

        static ConnectionHelper()
        {
            ConnectionHelper._iConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(ConnectionHelper._iConfiguration["Redis:Host"]);
            });
        }


        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
