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
            system.SPlogin("Ferguson", "scottishAccent");
            system.createForum("Ferguson", "scottishAccent", "First Forum", 2);
            IList<string> listOfForums = system.forumList();

            system.createForum("Ferguson", "scottishAccent", "Second Forum", 1);
            listOfForums = system.forumList();


            system.createForum("wrongUserName", "scottishAccent", "Third Forum", 2); //wrong user name: should not add forum
            listOfForums = system.forumList();


            // check: super manager member in forumro
            system.createSubForum("First Forum", "First Thread", "SubForumSubject", "", "Ferguson", "scottishAccent");
            system.memberConnect("First Forum", "Ferguson", "scottishAccent");
            Boolean added = system.addPost("First Forum", "First Thread", "Post subject", "Amir!best rommate ever ;)", "Ferguson", "scottishAccent");

            ForumSystemImpl our = ((real)system).OurSystem;
                        Forum f = our.GetForumByName("First Forum");
            Member m = f.GetMemberByNameAndPass("Ferguson", "scottishAccent");
            IList<SubForumInfo> subs = our.WatchAllSubForumInfo(m);
            our.WatchAllThreads(m, subs.ElementAt(0));
            

        }
    }
}
