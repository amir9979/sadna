using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server;
using ConsoleApplication1;
using DataTypes;
namespace server
{
    public class ForumSystemTest : ForumSystem
    {
        public override UserInfo entry(string ForumName)
        {
            if (ForumName == "test1")
                return new UserInfo{id= (new Guest()).Id};
            return null;
        }

        public override bool SetPolicy(int index, string ForumName)
        {
            if (index == 3 && ForumName == "test1")
                return true;
            return false;
        }

        public override Int64 Registration(string ForumName, string name, string pass, string mail, string fullname)
        {
            if (ForumName == "test1" && name == "test1" && pass == "test1" && mail == "test1" && fullname == "test1")
                return 555;
            return 3;
        }

        public override UserInfo login(string username, string pass, UserInfo u)
        {
            if (username == "test1" && pass == "test1")
                return new UserInfo { id = (new Guest()).Id };
            return null;
        }

        public override UserInfo loggout(UserInfo u)
        {
            return u;
        }

        public override bool AddNewSubForum(UserInfo u, string subject, MemberInfo moderator)
        {
            return subject == "test1" && testMember(moderator, 1);
        }

        public override IList<SubForumInfo> WatchAllSubForumInfo(UserInfo u)
        {

            return new List<SubForumInfo> { makeTestSubforum(1), makeTestSubforum(2), makeTestSubforum(3) };
        }

        public override List<PostInfo> WatchAllThreads(UserInfo u, SubForumInfo s)
        {
            return new List<PostInfo> { makeTestPost(1), makeTestPost(2), makeTestPost(3) };
        }

        public override List<PostInfo> WatchAllComments(UserInfo u, PostInfo s)
        {
            return new List<PostInfo> { makeTestPost(1), makeTestPost(2), makeTestPost(3) };
        }

        public override bool PublishNewThread(UserInfo u, string msg, SubForumInfo s)
        {
            return msg == "test1" && testSubForum(s, 1);
        }

        public override bool PublishCommentPost(UserInfo u, string msg, PostInfo p)
        {
            return msg == "test1" && testPost(p, 1);
        }

        public override int checkHowMuchMemberType(UserInfo u)
        {
            return 1;
        }

        public override bool addNewType(UserInfo u, string newType)
        {
            return newType == "test1";
        }

        public override bool promoteMemberToAdmin(UserInfo u, MemberInfo m)
        {
            return testMember(m, 1);
        }

        public override bool EmailConfirm(Int64 ConfNumber, UserInfo u, string username)
        {
            return ConfNumber == 1;
        }

        public override bool deleteType(UserInfo u, string newType)
        {
            return newType == "test1";
        }

        public override bool deletePost(UserInfo u, PostInfo p)
        {
            return testPost(p, 1);
        }

        public override bool SPlogin(string superusername, string superpass)
        {
            return superusername == "test1" && superpass == "test1";
        }

        public override List<ForumInfo> WatchAllForums(UserInfo u)
        {
            return new List<ForumInfo> { makeTestForum(1), makeTestForum(2), makeTestForum(3) };
        }

        public override bool BuildForum(UserInfo u, string name)
        {
            return name == "test1";
        }

        public override void CancelForum(UserInfo u, ForumInfo f)
        {
            return;
        }

        public override ForumInfo GetForumByName(UserInfo u, string forum)
        {
            ForumInfo f = new ForumInfo();
            f.id = Int2Guid(1);
            f.name = forum;
            return f;
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


        public override bool promoteMemberToModerator(UserInfo u, MemberInfo moder, SubForumInfo s)
        {
            return false;
        }
        public override List<PostInfo> WatchAllMemberPost(UserInfo u, MemberInfo m)
        {

            List<PostInfo> ans = new List<PostInfo>();
            return ans;

        }

        public override int HowManyForums(UserInfo u)
        {

            return 0;
        }

        public override List<MemberInfo> WatchAllMembers(UserInfo _usr, ForumInfo forumInfo)
        {
            return null;
        }

        public override bool UpdatePolicyParams(User u, ForumInfo f, int minword, int maxmont, List<String> legg)
        {
            return true ;
        }
    }
}
