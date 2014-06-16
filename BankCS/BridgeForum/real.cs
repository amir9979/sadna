using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;
using DataTypes;

namespace BridgeForum
{
    public class real : bridgeForum
    {
        public ForumSystemImpl OurSystem;
        public real()
        {
        }
		
		
		
        public Boolean init(string super_name, string pass, String mail, String fullname, string DBpath)
        {
            this.OurSystem = new ForumSystemImpl(super_name, pass, mail, fullname, DBpath);
            return true;
        } //uc
        public Boolean createForum(string super_name, string pass, string name, int policy)
        {
            this.OurSystem.BuildForum(name, super_name, pass);
            this.OurSystem.SetPolicy(policy, name);
            return true;
        }//uc

        // public Boolean changePolicy(string super_name, string pass, string forum, int policy){ }//uc

        public IList<string> anonymousConnect(string forum)
        {
            IList<string> WatchList = new List<string>();
            IList<SubForumInfo> SubForumList = new List<SubForumInfo>();
            UserInfo us = this.OurSystem.entry(forum);
            SubForumList = this.OurSystem.WatchAllSubForumInfo(us);
            if (SubForumList != null)
            {
                for (int i = 0; i < SubForumList.Count(); i++)
                    WatchList.Add(SubForumList.ElementAt(i).Name);
            }
            return WatchList;
        }// a guest connecting

        public Int64 register(string forum, string user, string pass, string fullname, string email)
        {
            return this.OurSystem.Registration(forum, user, pass, email, fullname);
        }//uc
        public Boolean memberConnect(string forum, string user, string pass)
        {
            UserInfo TestUser=this.OurSystem.entry(forum);
            TestUser = this.OurSystem.login(user, pass, TestUser);
            return (OurSystem.UserFromInfo(TestUser) is Member);
        }//uc
        public Boolean memberDisConnect(string forum, string user, string pass)
        {
            Forum f = OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            OurSystem.loggout(OurSystem.UserToInfo(m));
            return true;

        }//uc

        public Boolean createSubForum(string forum, string newName, string subject, string moderator, string user, string pass)
        {
            bool res=false;
            Forum TempForum=OurSystem.GetForumByName(forum);
            Member TempAdmin;
            Member TempModerator=null;
                //return true;

            TempAdmin = TempForum.GetMemberByNameAndPass(user, pass);
            if ((TempAdmin.state is Admin)  )
            {
                for (int i = 0; i < TempForum.Members.Count(); i++)
                {
                    if (TempForum.Members.ElementAt(i).username.Equals(moderator))
                    {
                        TempModerator = TempForum.Members.ElementAt(i);
                    }
                }
            }
           
            if (TempAdmin.state is Admin){
                res = OurSystem.AddNewSubForum(TempAdmin, newName, TempModerator);//((Admin)TempAdmin.state).AddNewSubForum(newName, TempModerator);
            }
               

            return res;

        }//uc

