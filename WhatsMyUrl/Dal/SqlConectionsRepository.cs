using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WhatsMyUrl.Dal.DTO;
using WhatsMyUrl.Dal.Model;

namespace WhatsMyUrl.Dal
{
    public class SqlConectionsRepository : IConectionsRepository
    {

        private string AliveSqlQuery
        {
            get
            {
                var query = new StringBuilder(";WITH a AS ");
                query.AppendLine("(SELECT MAX(CreatedOn) OVER (PARTITION BY SessionId,HubState) CreatedOn,MAX(HubState) ");
                query.AppendLine("OVER (PARTITION BY SessionId,HubId) HubState ");
                query.AppendLine("FROM dbo.SessionConnections) ");
                query.AppendLine("SELECT SessionId,d.HubId,d.HubState,d.CreatedOn FROM dbo.SessionConnections d ");
                query.AppendLine("INNER JOIN a ON a.CreatedOn = d.CreatedOn AND a.HubState = d.HubState ");
                query.AppendLine("WHERE a.HubState <> 2 ");
                query.AppendLine("GROUP BY SessionId,d.HubId,d.HubState,d.CreatedOn ");
                query.AppendLine("HAVING MAX(a.CreatedOn) = d.CreatedOn");
                return query.ToString();
            }
        }

        private string HistorySqlQuery
        {
            get
            {
                var query = new StringBuilder(" ");
                query.AppendLine("SELECT d.SessionId,d.HubId,d.HubState,d.CreatedOn FROM dbo.SessionConnections d ");
                query.AppendLine("WHERE d.SessionId = @sessionId ");
                return query.ToString();
            }
        }

        public Task<SessionConnection> Event(string sessionId, Guid hubId, HubState state = HubState.Unknown)
        {
            var connection = new SessionConnection(sessionId, hubId, state);
            using (var db = new SessionConnectionContext())
            {
                db.SessionConnections.Add(connection);
                db.SaveChanges();
                connection.SessionUser = db.SessionUsers.FirstOrDefault(x => x.SessionId == sessionId);
            }

            return Task.FromResult(connection);
        }

        public Task<SessionUserRef[]> Alive()
        {
            var bucket = new List<SessionConnection>();
            var result = new List<SessionUserRef>();
            using (var db = new SessionConnectionContext())
            {
                var query = db.Database.SqlQuery<SessionConnection>(AliveSqlQuery);
                bucket.AddRange(query.OrderByDescending(x => x.CreatedOn).ToList());
                foreach (var sessionConnection in bucket)
                {
                    var userName = "N/A";
                    var sesionUser = db.SessionUsers.FirstOrDefault(x => x.SessionId == sessionConnection.SessionId);
                    if (sesionUser != null) userName = sesionUser.UserName;
                    SessionUserRef sessionRef;
                    if (result.Any(x => x.UserName == userName))
                    {
                        sessionRef = result.Find(x => x.UserName == userName);
                        sessionRef.HubIds.Add(sessionConnection.HubId);
                        sessionRef.SessionIds.Add(sessionConnection.SessionId);
                    }
                    else
                    {
                        sessionRef = new SessionUserRef()
                        {
                            UserName = userName,
                            LastActivity = sessionConnection.CreatedOn
                        };
                        sessionRef.HubIds.Add(sessionConnection.HubId);
                        sessionRef.SessionIds.Add(sessionConnection.SessionId);
                        result.Add(sessionRef);
                    }

                }

            }
            return Task.FromResult(result.ToArray());
        }

        public Task<SessionConnection[]> History(string sessionId)
        {
            var result = new List<SessionConnection>();
            using (var db = new SessionConnectionContext())
            {
                var query = db.Database.SqlQuery<SessionConnection>(HistorySqlQuery, new SqlParameter("@sessionId", sessionId));
                result.AddRange(query.OrderByDescending(x => x.CreatedOn).ToList());
            }
            return Task.FromResult(result.ToArray());
        }

        #region Hub Events
        public Task<SessionConnection> OnConnected(string sessionId, Guid connectionId)
        {
            return Event(sessionId, connectionId, HubState.Connected);
        }
        public Task OnReconnected(string sessionId, Guid connectionId)
        {
            return Event(sessionId, connectionId, HubState.Connected);
        }
        public Task<SessionConnection> OnDisconnected(string sessionId, Guid connectionId, bool stopCalled)
        {
            return Event(sessionId, connectionId, HubState.Disconnected);
        }
        #endregion


        public Task<SessionConnection> SetUser(string userName, Guid hubId)
        {
            using (var db = new SessionConnectionContext())
            {
                var query =
                    db.SessionConnections.Where(x => x.HubId == hubId && x.HubState != HubState.Disconnected)
                        .OrderByDescending(x => x.CreatedOn);
                var querySession = query.FirstOrDefault();
                if (querySession != null)
                {
                    var sessionId = querySession.SessionId;
                    var sessionUser = db.SessionUsers.FirstOrDefault(x => x.SessionId == sessionId);
                    if (sessionUser == null)
                    {
                        sessionUser = new SessionUser() { SessionId = sessionId, UserName = userName };
                        db.SessionUsers.Add(sessionUser);
                    }
                    else
                    {
                        sessionUser.UserName = userName;
                    }
                    db.SaveChanges();
                    var connection = Event(sessionId, hubId, HubState.Connected).Result;
                    connection.SessionUser = sessionUser;
                    return Task.FromResult(connection);
                }
                else
                {
                    var connection = default(SessionConnection);
                    return Task.FromResult(connection);
                }
            }
        }





        public Task<SessionGroupMessage> GroupMessage(string sender, string body, params string[] recipients)
        {
            var message = new SessionGroupMessage(sender, body, recipients);
            using (var db = new SessionConnectionContext())
            {
                db.SessionGroupMessages.Add(message);
                db.SaveChanges();
            }
            return Task.FromResult(message);
        }
    }
}