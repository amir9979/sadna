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
using System.IO;
using System.Net.Mail;

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
            cfg.AddFile("Password.hbm.xml");
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
                // send txt to logger wrote by f.Register(name,pass,mail,fullname)
                rep.Update<Forum>(f);
                if (ans!=-1)
                     sendVerificationEmail(mail, ans);
                return ans;
            }
            return -1;

            
        }

        public override User login(String username, String pass, User u)
        {

            if (u is Guest)
            {
                User tmp = ((Guest)u).loggin(u.forum, username, pass);
                if (tmp != null)
                {
                   // rep.Add<User>(tmp);
                    rep.Update<Forum>(u.forum);
                    File.AppendAllText(@"Logger" + u.Id.ToString() + ".txt", "the user " + tmp.Id.ToString() + "loggin at " + DateTime.Now.ToString() + "\n");
                    return tmp;
                }
                else
                {
                    System.Console.Write("cannot login to forum :" + u.forum.getname() + "cause incorrect pass / username");
                    return null;
                }
            }

            else
            {
                System.Console.Write("cannot login to forum :" + u.forum.getname() + "cause its not  guest");
                return null;
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
                File.AppendAllText(@"Logger"+u.Id.ToString()+".txt", "the user " + u.Id.ToString() + "logged out at " + DateTime.Now.ToString() + "\n");
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
            if (u is Member && !msg.Equals("") && ContainId(s.Id,u.forum) && u.forum.policy.CanDoConfirmedOperations(((Member)u)))
            {
                SubForum sub = ContainId_get(s.Id, u.forum);
                bool check=sub.MyThreads.Count>u.forum.policy.minPostsToCheck() && intersection(sub.UsedWords,msg)>u.forum.policy.minWords();
                check = check || sub.MyThreads.Count <= u.forum.policy.minPostsToCheck();
                if ( check){
                    sub.AddWords(msg);

                Post p = new Post(msg, ((Member)u));
                ((Member)u).AddNewPost(p,null);
                sub.AddNewThread(p);
                rep.Add<Post>(p);
                rep.Update<SubForum>(sub);
                rep.Update<User>(u);
                File.AppendAllText(@"Logger" + u.Id.ToString() + ".txt", "the user " + u.Id + "publish new thread id: " + p.Id.ToString() + DateTime.Now.ToString() + "\n");
                return true;
            }
            else
                    return false;

        }
            else
            {
                System.Console.Write("cannot add new thread cause the user us not member or empty msg or subforum not found or u is not confirmed");
                return false;
            }
        }


        public bool ContainId(Guid Id, Forum f)
        {
            for (int i = 0; i < f.SubForum.Count; i++)
            {
                if (f.SubForum.ElementAt(i).Id.Equals(Id))
                    return true;
            }
            return false;
        }

        public SubForum ContainId_get(Guid Id, Forum f)
        {
            for (int i = 0; i < f.SubForum.Count; i++)
            {
                if (f.SubForum.ElementAt(i).Id.Equals(Id))
                    return f.SubForum.ElementAt(i);
            }
            return null;
        }


        public Post ContainPostId(Guid Id, Forum f)
        {
            Post ans=null;
            for (int i = 0; i < f.SubForum.Count; i++)
            {
                SubForum s = f.SubForum.ElementAt(i);
                for (int j = 0; j < s.MyThreads.Count; j++)
                {
                    ans = PostsIds(Id,s.MyThreads.ElementAt(j));
                    if (ans != null)
                    {
                        return ans;
                    }

                }
            }
            return ans;
        }

        public Post PostsIds(Guid Id,Post start)
        {
            Post ans = null;
            if (start.Id.Equals(Id))
            {
                return start;
            }
            for (int i = 0; i < start.comments.Count; i++)
            {
                Post p = start.comments.ElementAt(i);
                ans = PostsIds(Id,p);
                if (ans != null)
                {
                    return ans;
                }

            }
            return ans;
        }

        public int intersection(ICollection<String> words, string msg)
        {
            string[] ssize = msg.Split(null);
            int ans = 0;
            for (int i = 0; i < ssize.Length; i++)
            {
                string s = ssize[i];
                String a = s.ToString();
                if (words.Contains(a))
                    ans++;

            }

            return ans;

        }

        public bool PublishCommentPost(User u, String msg, Post p)
        {
            Post ans=ContainPostId(p.Id,u.forum);
            if (u is Member && !msg.Equals("") && ans!=null && u.forum.policy.CanDoConfirmedOperations(((Member)u)))
            {

                Post comm = new Post(msg, ((Member)u));
                //ans.addComment(comm);
                ((Member)u).AddNewPost(comm,ans);
                rep.Add<Post>(comm);
                rep.Update<User>(u);
                rep.Update<Post>(ans);
                File.AppendAllText(@"Logger" + u.Id.ToString() + ".txt", "the user " + u.Id.ToString() + "publish new comment id : " + comm.Id.ToString() + " to thread/comment id: " + p.Id.ToString() + DateTime.Now.ToString() + "\n");
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
            if (SuperManger is Member && this.SuperManager.password.Equals(((Member)SuperManger).password.pass) && this.SuperManager.username.Equals(((Member)SuperManger).username))
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
                File.AppendAllText(@"Logger" + u.Id.ToString() + ".txt", "the user " + u.Id.ToString() + "premote to Moderate at SubForum :" + s.Id.ToString() + DateTime.Now.ToString() + "\n");
                rep.Update<User>(moder);
                rep.Update<Forum>(u.forum);
                rep.Update<SubForum>(s);

            }
            return res;
        }


        public override bool deleteType(User u, string newType)
        {
            bool succ = false;
            if (u is Member && this.SuperManager.password.Equals(((Member)u).password.pass) && this.SuperManager.username.Equals(((Member)u).username))
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

            if ((u is Member) && (acc == ConfNumber)) // maybe delete this condition...
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

        public override  int HowManyForums(User u){
            if (activeSuper){
                IList<Forum> all = rep.allForums();
                List<ForumInfo> ans = new List<ForumInfo>();
                foreach (Forum a in all)
                {
                    ans.Add(ForumToInfo(a));
                }
                return ans.Count;
            }

            return -1;
                
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
            return new SubForumInfo { Name = f.Name, id = f.Id };
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
            Post p = PostFromInfo(s);
            IList<Post> all = p.comments;
            List< PostInfo> ans = new List< PostInfo>();
            foreach (Post a in all)
            {
                ans.Add(PostToInfo(a));
            }
            return ans;
        }

        public override List<PostInfo> WatchAllMemberPost(User u, MemberInfo m)
        {
            Member mem = GetMemberByInfo(m);
            IList<Post> all = mem.MemberPosts;
            List<PostInfo> ans = new List<PostInfo>();
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
            if (u is Member && this.SuperManager.username.Equals(((Member)u).username) && this.SuperManager.password.Equals(((Member)u).password.pass))
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
            return rep.GetByPostID(f.id);
        }
        public SubForum SubForumFromInfo( SubForumInfo s)
        {
            return rep.GetBySubForumID(s.id);
        }
        public  PostInfo PostToInfo(Post f)
        {
            return new  PostInfo { msg = f.msg, id = f.Id, owner = MemberToInfo(f.owner) };
        }
        public  ForumInfo ForumToInfo(Forum f)
        {
            return new  ForumInfo { name = f.name, id = f.Id };
        }
        public Member GetMemberByInfo( MemberInfo m)
        {
            if (m.id == Int2Guid(-1))
                return null;
            return rep.GetMemberById(m.id);
        }
        public  MemberInfo MemberToInfo(Member m)
        {
            return new  MemberInfo { username = m.username, id = m.Id, fullname = m.fullname, mail = m.mail, type = m.type };
        }

        public void sendVerificationEmail(string email, Int64 randomNumber)
                {
                        SmtpClient client = new SmtpClient ();
                        client.Port = 587;
                        client.Host = "smtp.gmail.com";
                        client.EnableSsl = true;
                        client.Timeout = 10000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential ("workshopforumsystem@gmail.com", "azzam1234");
                        MailMessage mm = new MailMessage("workshopforumsystem@gmail.com",email,"registration code","Wellcome to the forum system. please enter this verification code to complete your registraion :"+randomNumber.ToString());
                        mm.BodyEncoding = UTF8Encoding.UTF8;
                        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        client.Send (mm);
                }

    }
}



        


