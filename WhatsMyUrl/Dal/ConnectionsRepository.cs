using System.Configuration;
using System.Linq;
using System.Web.Services.Description;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using WhatsMyUrl.Dal.Model;
using  MongoDB.Driver.Linq;

namespace WhatsMyUrl.Dal
{
    public class ConnectionsRepository
    {
        private static MongoDatabase _mongoDB;
        static ConnectionsRepository()
        {
            _mongoDB = RetreiveMongohqDb();
        }
        private static MongoDatabase RetreiveMongohqDb()
        {
            MongoClient mongoClient = new MongoClient(
                new MongoUrl(ConfigurationManager.ConnectionStrings
                 ["mongo"].ConnectionString));

            return mongoClient.GetServer().GetDatabase("whatsMyUrl");
        }

        private static MongoCollection<HubConnection> GetHubConnectionCollection()
        {
            return _mongoDB.GetCollection<HubConnection>("hubConnections");
        }



        public static HubConnection Event(string sessionId, string hubId, HubState state = HubState.Unknown)
        {
            var connection = new HubConnection(sessionId, hubId, state);
            var collection = GetHubConnectionCollection();
            collection.Insert(connection);
            return connection;
        }

        private static HubConnection GetLastEventForSession(string sessionId, params HubState[] states)
        {
            var collection = GetHubConnectionCollection();
            var hit =
                collection.AsQueryable()
                    .Where(x => x.SessionId == sessionId && states.Contains(x.HubState))
                    .OrderByDescending(x => x.CreatedOn)
                    .FirstOrDefault();
            return hit;
        }
        private static HubConnection GetLastEventForHub(string hubId, params HubState[] states)
        {
            var collection = GetHubConnectionCollection();
            var hit =
                collection.AsQueryable()
                    .Where(x => x.HubId == hubId && states.Contains(x.HubState))
                    .OrderByDescending(x => x.CreatedOn)
                    .FirstOrDefault();
            return hit;
        }

        public static void OnConnected(string connectionId, string sessionId)
        {
            Event(sessionId, connectionId, HubState.Connected);
        }

        public static void OnReconnected(string connectionId, string sessionId)
        {
            Event(sessionId, connectionId, HubState.Connected);
        }

        public static void OnDisconnected(string connectionId, string sessionId, bool stopCalled)
        {
            Event(sessionId, connectionId, HubState.Disconnected);
        }
    }
   
}