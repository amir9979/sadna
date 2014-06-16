using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using ConsoleApplication1;


namespace server
{
    public class UserHandler
    {
        UserInfo _usr;
        ForumSystem _sys;
        List<ActionHandler> _actions;

        public delegate object ActionHandler(List<object> args);


        public UserHandler(ForumSystem sys)
        {
            _usr = null;
            _sys = sys;

            int  max = (int)Enum.GetValues(typeof(FuncMsgClient.FuncType)).Cast<FuncMsgClient.FuncType>().Max();
            _actions = Enumerable.Repeat(default(ActionHandler), max+1).ToList();
            

            _actions[(int)FuncMsgClient.FuncType.entry]                     = entry;
            _actions[(int)FuncMsgClient.FuncType.SetPolicy]                 = SetPolicy;
            _actions[(int)FuncMsgClient.FuncType.Registration]              = Registration;
            _actions[(int)FuncMsgClient.FuncType.login]                     = login;
            _actions[(int)FuncMsgClient.FuncType.loggout]                   = loggout;
            _actions[(int)FuncMsgClient.FuncType.AddNewSubForum]            = AddNewSubForum;
            _actions[(int)FuncMsgClient.FuncType.WatchAllSubForum]          = WatchAllSubForum;
            _actions[(int)FuncMsgClient.FuncType.WatchAllThreads]           = WatchAllThreads;
            _actions[(int)FuncMsgClient.FuncType.WatchAllComments]          = WatchAllComments;
            _actions[(int)FuncMsgClient.FuncType.PublishNewThread]          = PublishNewThread;
            _actions[(int)FuncMsgClient.FuncType.PublishCommentPost]        = PublishCommentPost;
            _actions[(int)FuncMsgClient.FuncType.checkHowMuchMemberType]    = checkHowMuchMemberType;
            _actions[(int)FuncMsgClient.FuncType.addNewType]                = addNewType;
            _actions[(int)FuncMsgClient.FuncType.promoteMemberToAdmin]      = promoteMemberToAdmin;
            _actions[(int)FuncMsgClient.FuncType.promoteMemberToModerator]  = promoteMemberToModerator;
            _actions[(int)FuncMsgClient.FuncType.EmailConfirm]              = EmailConfirm;
            _actions[(int)FuncMsgClient.FuncType.deleteType]                = deleteType;
            _actions[(int)FuncMsgClient.FuncType.deletePost]                = deletePost;
            _actions[(int)FuncMsgClient.FuncType.SPlogin]                   = SPlogin;
            _actions[(int)FuncMsgClient.FuncType.WatchAllForums]            = WatchAllForums;
            _actions[(int)FuncMsgClient.FuncType.BuildForum]                = BuildForum;
            _actions[(int)FuncMsgClient.FuncType.CancelForum]               = CancelForum;
            _actions[(int)FuncMsgClient.FuncType.GetForumByName]            = GetForumByName;
            _actions[(int)FuncMsgClient.FuncType.WatchAllMembers]           = WatchAllMembers;
            _actions[(int)FuncMsgClient.FuncType.UpdatePolicyParams]        = UpdatePolicyParams;
            _actions[(int)FuncMsgClient.FuncType.GetPolicyParam]            =  GetPolicyParam;
        }

        public FuncMsgServer processMsg(FuncMsgClient msg)
        {
            try
            {

                object response = _actions[(int)msg.type](msg.args);
                List<object> responseLst = new List<object> { response };
                FuncMsgServer responseMsg = new FuncMsgServer { type = FuncMsgServer.FuncType.Replay, args = responseLst };
                return responseMsg;
            }
            catch
            {
                FuncMsgServer responseMsg = new FuncMsgServer { type = FuncMsgServer.FuncType.ErrorReplay, args = null };
                return responseMsg;
            }

        }

        private object entry(List<object> args) //changed from void to bool  must call this function first to do other usecases!!!!!!
        {
            if (!argCheck<string>(args, 0))
                throw new Exception();
            UserInfo u = _sys.entry(getArgument<string>(args, 0));
            if (u == null)
                return false;
            _usr = u;

            return true ;
        }

        private object SetPolicy(List<object> args)
        {
            if (!argCheck<string>(args, 1) || !argCheck<int>(args, 0))
                throw new Exception();
            return _sys.SetPolicy(getArgument<int>(args, 0), getArgument<string>(args, 1));  ////need to check
        }

        private object UpdatePolicyParams(List<object> args)
        {
            if (!argCheck<ForumInfo>(args,0) || !argCheck<int>(args,1) || !argCheck<int>(args,2) || !argCheck<List<String>>(args,3))
                throw new Exception();

            return _sys.UpdatePolicyParams(_usr, getArgument<ForumInfo>(args, 0), getArgument<int>(args, 1), getArgument<int>(args, 2), getArgument<List<String>>(args, 3));
        }

        private object GetPolicyParam(List<object> args)
        {
            if (!argCheck<ForumInfo>(args, 0))
                throw new Exception();

            return _sys.GetPolicyParam(_usr, getArgument<ForumInfo>(args, 0));
        }


        private object Registration(List<object> args)  // need to fix with no forumname
        {
            if (!argCheck<string>(args, 4) || !argCheck<string>(args, 3) 
                || !argCheck<string>(args, 2) || !argCheck<string>(args, 1) || !argCheck<string>(args, 0))
                throw new Exception();

