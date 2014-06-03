using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class TestAdmin
    {
        Post p;
        Forum f;
        Member m;
        Admin admin;

        private void setUp() 
        {
            f = new Forum("forum test");
           admin = new Admin(f);
            f.Register("gali", "aaa", "sadsad@fff.com", "yanivra");
            m = f.login("gali", "aaa");
             p = new Post("shalom",m);

            
            
        }
        [TestMethod]
        public void TestAddNewSubForum()
        {
            setUp();
            admin.AddNewSubForum("Books", m);
            SubForum s=new SubForum("Books");
            List <SubForum> lst =(List<SubForum>) f.getSubForum();
            Assert.IsTrue(lst[0].Name==s.Name);
            Assert.AreEqual(lst.Count, 1);
        }

         [TestMethod]
         public void TestDeletePost()
        {
            setUp();
            Assert.IsTrue(admin.DeletePost(m,p));
         }

         [TestMethod]
         public void TestChangePolicy()
         {
             setUp();
             Policy newPolicy=new Policy(5);
             if (f.getPolicy() != 5)
             {
                 admin.ChangePolicy(newPolicy);
                 Assert.IsTrue(f.getPolicy() == 5);
             }
             else {
                 admin.ChangePolicy(new Policy(1));
                 Assert.IsTrue(f.getPolicy() ==1);
             }
         }

        
             [TestMethod]
         public void Testsetadminated()
             {
                 setUp();
                 Forum newF = new Forum("second");
                 admin.setadminated(newF);
                 Assert.IsTrue(admin.getForum().getname()=="second");
             }

             [TestMethod]
             public void TestAddNewModerator()
             {
                 setUp();
                 admin.AddNewSubForum("Books", m);
                 admin.AddNewModerator(m,new SubForum("Books"));
                 MemberState state=m.Getstate();
                 Assert.IsTrue(state is Moderator);
             }

    }
}
