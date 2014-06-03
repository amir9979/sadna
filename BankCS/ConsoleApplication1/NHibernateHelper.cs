using NHibernate;
using NHibernate.Cfg;

namespace ConsoleApplication1
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddFile("complaint.hbm.xml");
                    configuration.AddFile("Forum.hbm.xml");
                    configuration.AddFile("MemberState.hbm.xml");
                    configuration.AddFile("PolicyInterface.hbm.xml");
                    configuration.AddFile("Post.hbm.xml");
                    configuration.AddFile("SubForum.hbm.xml");
                    configuration.AddFile("User.hbm.xml");
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