            return _sys.Registration(getArgument<string>(args, 0),getArgument<string>(args, 1)
                                    ,getArgument<string>(args, 2),getArgument<string>(args, 3)
                                    ,getArgument<string>(args, 4));
        }

        private object login(List<object> args)
        {
            if (!argCheck<string>(args, 1) || !argCheck<string>(args, 0))
                throw new Exception();
            UserInfo u;
            if ((u = _sys.login(getArgument<string>(args, 0), getArgument<string>(args, 1), _usr)) == null)
                return false;
            _usr = u;
            return true;
        }


        private object loggout(List<object> args)
        {
            _usr = _sys.loggout(_usr);
            return null;

        }


        private object AddNewSubForum(List<object> args)
        {
            if (!argCheck<MemberInfo>(args, 1) || !argCheck<string>(args, 0))
                throw new Exception();
            return _sys.AddNewSubForum(_usr, getArgument<string>(args, 0), getArgument<MemberInfo>(args, 1));
        }


        private object WatchAllSubForum(List<object> args)
        {
            return _sys.WatchAllSubForumInfo(_usr);
        }

        

        private object WatchAllMembers(List<object> args)
        {
            if (!argCheck<ForumInfo>(args, 0))
                throw new Exception();
            return _sys.WatchAllMembers(_usr, getArgument<ForumInfo>(args, 0));
        }

        private object WatchAllThreads(List<object> args) //use case need to implements
        {
            if (!argCheck<SubForumInfo>(args, 0))
                throw new Exception();
            return _sys.WatchAllThreads(_usr, getArgument<SubForumInfo>(args, 0));
        }

        private object WatchAllComments(List<object> args) //use case need to implements
        {
            if (!argCheck<PostInfo>(args, 0))
                throw new Exception();
            return _sys.WatchAllComments(_usr, getArgument<PostInfo>(args, 0));
        }


        private object PublishNewThread(List<object> args)
        {
            if (!argCheck<SubForumInfo>(args, 1) || !argCheck<string>(args, 0))
                throw new Exception();

            return _sys.PublishNewThread(_usr, getArgument<string>(args, 0), getArgument<SubForumInfo>(args, 1));
        }

        private object PublishCommentPost(List<object> args)
        {
            if (!argCheck<PostInfo>(args, 1) || !argCheck<string>(args, 0))
                throw new Exception();
            return _sys.PublishCommentPost(_usr, getArgument<string>(args, 0), getArgument<PostInfo>(args, 1));
        }




        private object checkHowMuchMemberType(List<object> args)
        {
            return _sys.checkHowMuchMemberType(_usr);
        }

        private object addNewType(List<object> args)
        {
            if (!argCheck<string>(args, 0))
                throw new Exception();
            return _sys.addNewType(_usr, getArgument<string>(args, 0));
        }

        private object promoteMemberToAdmin(List<object> args)
        {
            if (!argCheck<MemberInfo>(args, 0))
                throw new Exception();
            return _sys.promoteMemberToAdmin(_usr, getArgument<MemberInfo>(args, 0));
        }


        private object promoteMemberToModerator(List<object> args)
        {
            if (!argCheck<MemberInfo>(args, 0) && !argCheck<SubForumInfo>(args, 0))
                throw new Exception();
            return _sys.promoteMemberToModerator(_usr, getArgument<MemberInfo>(args, 0) , getArgument<SubForumInfo>(args, 1));
        }
        private object EmailConfirm(List<object> args)
        {
            if (!argCheck<Int64>(args, 0))
                throw new Exception();
            return _sys.EmailConfirm(getArgument<Int64>(args, 0), _usr, getArgument<string>(args, 1));
        }



        private object deleteType(List<object> args)
        {
            if (!argCheck<string>(args, 0))
                throw new Exception();
            return _sys.deleteType(_usr, getArgument<string>(args, 0));
        }

        //abstract public void ComplaintAboutModerator(Complaint c, MemberInfo moderator); //need to ask



        private object deletePost(List<object> args)
        {
            if (!argCheck<PostInfo>(args, 0))
                throw new Exception();
            return _sys.deletePost(_usr, getArgument<PostInfo>(args, 0));
        }



        //super mannager actions

        private object SPlogin(List<object> args)  //use case need to implements must call this function first  for make super mannager operations
        {
            if (!argCheck<string>(args, 1) || !argCheck<string>(args, 0))
                throw new Exception();
            return _sys.SPlogin(getArgument<string>(args, 0), getArgument<string>(args, 1));
        }

        private object WatchAllForums(List<object> args)
        {

            return _sys.WatchAllForums(_usr);
        }

        private object BuildForum(List<object> args)  //made changes
        {
            if (!argCheck<string>(args, 0))
                throw new Exception();
            return _sys.BuildForum(_usr, getArgument<string>(args, 0));
        }

        private object CancelForum(List<object> args)
        {
            if (!argCheck<ForumInfo>(args, 0))
                throw new Exception();
            _sys.CancelForum(_usr, getArgument<ForumInfo>(args, 0));
            return null;
        }

        private object GetForumByName(List<object> args)
        {
            if (!argCheck<string>(args, 0))
                throw new Exception();
            return _sys.GetForumByName(_usr, getArgument<string>(args, 0));
        }

        private T getArgument<T>(List<object> args, int Index)
        {
            return (T)args[Index];
        }

        private bool argCheck<T>(List<object> args, int Index)
        {
            return args != null && args.Count > Index && (args[Index] == null || args[Index] is T || args==null);
        }

    }
}
