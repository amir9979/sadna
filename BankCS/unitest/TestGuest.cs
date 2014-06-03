using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class TestGuest
    {
        Forum f;
        Guest g;
        //
        [TestMethod]
        public void Testloggin()
        {
            SetUp();
            Assert.IsNull(g.loggin(f, "yaniv", "ddd"));
            f.Register("yaniv", "aaa", "sadsad@fff.com", "yanivra");
            Member m;
            Assert.IsNull(g.loggin(f, "yanivdd", "aaa"));
            Assert.IsNull(g.loggin(f, "yaniv", "aaffa"));
            Assert.IsNotNull(m = g.loggin(f, "yaniv", "aaa"));
        }


        private void SetUp()
        {
            f = new Forum("forum test");
            f.ChangePolicy(new DefaultPolicy());
            g = new Guest(f);

        }
    }
}
