using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using DataTypes;

namespace ConsoleApplication1
{
    public class ForumSystemImpl : ForumSystem
    {
        public SuperManager SuperManager;
        public bool activeSuper;
        public String database;
       // public IList<Forum> AllForum;
        //public IList<PolicyInterface> AllPolicy;
        public IList<String> AllTypesKind;
        public static IProductRepository rep = new IProductRepository();

        public ForumSystemImpl(String username, String pass, String mail, String fullname, String db)
        {
            this.SuperManager = new SuperManager(username, pass, mail, fullname);
            PolicyInterface p = new Policy(10);
            activeSuper = false;
           // this.AllPolicy = new List<PolicyInterface>();
           // this.AllForum = new List<Forum>();
            
            this.database = db;
            load();
            rep.Add<PolicyInterface>(p);
        }

        public static void load()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddFile("complaint.hbm.xml");
            cfg.AddFile("Forum.hbm.xml");
            cfg.AddFile("MemberState.hbm.xml");
            cfg.AddFile("PolicyInterface.hbm.xml");
            cfg.AddFile("Post.hbm.xml");
            cfg.AddFile("SubForum.hbm.xml");
            cfg.AddFile("User.hbm.xml");
            var export = new SchemaExport(cfg);//.Execute(false, true, false);
            export.Drop(false, true);
            export.Create(true, true);

        }
       // public bool BuildForum(String name, String adminname, String adminpass, String adminmail, String fullname, String superusername, String superpass)
        public bool BuildForum(String name, String superusername, String superpass)
        {
            if (this.SuperManager.password.Equals(superpass) && this.SuperManager.username.Equals(superusername))
            {
            /*    for (int i = 0; i < this.AllForum.Count; i++)
                {
                    if (this.AllForum.ElementAt(i).getname().Equals(name))
                        
                } */
                if(rep.GetByForumName(name)!= null)
                    return false;
                Forum f = new Forum(name);
                Member super = new Member(this.SuperManager.username, this.SuperManager.password, this.SuperManager.mail, this.SuperManager.fullname, f, "Gold");
               // Member a = new Member(adminname, adminpass, adminmail, fullname, f, "Silver");
             //   a.ChangeMemberState(new Admin(f));
                super.ChangeMemberState(new Admin(f));
             //   f.getMembers().Add(a);
                f.getMembers().Add(super);

                //this.AllForum.Add(f);
               // rep.Add<User>(super);
                rep.Add<Forum>(f);
                return true;
            }
            else
                return false;
        }


        public override bool BuildForum(User u, string name)
        {
            if(activeSuper){
                return BuildForum(name, SuperManager.username, SuperManager.password);
            }
            return false;
        }

        public override bool SetPolicy(int index, string ForumName)  
        {
            bool OK = false;
            PolicyInterface p = rep.GetByPolicyId(ToGuid(index));
            if (p == null)
            {
                p = new Policy(index);
                rep.Add<PolicyInterface>(p);
            }
            Forum f = rep.GetByForumName(ForumName);
                if (f!=null)
                {
                    f.policy=p;
                    OK = true;
                    rep.Update<Forum>(f);
                } 
            return OK;      
        }
        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public override User entry(string ForumName)
        {

            Forum f = rep.GetByForumName(ForumName);
            if (f != null)
            {
                Guest g= new Guest(f);
                rep.Update<Forum>(f);
                return g;
            } 
            return null;

        }

        public override Int64 Registration(string ForumName, string name, string pass, string mail, string fullname)  // need to fix with no forumname
        {
            Forum f = rep.GetByForumName(ForumName);
            if (f != null)
            {
                Int64 ans= f.Register(name, pass, mail, fullname);
                rep.Update<Forum>(f);
                return ans;
            }
            return -1;

            
        }

        public override User login(String username, String pass, User u)
        {

            if (u is Guest)
            {
                User tmp = ((Guest)u).loggin(u.forum, username, pass);
                rep.Add<User>(tmp);
                rep.Update<Forum>(u.forum);
                if (tmp != null)
                    return tmp;
                else
                {
                    System.Console.Write("cannot login to forum :" + u.forum.getname() + "cause incorrect pass / username");
                    return u;
                }
            }

            else
            {
                System.Console.Write("cannot login to forum :" + u.forum.getname() + "cause its not  guest");
                return u;
            }

        }

