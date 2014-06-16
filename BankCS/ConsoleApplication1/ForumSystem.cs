using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;


namespace ConsoleApplication1
{
    public abstract  class ForumSystem
    {

        //usecases
        abstract public UserInfo entry(string ForumName); //changed from void to bool  must call this function first to do other usecases!!!!!!


        abstract public bool SetPolicy(int index, string ForumName);




        abstract public Int64 Registration(string ForumName, string name, string pass, string mail, string fullname);  // need to fix with no forumname


        abstract public UserInfo login(string username, string pass, UserInfo u);


        abstract public UserInfo loggout(UserInfo u);


        abstract public bool AddNewSubForum(UserInfo u, string subject, MemberInfo moderator);


        abstract public IList<SubForumInfo> WatchAllSubForumInfo(UserInfo u);

        abstract public List<PostInfo> WatchAllThreads(UserInfo u, SubForumInfo s); //use case need to implements

        abstract public List<PostInfo> WatchAllComments(UserInfo u, PostInfo s); //use case need to implements


        abstract public bool PublishNewThread(UserInfo u, string msg, SubForumInfo s);

        abstract public bool PublishCommentPost(UserInfo u, string msg, PostInfo p);




        abstract public int checkHowMuchMemberType(UserInfo u);

        abstract public bool addNewType(UserInfo u, string newType);

        abstract public bool promoteMemberToAdmin(UserInfo u, MemberInfo m);

        abstract public bool promoteMemberToModerator(UserInfo u, MemberInfo moder, SubForumInfo s);


        abstract public bool EmailConfirm(Int64 ConfNumber, UserInfo u, string username);



        abstract public bool deleteType(UserInfo u, string newType);

        //abstract public void ComplaintAboutModerator(Complaint c, MemberInfo moderator); //need to ask



        abstract public bool deletePost(UserInfo u, PostInfo p);



        //super mannager actions

        abstract public bool SPlogin(string superusername, string superpass);  //use case need to implements must call this function first  for make super mannager operations

        abstract public List<ForumInfo> WatchAllForums(UserInfo u);

        abstract public bool BuildForum(UserInfo u, string name);  //made changes

        abstract public void CancelForum(UserInfo u, ForumInfo f);

        abstract public ForumInfo GetForumByName(UserInfo u, string forum);

        abstract public List<PostInfo> WatchAllMemberPost(UserInfo u, MemberInfo m);

        abstract public int HowManyForums(UserInfo u);

        abstract public List<MemberInfo> WatchAllMembers(UserInfo _usr, ForumInfo forumInfo);

    }
}
