using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{

    [Serializable()]
    public class FuncMsgClient
    {

        public enum FuncType
        {
            Replay,
            ErrorReplay,
            entry,
            SetPolicy ,
            Registration,
            login,
            loggout,
            AddNewSubForum,
            WatchAllSubForum,
            WatchAllThreads,
            WatchAllComments,
            PublishNewThread,
            PublishCommentPost,
            checkHowMuchMemberType,
            addNewType,
            promoteMemberToAdmin,
            EmailConfirm,
            deleteType,
            deletePost,
            SPlogin,
            WatchAllForums,
            BuildForum,
            CancelForum,
            GetForumByName
        };

        public FuncType type;

        public List<Object> args;


    }
}
