using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using TravelWebApp.Domain.Mapping;

namespace TravelWebApp.Repository
{
    public class NhibernateHelper
    {
        private static ISessionFactory _sessionFactory;
       private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    InitializeSessionFactory();
                }

                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(connectionString)
                    .ShowSql())
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<UserMap>())
                .BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
