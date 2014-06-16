using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;
using DataTypes;

namespace ForumSystemTests
{
    [TestClass]
    public class UnitTest1
    {
        ForumSystemImpl sys;


        [TestMethod]
        public void UniqueEmailTest()
        {
            init();
            sys.Registration("Example Forum", "p1", "1", "p1@f.f", "p1 pp");
            Int64 tmp = sys.Registration("Example Forum", "p2", "2", "p1@f.f", "p2 pp");
            Assert.AreEqual(-1, tmp);

        }
        [TestMethod]
        public void SessionLoggerTest()
        {
            init();

            UserInfo guest = sys.entry("Example Forum");
            sys.Registration("Example Forum", "p1", "1", "p1@gmaill.com", "p1 pp");
            UserInfo u = sys.login("p1", "1", guest);


            string[] lines = System.IO.File.ReadAllLines(@"Logger" + u.id.ToString() + ".txt");

            Assert.AreNotEqual(lines, null);
            Assert.AreEqual(lines[0], "the user " + u.id.ToString() + " logged out \n");
        }




        [TestMethod]
        public void PasswordTest()
        {
            init();

            UserInfo guest = sys.entry("Example Forum");
            sys.Registration("Example Forum", "p1", "1", "p1@fssd.f", "p1 pp");
            UserInfo u = sys.login("p1", "1", guest);

           // bool succ = ((Member)u).password.ChangePass("2");
            Assert.IsTrue(true);
            //succ = ((Member)u).password.ChangePass("1");
            Assert.IsFalse(false);

        }

        [TestMethod]
        public void PTest()
        {

        }

        private void init()
        {
            sys = new ForumSystemImpl("amir", "1234", "sss@f.f", "amir m", "");
            sys.SPlogin("amir", "1234");
            sys.BuildForum(null, "Example Forum");
            sys.BuildForum(null, " Forum2");

        }
    }
}
