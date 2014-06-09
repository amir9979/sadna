using System;
using System.Collections.Generic;
using System.Threading;
using client.Network;
using DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using server;
using DataTypes;

namespace ClientServerTests
{
    [TestClass]
    public class Test
    {

        static ForumConnection forum = null;
        static TpcServer _server;

        [ClassInitialize()]

        public static void setup(TestContext ss)
        {
            _server = new TpcServer(12343, new ForumSystemTest());
            _server.start();
            Thread.Sleep(1000);
            forum = new ForumConnectionImpl(TpcServer.LocalIPAddress().ToString(), 12343);
            forum.connect();
            


        }


        [ClassCleanup()]
        public static void cleanup()
        {
            forum.disconnect();
            _server.close();




        }


        [TestMethod]
        public void TestEntry()  //need to return true only to value "test1"
        {
            Assert.IsTrue(forum.entry("test1"));
            Assert.IsFalse(forum.entry("test2"));
        }


        [TestMethod]
        public void TestSetPolicy() //need to return true only to value 3 "test1"
        {
            Assert.IsTrue(forum.SetPolicy(3,"test1"));
            Assert.IsFalse(forum.SetPolicy(0, "test1"));
            Assert.IsFalse(forum.SetPolicy(3, "test2"));
            Assert.IsFalse(forum.SetPolicy(0, "dvtest1"));
        }

        [TestMethod]
        public void TestRegistration() //need to return 555 only to values "test1" else return 3
        {
            Assert.AreEqual(forum.Registration("test1", "test1", "test1", "test1", "test1"), 555);

            Assert.AreEqual(forum.Registration("bbb", "test1", "test1", "test1", "test1"), 3);
            Assert.AreEqual(forum.Registration("test1", "bbb", "test1", "test1", "test1"), 3);
            Assert.AreEqual(forum.Registration("test1", "test1", "bbb", "test1", "test1"), 3);
            Assert.AreEqual(forum.Registration("test1", "test1", "test1", "bbb", "test1"), 3);
            Assert.AreEqual(forum.Registration("test1", "test1", "test1", "test1", "bbb"), 3);

        }



        [TestMethod]
        public void Testlogin() //need to return true only to values "test1"
        {
            Assert.IsTrue(forum.login("test1", "test1"));

            Assert.IsFalse(forum.login("ddd", "test1"));

            Assert.IsFalse(forum.login("test1", "dsfdsf"));
        }



        [TestMethod]
        public void Testloggout() //test have no exeption
        {
            forum.loggout();
        }



        [TestMethod]
        public void TestAddNewSubForum() //need to return true only to values
        {
            Assert.IsTrue(forum.AddNewSubForum("test1", makeTestMember(1)));

            Assert.IsFalse(forum.AddNewSubForum("test2", makeTestMember(1)));

            Assert.IsFalse(forum.AddNewSubForum("test1", makeTestMember(2)));
        }

        [TestMethod]
        public void TestWatchAllSubForum() //need to return true only to values
        {
            List<ForumInfo> forums = forum.WatchAllForums();
            Assert.AreEqual(3, forums.Count);
            Assert.IsTrue(testForum(forums[0], 1));

            Assert.IsTrue(testForum(forums[1], 2));
            Assert.IsTrue(testForum(forums[2], 3));

        }



        [TestMethod]
        public void TestWatchAllThreads() //need to return true only to values
        {
            List<PostInfo> posts =  forum.WatchAllThreads(makeTestSubforum(1));

            Assert.AreEqual(3, posts.Count);

            Assert.IsTrue(testPost(posts[0], 1));

            Assert.IsTrue(testPost(posts[1], 2));
            Assert.IsTrue(testPost(posts[2], 3));
        }



        [TestMethod]
        public void TestWatchAllComments() //need to return true only to values
        {
            List<PostInfo> posts = forum.WatchAllComments(makeTestPost(1));

            Assert.AreEqual(3, posts.Count);

            Assert.IsTrue(testPost(posts[0], 1));
            Assert.IsTrue(testPost(posts[1], 2));
            Assert.IsTrue(testPost(posts[2], 3));
        }



        [TestMethod]
        public void TestPublishNewThread() //need to return true only to values
        {
            Assert.IsTrue(forum.PublishNewThread("test1", makeTestSubforum(1)));
            Assert.IsFalse(forum.PublishNewThread("test2", makeTestSubforum(1)));
            Assert.IsFalse(forum.PublishNewThread("test1", makeTestSubforum(2)));

        }



