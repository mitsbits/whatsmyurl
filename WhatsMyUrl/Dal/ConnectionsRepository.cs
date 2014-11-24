using System.Configuration;
using System.Linq;
using MongoDB.Driver;
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

        public static int CheckSessionForConncetions(string sessionId)
        {
            var collection = GetHubConnectionCollection();
            var activeConnectionCount = collection.AsQueryable().Count(x => x.SessionId == sessionId);
            return activeConnectionCount;
        }
    }
   
}