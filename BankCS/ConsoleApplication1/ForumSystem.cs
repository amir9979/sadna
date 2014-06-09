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
        abstract public User entry(string ForumName); //changed from void to bool  must call this function first to do other usecases!!!!!!


        abstract public bool SetPolicy(int index, string ForumName);




        abstract public Int64 Registration(string ForumName, string name, string pass, string mail, string fullname);  // need to fix with no forumname


        abstract public User login(string username, string pass, User u);


        abstract public User loggout(User u);


        abstract public bool AddNewSubForum(User u, string subject, MemberInfo moderator);


        abstract public IList<SubForumInfo> WatchAllSubForumInfo(User u);

        abstract public List<PostInfo> WatchAllThreads(User u, SubForumInfo s); //use case need to implements

        abstract public List<PostInfo> WatchAllComments(User u, PostInfo s); //use case need to implements


        abstract public bool PublishNewThread(User u, string msg, SubForumInfo s);

        abstract public bool PublishCommentPost(User u, string msg, PostInfo p);




        abstract public int checkHowMuchMemberType(User u);

        abstract public bool addNewType(User u, string newType);

        abstract public bool promoteMemberToAdmin(User u, MemberInfo m);


        abstract public bool EmailConfirm(Int64 ConfNumber, User u);



        abstract public bool deleteType(User u, string newType);

        //abstract public void ComplaintAboutModerator(Complaint c, MemberInfo moderator); //need to ask



        abstract public bool deletePost(User u, PostInfo p);



        //super mannager actions

        abstract public bool SPlogin(string superusername, string superpass);  //use case need to implements must call this function first  for make super mannager operations

        abstract public List<ForumInfo> WatchAllForums(User u);

        abstract public bool BuildForum(User u, string name);  //made changes

        abstract public void CancelForum(User u, ForumInfo f);

        abstract public ForumInfo GetForumByName(User u, string forum);

        abstract public List<PostInfo> WatchAllMemberPost(User u, MemberInfo m);

        abstract public int HowManyForums(User u);

        abstract public List<MemberInfo> WatchAllMembers(User _usr, ForumInfo forumInfo);

    }
}
