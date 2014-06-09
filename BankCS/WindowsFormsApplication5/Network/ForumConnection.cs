using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observer;
using DataTypes;

namespace client.Network
{
    public abstract  class ForumConnection : Observable
    {

        //connection funcs

        abstract public void connect();


        abstract public void disconnect();


        abstract public bool isConnected();

        


        //usecases
        abstract public bool entry(string ForumName); //changed from void to bool  must call this function first to do other usecases!!!!!!


        abstract public bool SetPolicy(int index, string ForumName);




        abstract public Int64 Registration(string ForumName, string name, string pass, string mail, string fullname);  // need to fix with no forumname


        abstract public bool login(string username, string pass);
 

        abstract public void loggout();


        abstract public bool AddNewSubForum(string subject, MemberInfo moderator);


        abstract public List<SubForumInfo> WatchAllSubForum();

        abstract public List<PostInfo> WatchAllThreads(SubForumInfo s); //use case need to implements

        abstract public List<PostInfo> WatchAllComments(PostInfo s); //use case need to implements


        abstract public bool PublishNewThread(string msg, SubForumInfo s);

        abstract public bool PublishCommentPost(string msg, PostInfo p);




        abstract public int checkHowMuchMemberType();

        abstract public bool addNewType(string newType);

        abstract public bool promoteMemberToAdmin(MemberInfo u);


        abstract public bool EmailConfirm(Int64 ConfNumber,string username);



        abstract public bool deleteType(string newType);

        //abstract public void ComplaintAboutModerator(Complaint c, MemberInfo moderator); //need to ask

        

        abstract public bool deletePost(PostInfo p);
		
		abstract public List<MemberInfo> WatchAllMembers(ForumInfo forumInfo);




        //super mannager actions

        abstract public bool SPlogin(string superusername, string superpass);  //use case need to implements must call this function first  for make super mannager operations

        abstract public List<ForumInfo> WatchAllForums();

        abstract public bool BuildForum(string name);  //made changes

        abstract public void CancelForum(ForumInfo f);

        abstract public ForumInfo GetForumByName(string forum);
    }
}