        public override User loggout(User u)
        {
            if ((u is Member) && ((Member)u).loggOut())
            {
                u.forum.OnlineMember.Remove(((Member)u));
                //u.forum.
                rep.Update<Forum>(u.forum);
              //  rep.Remove<User>(u);
                return new Guest(u.forum);
            }
            System.Console.Write("cannot loggout to forum :" + u.forum.getname() + "cause its not  logged in");
            return u;
        }

        public  bool AddNewSubForum(User u, String subject, Member moderator)
        {
            if ((u is Member) && ((Member)u).state is Admin)
            {
                bool ans = u.forum.AddNewSubForum(subject, moderator);
                rep.Add<SubForum>(u.forum.GetSubForumByName(subject));
                rep.Update<Forum>(u.forum);
                return ans;
            }
            else
            {
                System.Console.Write("cannot add subforum cause the user is not admin or is not confirmed yet");
                return false;
            }
        }

        public IList<SubForum> WatchAllSubForum(User u)
        {
            if (u != null)
                return u.forum.SubForumList();
            else
                return null;
        }

        public bool PublishNewThread(User u, String msg, SubForum s)
        {
            if (u is Member && !msg.Equals("") && u.forum.getSubForum().Contains(s) && u.forum.policy.CanDoConfirmedOperations(((Member)u)))
            {
                Post p = new Post(msg, ((Member)u));
                ((Member)u).AddNewPost(p,null);
                s.AddNewThread(p);
                rep.Update<SubForum>(s);
                rep.Update<User>(u);
                return true;
            }
            else
            {
                System.Console.Write("cannot add new thread cause the user us not member or empty msg or subforum not found or u is not confirmed");
                return false;
            }
        }

        public bool PublishCommentPost(User u, String msg, Post p)
        {
            if (u is Member && !msg.Equals("") && u.forum.IsContain(p) && u.forum.policy.CanDoConfirmedOperations(((Member)u)))
            {
                Post comm = new Post(msg, ((Member)u));
                ((Member)u).AddNewPost(comm,p);
                rep.Update<User>(u);
                rep.Update<Post>(p);
                return true;
            }
            else
            {
                System.Console.Write("cannot add new comment cause the user us not member or empty msg or this subforum not contain this post or u is not confirmed");
                return false;
            }

        }




        public override int checkHowMuchMemberType(User u)
        {
            if (u is Member && this.SuperManager.password.Equals(((Member)u).password) && this.SuperManager.username.Equals(((Member)u).username))
            {
                return u.forum.AllTypesKind.Count();
            }
            else
            {
                System.Console.Write("not an admin cant check how much types");
                return 0;    //if not admin 
            }
        }

        public override bool addNewType(User u, string newType)
        {
            bool succ = false;
            if (u is Member && this.SuperManager.password.Equals(((Member)u).password) && this.SuperManager.username.Equals(((Member)u).username) && (!u.forum.AllTypesKind.Contains(newType)))
            {
                u.forum.AllTypesKind.Add(newType);
                succ = true;
            }
            else
                System.Console.Write("not an admin or exist type");
            return succ;
        }

        public bool promoteMemberToAdmin(Member u, User SuperManger)
        {
            bool res = false;
            if (SuperManger is Member && this.SuperManager.password.Equals(((Member)SuperManger).password) && this.SuperManager.username.Equals(((Member)SuperManger).username))
            {
                res = u.forum.promoteMemberToAdmin(u);
                rep.Update<User>(u);
                rep.Update<Forum>(u.forum);
            }
            return res;
        }


        public bool promoteMemberToModerator(User u,Member moder, SubForum s)
        {
            bool res = false;
            if (u is Member && ((Member)u).state is Admin && u.forum.SubForumList().Contains(s) && u.forum.Members.Contains(moder))
            {
                res = u.forum.promoteMemberToModerate(moder, s);
                rep.Update<User>(moder);
                rep.Update<Forum>(u.forum);
                rep.Update<SubForum>(s);

            }
            return res;
        }


        public override bool deleteType(User u, string newType)
        {
            bool succ = false;
            if (u is Member && this.SuperManager.password.Equals(((Member)u).password) && this.SuperManager.username.Equals(((Member)u).username))
            {
                succ = (u.forum.AllTypesKind.Remove(newType));
            }
            else
                System.Console.Write("not an admin cant delete type");

            return succ;
        }

