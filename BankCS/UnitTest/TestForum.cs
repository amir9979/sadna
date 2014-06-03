using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;
using System.Collections.Generic;

namespace UnitTestProject1 
{
    [TestClass]
    public class TestForum
    {
        Forum f = new Forum("forum test");
        Member m;

        [TestMethod]
        public void TestRegister()
        {
            Assert.AreNotEqual(-1, f.Register("yaniv", "aaa", "sadsad@fff.com", "yanivra"));
        }

        [TestMethod]
        public void TestLogin()
        {
            f.Register("yaniv", "aaa", "sadsad@fff.com", "yanivra");
            Assert.IsNotNull(m = f.login("yaniv", "aaa"));
            Assert.IsNull(f.login("yaniva", "aaa"));
            Assert.IsNull(f.login("yaniv", "aasadsada"));
        }

        [TestMethod]
        public void TestAddNewSubForum()
        {
            f.Register("yaniv", "aaa", "sadsad@fff.com", "yanivra");
            Member tempm = new Member("yaniv","aaa","asdas@fff.com","yanivra",f,"Dsfsdf");
            Assert.IsFalse(f.AddNewSubForum("blabla", tempm));
            Member m = f.login("yaniv", "aaa");
            List<SubForum> list = (List<SubForum>)f.getSubForum();
            Assert.AreEqual(0, list.Count);
            //Assert.AreNotEqual(-1,f.Register("yaniv", "aaa", "sadsad@fff.com", "yanivra"));
            //Assert.IsNotNull (m = f.login("yaniv", "aaa"));
            f.AddNewSubForum("blabla", m);

            list = (List<SubForum>)f.getSubForum();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("blabla", list[0].Name);
        }
        [TestMethod]
        public void promoteMemberToAdmin()
        {
            SetUp();
            Member tempm = new Member("yaniv", "aaa", "asdas@fff.com", "yanivra", f, "Dsfsdf");
            Assert.IsFalse(f.promoteMemberToAdmin(tempm));

            
            Assert.IsTrue(f.promoteMemberToAdmin(m));

        }
        [TestMethod]
        public void TestGetMemberByNameAndPass()
        {
            SetUp();
            Assert.IsNull(f.GetMemberByNameAndPass("yaniv","addaa"));
            Assert.IsNull(f.GetMemberByNameAndPass("yanivaa", "aaa"));
            Assert.IsTrue(f.GetMemberByNameAndPass("yaniv", "aaa")==m);
        }


        [TestMethod]
        public void TestpromoteMemberToModerate()
        {
            
            SetUp();
            Member tempm = new Member("yaniv", "aaa", "asdas@fff.com", "yanivra", f, "Dsfsdf");
            Assert.IsFalse(f.promoteMemberToModerate(tempm, f.getSubForum()[0]));
            //SubForum 
            //Assert.IsFalse(f.promoteMemberToModerate(m, new SubForum("Aaa"))); //failed
            Assert.IsTrue(f.promoteMemberToModerate(m, f.getSubForum()[0]));

        }
        
        [TestMethod]
        public void TestDeleteSubForum()
        {
            SetUp();
            Assert.IsFalse(f.DeleteSubForum(new SubForum("Aaa")));

            Assert.IsTrue(f.DeleteSubForum(f.getSubForum()[0]));
            Assert.AreEqual(0, f.getSubForum().Count);

        }


        private void SetUp()
        {
            f = new Forum("forum test");
            f.ChangePolicy(new DefaultPolicy());
            f.Register("yaniv", "aaa", "sadsad@fff.com", "yanivra");
            m = f.login("yaniv", "aaa");
            f.AddNewSubForum("blabla", m);

        }
    }
}
