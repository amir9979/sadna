using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class TestPost
    {
        Post p;
        Forum f;
        Member m;

        
        [TestMethod]
        public void Testedit()
        {
            SetUp();
            p.edit("hi");
            Assert.IsTrue(p.getMsg().Equals("hi"));
            //Member m = new Member
            //Post post = new Post("shalom",

        }

        [TestMethod]
        public void TestaddComment()
        {
            SetUp();
            //p.addComment(
            Post newPost = new Post("shalom gam leha",m);
            m.AddNewPost(newPost, p);
            Assert.AreEqual(1, p.getComments().Count);
            Assert.IsTrue(p.getComments()[0] == newPost);
            Assert.AreEqual(2, m.MemberPosts.Count);
        }

        [TestMethod]
        public void TestdeleteAllSons()
        {
            SetUp();
            /*p.addComment(*/
            Post newPost = new Post("shalom gam leha", m);
            m.AddNewPost(newPost, p);
            p.deleteAllSons();
            Assert.AreEqual(0, m.MemberPosts.Count);

        }
        public void TestisContain()
        {
            SetUp();
            /*p.addComment(*/
            Post newPost = new Post("shalom gam leha", m);
            m.AddNewPost(newPost, p);
            p.deleteAllSons();
            Assert.IsTrue(p.isContain(p));
            Assert.IsTrue(p.isContain(newPost));


        }



        private void SetUp()
        {
            f = new Forum("forum test");
            f.Register("yaniv", "aaa", "sadsad@fff.com", "yanivra");
            m = f.login("yaniv", "aaa");
            f.AddNewSubForum("Games",m);
            p = new Post("shalom",m);
            p = m.AddNewThread(f.getSubForum()[0], p);
        }
    }
}