        public IList<string> subForumList(string forum) 
        {
            return anonymousConnect(forum);
        }//uc
        public IList<string> postsList(string forum, string subForum)
        {
            IList<string> ReturnValue= new List<string>();         
            SubForum TempSubForum=null;
            User u = OurSystem.UserFromInfo(this.OurSystem.entry(forum));
            IList<SubForum> SubForumList = u.SubForumList(u.forum);
            for (int i = 0; i < SubForumList.Count(); i++)
            {
                if (SubForumList.ElementAt(i).Name.Equals(subForum))
                    TempSubForum = SubForumList.ElementAt(i);
            }

            for (int i = 0; i < TempSubForum.MyThreads.Count(); i++)
            {
                ReturnValue.Add(TempSubForum.MyThreads.ElementAt(i).getMsg());
            }

            return ReturnValue;


        } // list of all posts in the subforum
        public Boolean addPost(string forum, string sub, string subject, string body, string user, string pass) 
        {
            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            SubForum subforum = null;
            if (m != null && f.OnlineMember.Contains(m))
            {
                subforum = f.GetSubForumByName(sub);


                return this.OurSystem.PublishNewThread(m, body, subforum);
            }
            return false;

        }//uc
        public Boolean reply(string forum, string sub, string parentPost, string subject, string body, string user, string pass) {
            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            SubForum subforum = null;
            Post p = null;
            if (m != null && f.OnlineMember.Contains(m))
            {
                subforum = f.GetSubForumByName(sub);
                if (subforum != null)
                    p = subforum.GetThreadByBody(parentPost);
            }
            
            if (p!=null)
                    return this.OurSystem.PublishCommentPost(m, body, p);
            return false;
        }//uc
        public IList<string> AllPostreplies(string forum, string sub, string parentPost, string user, string pass) {
            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            SubForum subforum = null;
            List<string> replies = new List<string>() ;
            Post p = null;
            if (m != null && f.OnlineMember.Contains(m))
            {
                subforum = f.GetSubForumByName(sub);
                if (subforum != null)
                    p = subforum.GetThreadByBody(parentPost);
            }

            if (p != null)
            {
                for (int i = 0; i < p.getComments().Count; i++)
                    replies.Add(p.getComments().ElementAt(i).getMsg());
            }
            return replies;

        } //get list of all replies of specific post

        public Boolean addMemberType(string user, string pass, string forum, string type) 
        {
            bool res = false;
            Forum f = this.OurSystem.GetForumByName(forum);
            Member u=f.GetMemberByNameAndPass(user, pass);
            if (!res && f!=null && u!=null &&u.forum.OnlineMember.Contains(u))
            {
               res= this.OurSystem.addNewType(OurSystem.UserToInfo(u), type);
            }
            return res;   
        }//uc
        public Boolean removeMemberType(string user, string pass, string forum, string type)
        {
            bool res = false;
            Forum f = this.OurSystem.GetForumByName(forum);
            Member u = f.GetMemberByNameAndPass(user, pass);
            if (f != null && u != null && u.forum.OnlineMember.Contains(u))
            {
                res = this.OurSystem.deleteType(OurSystem.UserToInfo(u), type);
            }
            return res;
        }//uc
        public int howManySameType(string user, string pass, string forum, string type)
        {
            int res = 0;
            Forum f = this.OurSystem.GetForumByName(forum);
            Member u = f.GetMemberByNameAndPass(user, pass);
            if (f != null && u != null && u.forum.OnlineMember.Contains(u))
            {
                res = this.OurSystem.checkHowMuchMemberType(OurSystem.UserToInfo(u));
            }
            return res;
        }//uc//uc
        public Boolean changeMemberToType(string user, string pass, string forum, string member, string type) { return true; }//uc
        public Boolean promoteMemberToAdmin(string user, string pass, string forum, string member)
        {
            bool res = false;
            Member PromotedMember=null;
            Forum f = this.OurSystem.GetForumByName(forum);
            Member u = f.GetMemberByNameAndPass(user, pass);
            if (f!=null)
            {
                for (int i = 0; i < f.Members.Count(); i++)
                {
                    if (f.Members.ElementAt(i).username.Equals(member))
                         PromotedMember = f.Members.ElementAt(i);
                }
                res = OurSystem.promoteMemberToAdmin(PromotedMember, u);
            }
            return res;   
        }
        public Boolean promoteMemberToModerator(string user, string pass, string forum, string member, string subForum) 
        {
            bool res = false;
            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            SubForum s = f.GetSubForumByName(subForum);
            Member MemberToModerator = null;
            for (int i = 0; i < f.Members.Count; i++)
            {
                if (f.Members.ElementAt(i).username.Equals(member))
                {
                    MemberToModerator = f.Members.ElementAt(i);
                }

            }

            return this.OurSystem.promoteMemberToModerator(m, MemberToModerator, s);


        }
        public Boolean addComplaintToModerator(string user, string pass, string forum, string moderateName, string subForum) {

            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            Member moder =null;
            for (int i = 0; i < f.Members.Count; i++)
            {
                if (f.Members.ElementAt(i).username.Equals(moderateName))
                    moder = f.Members.ElementAt(i);
            }
            return this.OurSystem.ComplaintAboutModerator(m, new Complaint(m, "complaint"),moder);

        }//uc
        public int ModeratorComplaints(string user, string pass, string forum, string moderateName, string subForum) { return 0; } // how much complains a moderator has
        public IList<string> subForumModerators(string user, string pass, string forum, string subForum)
        {
            List<string> ReturnValue = new List<string>();
            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            SubForum s = f.GetSubForumByName(subForum);
            for (int i = 0; i < f.Members.Count; i++)
            {
                if ((f.Members.ElementAt(i).state is Moderator) && ((Moderator)f.Members.ElementAt(i).state).subForum.Name.Equals(subForum))
                {
                    ReturnValue.Add(f.Members.ElementAt(i).username);
                }
            }
            return ReturnValue;
        } // get list of all moderators names

