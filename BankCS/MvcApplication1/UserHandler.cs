using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using ConsoleApplication1;


namespace MvcApplication1
{
    public class UserHandler :ForumInterface
    {
        public UserInfo _usr { get; private set; }
        ForumSystem _sys;
        public string _id{ get; private set; }
        public string _forum{ get; private set; }

        public string username { get; private set; }


        public UserHandler(ForumSystem sys)
        {
            _usr = null;
            _sys = sys;
            username = null;

        }

        public UserHandler(ForumSystem sys,string id ,string forum)
        {
            _usr = null;
            _sys = sys;
            _id = id;
            _forum = forum;
            username = null;

        }



        public override bool entry(string ForumName)
        {
            _usr =  _sys.entry(ForumName);
            return _usr != null;
        }

        public override bool SetPolicy(int index, string ForumName)
        {
            return _sys.SetPolicy(index, ForumName);
        }

        public override long Registration(string ForumName, string name, string pass, string mail, string fullname)
        {
            return _sys.Registration(ForumName, name, pass, mail, fullname);
        }

        public override bool login(string username, string pass)
        {

            UserInfo temp = _sys.login(username, pass,_usr);
            if(temp==null)
                return false;
            _usr= temp;
            this.username = username;
            return true; 
        }

        public override void loggout()
        {
            _usr = _sys.loggout(_usr);
            this.username = null;
        }

        public override bool AddNewSubForum(string subject, MemberInfo moderator)
        {
            return _sys.AddNewSubForum(_usr, subject, moderator);
        }

        public override IList<SubForumInfo> WatchAllSubForum()
        {
            return _sys.WatchAllSubForumInfo(_usr);
        }

        public override IList<PostInfo> WatchAllThreads(SubForumInfo s)
        {
            return _sys.WatchAllThreads(_usr, s);
        }

        public override IList<PostInfo> WatchAllComments(PostInfo s)
        {
            return _sys.WatchAllComments(_usr, s);
        }

        public override bool PublishNewThread(string msg, SubForumInfo s)
        {
            return _sys.PublishNewThread(_usr, msg, s);
        }

        public override bool PublishCommentPost(string msg, PostInfo p)
        {
            return _sys.PublishCommentPost(_usr, msg, p);
        }

        public override int checkHowMuchMemberType()
        {
            return _sys.checkHowMuchMemberType(_usr);
        }

        public override bool addNewType(string newType)
        {
            return _sys.addNewType(_usr, newType);
        }

        public override bool promoteMemberToAdmin(MemberInfo u)
        {
            return _sys.promoteMemberToAdmin(_usr, u);
        }

        public override bool EmailConfirm(long ConfNumber)
        {
            return _sys.EmailConfirm(ConfNumber,_usr,"");//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        }

        public override bool deleteType(string newType)
        {
            return _sys.deleteType(_usr, newType);
        }

        public override bool deletePost(PostInfo p)
        {
            return _sys.deletePost(_usr, p);
        }

        public override bool SPlogin(string superusername, string superpass)
        {
            return _sys.SPlogin(superusername, superpass);
        }

        public override IList<ForumInfo> WatchAllForums()
        {
            return _sys.WatchAllForums(_usr);
        }

        public override bool BuildForum(string name)
        {
            return _sys.BuildForum(_usr, name);
        }

        public override void CancelForum(ForumInfo f)
        {
            _sys.CancelForum(_usr, f);
        }

        public override ForumInfo GetForumByName(string forum)
        {
            return _sys.GetForumByName(_usr, forum);
        }
    }


}
