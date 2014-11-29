using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WhatsMyUrl.Dal.Model;

namespace WhatsMyUrl.Dal
{
    public class SessionConnectionContext : DbContext
    {
        public SessionConnectionContext()  : base("sql"){ }
        public DbSet<SessionConnection> SessionConnections { get; set; }
        public DbSet<SessionUser> SessionUsers { get; set; }
        public DbSet<SessionGroupMessage> SessionGroupMessages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionConnection>().Ignore(x => x.SessionUser);
        }
    }
}