        public bool ComplaintAboutModerator(User u, Complaint c, Member moderator)
        {
            bool found = false;
            if (u is Member && (moderator.state is Moderator) && u.forum.getMembers().Contains(moderator))
            {
                for (int i = 0; (i < ((Member)u).MemberPosts.Count()) && !found; i++)
                {
                    found = ((Moderator)moderator.state).subForum.IsContain(((Member)u).MemberPosts.ElementAt(i));
                }

                if (found)
                {
                     ((Moderator)moderator.state).addNewcomplaint(c);
                     return found;
                }
                else
                    System.Console.Write("the user not posted any post in the moderator subfourum ");
            }
            else
                System.Console.Write("buggish in ComplaintAboutModerator");
            return found;
         
        }

        public override bool EmailConfirm(Int64 ConfNumber, User u)
        {
            bool OK = false;
            Int64 acc=0;
            for (int t = 0; t < ((Member)u).username.Length; t++)
            {
                acc = acc + System.Convert.ToInt64(((Member)u).username.ElementAt(t));
            }

            if ((u is Member) && (acc == ConfNumber))
            {
                ((Member)u).SetNotConfToRegular();
                OK = true;
            }
            return OK;
        }

        public bool deletePost(User u, Post p)
        {
            IList<SubForum> subs=u.forum.getSubForum();
            SubForum s=null;
            for (int i = 0; i < subs.Count; i++)
            {
                if (subs[i].GetMyThreads().Contains(p))
                {
                    s = subs[i];
                    break;
                }
            }
            bool isAdmin = false;
            if ((u is Member))
            {
                Member mem = (Member)u;
                isAdmin = mem.Getstate() is Admin;
            }
                if ((u is Member) && (((Member)u).MemberPosts.Contains(p)) || isAdmin) 
                {

                    bool b = ((Member)u).delPost(p) && s.removeThread(p);
                    if (s != null)
                    {
                        rep.Update<SubForum>(s);
                    }
                    rep.Update<User>(p.owner);
                    rep.Update<Forum>(u.forum);
                    //                rep.Remove<Post>(p);
                    return b;
                }
                else
                {
                    System.Console.Write("cant delete post  u isnt a member or not own the delete");
                    return false;
                }
        }

        public void CancelForum(SuperManager superManager, Forum f)
        {
            if ((superManager == this.SuperManager))
            {
                int i = 0;
                List<MemberState> lst = new List<MemberState>();
                for (; i < f.Members.Count; i++)
                {
                    Member m = f.Members.ElementAt<Member>(i);
                    MemberState ms = m.state;
                    if (ms is Admin)
                    {
                        Admin a = (Admin)ms;
                        a.forum = null;
                    }
                    m.Friends = null;
                    lst.Add(ms);
                    m.state = null;
                    rep.Update<User>(m);
                }
                
                i = 0;
                for (; i < f.Members.Count; i++)
                {
                    Member m = f.Members.ElementAt<Member>(i);
                    //rep.Remove<MemberState>(ms);
                    rep.Remove<User>(m);
                }
                f.Members = null;
                rep.Update<Forum>(f);
                i = 0;
                for (; i < lst.Count; i++)
                {
                    rep.Remove<MemberState>(lst.ElementAt(i));
                }
                      i = 0;
                      for (; i < f.SubForum.Count; i++)
                      {
                          SubForum m = f.SubForum.ElementAt<SubForum>(i);
                          rep.Remove<SubForum>(m);
                      }
                      f.Cancel();
                    rep.Remove<Forum>(f);
            }
        }

        public Forum GetForumByName(string forum)
        {
            return rep.GetByForumName(forum);
        }


   //     public bool deleteMyPost(User u, Post p)
   //     {
  //          if ((u is Member) && ((Member)u).MemberPosts.Contains(p))
  //          {
  //              
  //          }
  //      }


        public override void CancelForum(User u,  ForumInfo f)
        {
            if (activeSuper)
            {
                CancelForum(SuperManager, ForumFromInfo(f));
            }
        }
















         public override bool AddNewSubForum(User u, string subject,  MemberInfo moderator)
        {
            return AddNewSubForum(u, Convert.ToString(subject), GetMemberByInfo(moderator));
        }

