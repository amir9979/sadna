using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class TestMember
    {

        Forum f;
        Member m;



        [TestMethod]
        public void TestAddNewPost()
        {
            SetUp();
            Post Thread = new Post("aaaaa",m);
            m.AddNewThread(f.getSubForum()[0], Thread);
            Post reply = new Post("dsfdsfds",m);
            m.AddNewPost(reply, Thread);
            Assert.IsTrue(m.GetMemberPosts().Count==2);
            Assert.IsTrue(m.GetMemberPosts()[0] == Thread);
            Assert.IsTrue(m.GetMemberPosts()[1] == reply);
            Assert.IsTrue(Thread.getComments()[0] == reply);
        }
        [TestMethod]
        public void TestdelPost()
        {
            
        }

        [TestMethod]
        public void TestAddNewThread()
        {
            SetUp();
            Post Thread = new Post("aaaaa", m);
            m.AddNewThread(f.getSubForum()[0], Thread);
            Assert.IsTrue(m.GetMemberPosts().Count == 1);
            Assert.IsTrue(m.GetMemberPosts()[0] == Thread);
        }

        [TestMethod]
        public void TestSetNotConfToRegular()
        {
            SetUp();
            m.SetNotConfToRegular();
            Assert.IsTrue(m.Gettype().Equals("Regular"));
        }

        [TestMethod]
        public void TestaddFriend()
        {
            SetUp();
            Assert.IsNull(m.addFriend(m));
            f.Register("yaniv2", "aaa2", "sadsdsfdsad@fff.com", "yanivdsfdsra");
            Member NewM = f.login("yaniv2", "aaa2");
            Assert.IsNotNull(m.addFriend(NewM));
            Assert.IsTrue(m.GetFriends().Count == 1);
            Assert.IsTrue(m.GetFriends()[0] == NewM);
        }

        [TestMethod]
        public void TestChangeMemberState()
        {
            SetUp();
            MemberState state = new Admin(f);
            m.ChangeMemberState(state);
            Assert.IsTrue(m.Getstate() == state);
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
