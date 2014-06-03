using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server;
using DataTypes;
using ConsoleApplication1;

namespace ClientServerTests
{
    public class ForumSystemTest : ForumSystem
    {
        public override User entry(string ForumName)
        {
            if(ForumName=="test1")
                return new Guest();
            return null;
        }

        public override bool SetPolicy(int index, string ForumName)
        {
            if (index == 3 && ForumName == "test1")
                return true;
            return false;
        }

        public override long Registration(string ForumName, string name, string pass, string mail, string fullname)
        {
            if (ForumName == "test1" && name == "test1" && pass == "test1" && mail == "test1" && fullname == "test1")
                return 555;
            return 3;
        }

        public override User login(string username, string pass, User u)
        {
            if (username == "test1" && pass == "test1")
                return new Guest();
            return null;
        }

        public override User loggout(User u)
        {
            return u;
        }

        public override bool AddNewSubForum(User u, string subject,  MemberInfo moderator)
        {
            return subject == "test1" && testMember(moderator,1);
        }

        public override IList< SubForumInfo> WatchAllSubForumInfo(User u)
        {

            return new List<SubForumInfo> { makeTestSubforum(1), makeTestSubforum(2), makeTestSubforum(3)};
        }

        public override List< PostInfo> WatchAllThreads(User u,  SubForumInfo s)
        {
            return new List<PostInfo> { makeTestPost(1), makeTestPost(2), makeTestPost(3) };
        }

        public override List< PostInfo> WatchAllComments(User u,  PostInfo s)
        {
            return new List<PostInfo> { makeTestPost(1), makeTestPost(2), makeTestPost(3) };
        }

        public override bool PublishNewThread(User u, string msg,  SubForumInfo s)
        {
            return msg == "test1" && testSubForum(s, 1);
        }

        public override bool PublishCommentPost(User u, string msg,  PostInfo p)
        {
            return msg == "test1" && testPost(p, 1);
        }

        public override int checkHowMuchMemberType(User u)
        {
            return 1;
        }

        public override bool addNewType(User u, string newType)
        {
            return newType == "test1";
        }

        public override bool promoteMemberToAdmin(User u,  MemberInfo m)
        {
            return testMember(m,1);
        }

        public override bool EmailConfirm(long ConfNumber, User u)
        {
            return ConfNumber == 1;
        }

        public override bool deleteType(User u, string newType)
        {
            return newType == "test1";
        }

        public override bool deletePost(User u,  PostInfo p)
        {
            return testPost(p, 1);
        }

        public override bool SPlogin(string superusername, string superpass)
        {
            return superusername == "test1" && superpass == "test1" ;
        }

        public override List< ForumInfo> WatchAllForums(User u)
        {
            return new List<ForumInfo> { makeTestForum(1), makeTestForum(2), makeTestForum(3) };
        }

        public override bool BuildForum(User u, string name)
        {
            return name == "test1";
        }

        public override void CancelForum(User u,  ForumInfo f)
        {
            return;
        }

        public override  ForumInfo GetForumByName(User u, string forum)
        {
            ForumInfo f = new ForumInfo();
            f.id = 1;
            f.name = forum;
            return f;
        }

        private static bool testMember(MemberInfo memb,int i)
        {
            string str = "test" + i;
            return memb.fullname == str && memb.mail == str && memb.rank == str && memb.type == str && memb.username == str;

        }

        private static bool testSubForum(SubForumInfo sf, int i)
        {
            string str = "test" + i;
            return sf.Name == str && sf.id == i;

        }

        private static bool testForum(ForumInfo f, int i)
        {
            string str = "test" + i;
            return f.id == i && f.name == str;

        }

        private static bool testPost(PostInfo p, int i)
        {
            string str = "test" + i;
            return p.id == i && p.msg == str && testMember(p.owner,i);

        }


        private SubForumInfo makeTestSubforum(int i)
        {
            SubForumInfo f1 = new SubForumInfo();
            f1.Name = "test"+ i;
            f1.id = i;
            return f1;

        }

        private MemberInfo makeTestMember(int i)
        {
            MemberInfo m = new MemberInfo();
            m.fullname = "test"+ i;
            m.mail = "test"+ i;
            m.rank = "test" + i;
            m.type= "test"+ i;
            m.username = "test"+ i;
            m.id = i;
            return m;

        }
        private PostInfo makeTestPost(int i)
        {
            PostInfo p = new PostInfo();
            p.id = i;
            p.msg = "test"+ i;
            p.owner = makeTestMember(i);
            return p;
        }

        private ForumInfo makeTestForum(int i)
        {
            ForumInfo f = new ForumInfo();
            f.id = i;
            f.name = "test" + i;
            return f;

        }


    }
}