         public override IList< SubForumInfo> WatchAllSubForumInfo(User u)
        {
            IList<SubForum> all = WatchAllSubForum(u);
            IList< SubForumInfo> ans = new List< SubForumInfo>();
            foreach (SubForum a in all)
            {
                ans.Add(SubForumToInfo(a));
            }
            return ans;
        }

        private  SubForumInfo SubForumToInfo(SubForum f)
        {
           return new  SubForumInfo { Name = f.Name, id = Guid2Int(f.Id) };
        }

         public override List< PostInfo> WatchAllThreads(User u,  SubForumInfo s)
        {
            IList<Post> all = SubForumFromInfo(s).MyThreads;
            List< PostInfo> ans = new List< PostInfo>();
            foreach (Post a in all)
            {
                ans.Add(PostToInfo(a));
            }
            return ans;

        }



        public override List< PostInfo> WatchAllComments(User u,  PostInfo s)
        {
            IList<Post> all = PostFromInfo(s).comments;
            List< PostInfo> ans = new List< PostInfo>();
            foreach (Post a in all)
            {
                ans.Add(PostToInfo(a));
            }
            return ans;
        }

        public override bool PublishNewThread(User u, string msg,  SubForumInfo s)
        {
            return PublishNewThread(u, msg, SubForumFromInfo(s));
        }

        
        public override bool PublishCommentPost(User u, string msg,  PostInfo p)
        {
            return PublishCommentPost(u,msg, PostFromInfo(p));
        }

        public override bool promoteMemberToAdmin(User u,  MemberInfo m)
        {
            return promoteMemberToAdmin( GetMemberByInfo(m),u);
        }

        public override bool deletePost(User u,  PostInfo p)
        {
            return deletePost(u, PostFromInfo(p));
        }

        public bool deleteSubForum(User u, Forum f, SubForum s)
        {
            if (u is Member && (((Member)u).Getstate() is Admin))
            {
                Admin ad= (Admin)((Member)u).Getstate();
                if (ad.getForum() == f)
                {
                    bool ans=f.DeleteSubForum(s);
                    rep.Update<Forum>(f);
                   // rep.Remove<SubForum>(s);
                    return ans;
                }
            }
            return false;
        }

        public override List< ForumInfo> WatchAllForums(User u)
        {
            IList<Forum> all = rep.allForums();
            List< ForumInfo> ans = new List< ForumInfo>();
            foreach(Forum a in all){
                ans.Add(ForumToInfo(a));
            }
            return ans;
        }


        public override  ForumInfo GetForumByName(User u, string forum)
        {
            return ForumToInfo(GetForumByName(forum));  
        }

       
        
        public SuperManager isSuper(User u)
        {
            if (u is Member && this.SuperManager.username.Equals(((Member)u).username) && this.SuperManager.password.Equals(((Member)u).password))
                return SuperManager;
            return null;
        }

        public override bool SPlogin(string superusername, string superpass)
        {
            if (this.SuperManager.username.Equals(superusername) && this.SuperManager.password.Equals(superpass))
                activeSuper = true;
            return activeSuper;

        }






        public static Guid Int2Guid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static int Guid2Int(Guid value)
        {
            byte[] b = value.ToByteArray();
            int bint = BitConverter.ToInt32(b, 0);
            return bint;
        }







        public Forum ForumFromInfo( ForumInfo f)
        {
            return rep.GetByForumName(f.name);
        }
        public Post PostFromInfo( PostInfo f)
        {
            return rep.GetByPostID(Int2Guid(f.id));
        }
        public SubForum SubForumFromInfo( SubForumInfo s)
        {
            return rep.GetBySubForumID(Int2Guid(s.id));
        }
        public  PostInfo PostToInfo(Post f)
        {
            return new  PostInfo { msg = f.msg, id = Guid2Int(f.Id), owner = MemberToInfo(f.owner) };
        }
        public  ForumInfo ForumToInfo(Forum f)
        {
            return new  ForumInfo { name = f.name, id = Guid2Int(f.Id) };
        }
        public Member GetMemberByInfo( MemberInfo m)
        {
            return rep.GetMemberById(Int2Guid(m.id));
        }
        public  MemberInfo MemberToInfo(Member m)
        {
            return new  MemberInfo { username = m.username, id = Guid2Int(m.Id), fullname = m.fullname, mail = m.mail, type = m.type };
        }


    }
}



        