        public IList<string> subForumPosts(string user, string pass, string forum, string subForum)
        {
           List<string> ReturnValue = new List<string>();
           Forum f= this.OurSystem.GetForumByName(forum);
           Member m = f.GetMemberByNameAndPass(user, pass);
           SubForum s = f.GetSubForumByName(subForum);
           for (int i = 0; i < s.MyThreads.Count; i++)
           {
               ReturnValue.Add(s.MyThreads.ElementAt(i).getMsg());
           }
           return ReturnValue;



        }

        public string ForumAdmin(string user, string pass, string forum)
        {
            string ReturnValue;
            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            for (int i = 0; i < f.Members.Count; i++)
            {
                if ((f.Members.ElementAt(i).state is Admin) && !(f.Members.ElementAt(i).username.Equals(this.OurSystem.SuperManager.username)))
                {
                    return f.Members.ElementAt(i).username;
                }
                
            }
        return null;
        } //get the name of the admin


        public Boolean mailConfirm(string user, string pass, string forum, Int64 code) 
        {
            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            return this.OurSystem.EmailConfirm(code,OurSystem.UserToInfo(m),m.username);
        }//uc
        public Boolean deletePost(string user, string pass, string forum, string sub, string subject, string body) {
            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            SubForum s=f.GetSubForumByName(sub);
            Post p = s.GetThreadByBody(body);
            return OurSystem.deletePost(m,p);
        }
        public Boolean deleteSubForum(string user, string pass, string forum, string sub) {
            Forum f = this.OurSystem.GetForumByName(forum);
            Member m = f.GetMemberByNameAndPass(user, pass);
            SubForum s = f.GetSubForumByName(sub);
            return OurSystem.deleteSubForum(m,f,s);
        }
        public IList<string> forumList() 
        {
            IList<ForumInfo> lst = this.OurSystem.WatchAllForums(null);
            IList<string> ReturnValue = new List<string>();
            foreach (ForumInfo a in lst)
            {
                ReturnValue.Add(a.name);
            }

            return ReturnValue;
        } // get list of all forums names


        public bool SPlogin(string superusername, string superpass)
        {
            return this.OurSystem.SPlogin(superusername, superpass);
        }

        public void CancelForum(string superusername, string superpass, string forumName)
        {
            this.OurSystem.CancelForum(null, new ForumInfo{name = forumName});
        }



        public static void main()
        {
            bridgeForum system = new real();
            system.init("Ferguson", "scottishAccent", "alex@fer", "fergi", "england.manchster");
            // allowed to perform actions after initialization
            Boolean added = system.createForum("Ferguson", "scottishAccent", "First Forum", 2);

            added = system.createSubForum("First Forum", "First Thread", "SubForumSubject", "", "Ferguson", "scottishAccent");
        }
    }
}

