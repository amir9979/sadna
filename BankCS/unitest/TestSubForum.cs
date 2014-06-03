using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class TestSubForum
    {

        public SubForum sf= new SubForum("maccabi");
        public Member m;
        public Forum f;

        [TestMethod]
        public void TestAddNewModerator()
        {
            SetUp();
            Member m = new Member("yaniv", "sdsad", "sadasd@dafdsf.com", "ddd", f, "sadas");
            sf.AddNewModerator(m);
            Assert.IsTrue(sf.GetMyModerators().Count==1);
            Assert.IsTrue(sf.GetMyModerators()[0] == m);
        }


        [TestMethod]
        public void TestAddNewThread()
        {
            SetUp();
            Post p = new Post("aaa", m);
            sf.AddNewThread(p);
            Assert.IsTrue(sf.GetMyThreads().Count == 1);
            Assert.IsTrue(sf.GetMyThreads()[0]==p);
        }

        [TestMethod]
        public void TestIsContain()
        {
            SetUp();
            Post p = new Post("aaa", m);
            sf.AddNewThread(p);
            Post reply = new Post("dddd", m);
            Assert.IsFalse(sf.IsContain(reply));
            p.addComment(reply);
            Assert.IsTrue(sf.IsContain(reply));
        }

        private void SetUp()
        {
            f = new Forum("forum test");
            f.ChangePolicy(new DefaultPolicy());
            f.Register("yaniv", "aaa", "sadsad@fff.com", "yanivra");
            m = f.login("yaniv", "aaa");
            f.AddNewSubForum("blabla", m);
            sf = new SubForum("maccabi");

        }
    }
}
