using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;


namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {


            Console.Write("start");
            load();

            IProductRepository rep = new IProductRepository();
            //create
            var p = new Forum("amir");
            //rep.Add(p.policy);
            rep.Add(p);

            Member a = new Member("am","pass","mail","full",p,"sss");
            rep.Add(a);

            //read
            //Product p1 = rep.GetByName("bal");

            //update
            p.AddNewSubForum("sub",a);
            rep.Update(p);

            //delete
            //rep.Remove(p1);

            Console.Write("end");
        }



        public static void load()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddFile("complaint.hbm.xml");
            cfg.AddFile("Forum.hbm.xml");
            cfg.AddFile("MemberState.hbm.xml");
            cfg.AddFile("PolicyInterface.hbm.xml");
            cfg.AddFile("Post.hbm.xml");
            cfg.AddFile("SubForum.hbm.xml");
            cfg.AddFile("User.hbm.xml");
            new SchemaExport(cfg).Execute(false, true, false);
        }
    }
}
