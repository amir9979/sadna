using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;


namespace ConsoleApplication1
{
    public class IProductRepository
    {
         public void Add<T>(T product)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(product);
                    transaction.Commit();
                }
        }
         public void Update<T>(T product)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(product);
                transaction.Commit();
            }
        }
         public void Remove<T>(T product)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(product);
                    transaction.Commit();
                }
        }
        public Product GetById(Guid productId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<Product>(productId);
        }
        public PolicyInterface GetByPolicyId(Guid productId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<PolicyInterface>(productId);
        }
        public Product GetByName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Product product = session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Product>();
                return product;
            }
        }

        public Forum GetByForumName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Forum product = session
                    .CreateCriteria(typeof(Forum))
                    .Add(Restrictions.Eq("name", name))
                    .UniqueResult<Forum>();
                return product;
            }
        }
        public IList<Forum> allForums()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<Forum> product = session
                    .CreateCriteria<Forum>()
                    .List<Forum>();
                return product;
            }
        }
        public ICollection<Product> GetByCategory(string category)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var products = session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Category", category))
                    .List<Product>();
                return products;
            }
        }

        public Post GetByPostID(Guid p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Post product = session
                    .CreateCriteria(typeof(Post))
                    .Add(Restrictions.Eq("Id", p))
                    .UniqueResult<Post>();
                return product;
            }
        }

        public Member GetMemberById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Member product = session
                    .CreateCriteria(typeof(Member))
                    .Add(Restrictions.Eq("Id", id))
                    .UniqueResult<Member>();
                return product;
            }
        }


        public SubForum GetBySubForumID(Guid p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria cr = session.CreateCriteria(typeof(SubForum));
                ICriteria cr2 = cr.Add(Restrictions.Eq("Id", p));
                    SubForum product = cr2.UniqueResult<SubForum>();
                    return product;
            }
        }


        public User GetByUserID(Guid p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria cr = session.CreateCriteria(typeof(User));
                ICriteria cr2 = cr.Add(Restrictions.Eq("Id", p));
                User product = cr2.UniqueResult<User>();
                return product;
            }
        }

    }
}