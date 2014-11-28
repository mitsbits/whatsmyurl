using System.Configuration;
using System.Linq;
using System.Web.Services.Description;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using WhatsMyUrl.Dal.Model;
using  MongoDB.Driver.Linq;

namespace WhatsMyUrl.Dal
{
    public class MongoConnectionsRepository
    {
        private static MongoDatabase _mongoDB;
        static MongoConnectionsRepository()
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

        private static MongoCollection<MongoHubConnection> GetHubConnectionCollection()
        {
            return _mongoDB.GetCollection<MongoHubConnection>("hubConnections");
        }

        public static MongoHubConnection Event(string sessionId, string hubId, HubState state = HubState.Unknown)
        {
            var connection = new MongoHubConnection(sessionId, hubId, state);
            var collection = GetHubConnectionCollection();
            collection.Insert(connection);
            return connection;
        }

        private static MongoHubConnection GetLastEventForSession(string sessionId, params HubState[] states)
        {
            var collection = GetHubConnectionCollection();
            var hit =
                collection.AsQueryable()
                    .Where(x => x.SessionId == sessionId && states.Contains(x.HubState))
                    .OrderByDescending(x => x.CreatedOn)
                    .FirstOrDefault();
            return hit;
        }
        private static MongoHubConnection GetLastEventForHub(string hubId, params HubState[] states)
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