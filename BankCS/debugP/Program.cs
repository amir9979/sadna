using BridgeForum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication1;
using DataTypes;


namespace debugP
{
    class Program
    {


        public static Guid Int2Guid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        static void Main(string[] args)
        {
            bridgeForum system = new real();
            system.init("Ferguson", "scottishAccent", "alex@fer", "fergi", "england.manchster");
            // allowed to perform actions after initialization
            Boolean added = system.createForum("Ferguson", "scottishAccent", "First Forum", 2);
            //Assert.IsTrue(added);

            added = system.createSubForum("First Forum", "First Thread", "SubForumSubject", "", "Ferguson", "scottishAccent");
            //Assert.IsTrue(added);


            

        }
    }
}