        [TestMethod]
        public void TestPublishCommentPost() //need to return true only to values
        {
            Assert.IsTrue(forum.PublishCommentPost("test1", makeTestPost(1)));
            Assert.IsFalse(forum.PublishCommentPost("test1", makeTestPost(2)));
            Assert.IsFalse(forum.PublishCommentPost("test2", makeTestPost(1)));
        }



        [TestMethod]
        public void TestcheckHowMuchMemberType() //need to return true only to values
        {
            Assert.AreEqual(1, forum.checkHowMuchMemberType());
        }



        [TestMethod]
        public void TestaddNewType() //need to return true only to values
        {
            Assert.IsTrue( forum.addNewType("test1"));

            Assert.IsFalse(forum.addNewType("test2"));
        }



        [TestMethod]
        public void TestpromoteMemberToAdmin() //need to return true only to values
        {
            Assert.IsTrue( forum.promoteMemberToAdmin(makeTestMember(1)));

            Assert.IsFalse(forum.promoteMemberToAdmin(makeTestMember(2)));
        }



        [TestMethod]
        public void TestEmailConfirm() //need to return true only to values
        {
            Assert.IsTrue(forum.EmailConfirm(1,""));
            Assert.IsFalse(forum.EmailConfirm(2,""));
        }



        [TestMethod]
        public void TestdeleteType() //need to return true only to values
        {
            Assert.IsTrue(forum.deleteType("test1"));

            Assert.IsFalse(forum.deleteType("test2"));
        }

        [TestMethod]
        public void TestdeletePost() //need to return true only to values
        {
            Assert.IsTrue(forum.deletePost(makeTestPost(1)));

            Assert.IsFalse(forum.deletePost(makeTestPost(3)));
        }

        [TestMethod]
        public void TestSPlogin() //need to return true only to values
        {
            Assert.IsTrue(forum.SPlogin("test1", "test1"));

            Assert.IsFalse(forum.SPlogin("test2", "test1"));

            Assert.IsFalse(forum.SPlogin("test1", "test2"));
        }

        [TestMethod]
        public void TestWatchAllForums() //need to return true only to values
        {
            List<ForumInfo> f = forum.WatchAllForums();

            Assert.AreEqual(3, f.Count);

            Assert.IsTrue(testForum(f[0], 1));
            Assert.IsTrue(testForum(f[1], 2));
            Assert.IsTrue(testForum(f[2], 3));
        }

        [TestMethod]
        public void TestBuildForum() //need to return true only to values
        {
            Assert.IsTrue(forum.BuildForum("test1"));

            Assert.IsFalse(forum.BuildForum("test2"));
        }

        [TestMethod]
        public void TestCancelForum() //need to return true only to values
        {
            forum.CancelForum(makeTestForum(1));
        }

        [TestMethod]
        public void TestGetForumByName() //need to return true only to values
        {
            Assert.IsTrue(testForum(forum.GetForumByName("test1"), 1));
        }


        private static bool testMember(MemberInfo memb, int i)
        {
            string str = "test" + i;
            return memb.fullname == str && memb.mail == str && memb.rank == str && memb.type == str && memb.username == str;

        }

        private static bool testSubForum(SubForumInfo sf, int i)
        {
            string str = "test" + i;
            return sf.Name == str && sf.id.Equals(Int2Guid(i));

        }

        public static Guid Int2Guid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        private static bool testForum(ForumInfo f, int i)
        {
            string str = "test" + i;
            return f.id.Equals(Int2Guid(i)) && f.name == str;

        }

        private static bool testPost(PostInfo p, int i)
        {
            string str = "test" + i;
            return p.id.Equals(Int2Guid(i)) && p.msg == str && testMember(p.owner, i);

        }


        private SubForumInfo makeTestSubforum(int i)
        {
            SubForumInfo f1 = new SubForumInfo();
            f1.Name = "test" + i;
            f1.id = Int2Guid(i);
            return f1;

        }

        private MemberInfo makeTestMember(int i)
        {
            MemberInfo m = new MemberInfo();
            m.fullname = "test" + i;
            m.mail = "test" + i;
            m.rank = "test" + i;
            m.type = "test" + i;
            m.username = "test" + i;
            m.id = Int2Guid(i);
            return m;

        }
        private PostInfo makeTestPost(int i)
        {
            PostInfo p = new PostInfo();
            p.id = Int2Guid(i);
            p.msg = "test" + i;
            p.owner = makeTestMember(i);
            return p;
        }

        private ForumInfo makeTestForum(int i)
        {
            ForumInfo f = new ForumInfo();
            f.id = Int2Guid(i);
            f.name = "test" + i;
            return f;

        }
        


    }
}
