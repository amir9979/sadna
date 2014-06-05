using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeForum
{
    public interface bridgeForum
    {
        Boolean init(string super_name, string pass, String mail, String fullname, string DBpath); //uc
        Boolean createForum(string super_name, string pass, string name, int policy);//uc
      //  Boolean changePolicy(string super_name, string pass, string forum, int policy);//uc
        IList<string> anonymousConnect(string forum);//uc
        Int64 register(string forum, string user, string pass, string fullname, string email);//uc
        Boolean memberConnect(string forum, string user, string pass);//uc
        Boolean memberDisConnect(string forum, string user, string pass);//uc
        Boolean createSubForum(string forum, string newName, string subject, string moderator, string user, string pass);//uc
        IList<string> subForumList(string forum);//uc
        IList<string> postsList(string forum, string subForum);
        Boolean addPost(string forum, string sub, string subject, string body, string user, string pass);//uc
        Boolean reply(string forum, string sub, string parentPost, string subject, string body, string user, string pass);//uc
        IList<string> AllPostreplies(string forum, string sub, string parentPost, string user, string pass);

        Boolean addMemberType(string user, string pass, string forum, string type);//uc
        Boolean removeMemberType(string user, string pass, string forum, string type);//uc
        int howManySameType(string user, string pass, string forum, string type);//uc
        Boolean changeMemberToType(string user, string pass, string forum, string member, string type);//uc
        Boolean promoteMemberToAdmin(string user, string pass, string forum, string member);
        Boolean promoteMemberToModerator(string user, string pass, string forum, string member, string subForum);
        Boolean addComplaintToModerator(string user, string pass, string forum, string moderateName, string subForum);//uc
        int ModeratorComplaints(string user, string pass, string forum, string moderateName, string subForum);
        IList<string> subForumModerators(string user, string pass, string forum, string subForum);

        IList<string> subForumPosts(string user, string pass, string forum, string subForum);

        string ForumAdmin(string user, string pass, string forum);


        Boolean mailConfirm(string user, string pass, string forum, Int64 code);//uc
        Boolean deletePost(string user, string pass, string forum, string sub, string subject, string body);
        Boolean deleteSubForum(string user, string pass, string forum, string sub);

        IList<string> forumList();

        bool SPlogin(string superusername, string superpass);  //use case need to implements must call this function first  for make super mannager operations

        void CancelForum(string superusername, string superpass, string forumName);


    }
}
