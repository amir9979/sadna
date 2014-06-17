using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;

namespace ConsoleApplication1
{
    public class Forum
    {
        public virtual Guid Id { get; set; }
        public virtual IList<SubForum> SubForum { get; set; }
        public virtual IList<Member> Members { get; set; }
        public virtual PolicyInterface policy { get; set; }
        public virtual string name { get; set; }
        public virtual IList<String> AllTypesKind { get; set; }
        public virtual IList<Member> OnlineMember { get; set; }

        public Forum()
            : this("")
        {
        }
        public Forum(string name)
        {
            this.OnlineMember = new List<Member>();
            this.SubForum = new List<SubForum>();
            this.Members = new List<Member>();
            this.name = name;
            this.AllTypesKind = new List<String>();
            this.AllTypesKind.Add("Gold");
            this.AllTypesKind.Add("Silver");
            this.AllTypesKind.Add("Regular");
            this.policy = new Policy(1);
        }
        public virtual bool promoteMemberToAdmin(Member m)
        {

            if (this.Members.Contains(m))
                if (this.policy.CanBeAdmin(m))
                {
                    m.ChangeMemberState(new Admin(this));


                    return true;
                }
            return false;

        }

        public virtual SubForum GetSubForumByName(String name)
        {
            for (int i = 0; i < SubForum.Count; i++)
            {
                if (SubForum.ElementAt(i).Name.Equals(name))
                    return SubForum.ElementAt(i);
            }
            return null;
        }


        public virtual Member GetMemberByNameAndPass(string user, string pass)
        {

            for (int i = 0; i < this.Members.Count(); i++)
            {
                if ((this.Members.ElementAt(i).username.Equals(user)) && (this.Members.ElementAt(i).password.pass.Equals(pass)))
                    return this.Members.ElementAt(i);
            }
            return null;
        }

        public virtual bool promoteMemberToModerate(Member m, SubForum b)
        {
            if (this.Members.Contains(m) && this.policy.CanBeModerate(m, b))
            {
                m.ChangeMemberState(new Moderator(b));


                return true;
            }
            return false;
        }

        public virtual Int64 Register(String username, String pass, String mail, String fullname)
        {
            for (int i = 0; i < this.Members.Count; i++)
            {

                if (this.Members.ElementAt(i).username.Equals(username) || this.Members.ElementAt(i).mail.Equals(mail))
                    return -1;
            }
            Int64 acc = 0;
            for (int t = 0; t < username.Length; t++)
                acc = acc + System.Convert.ToInt64(username.ElementAt(t));

            Member tmp = new Member(username, pass, mail, fullname, this, "Confirmed");
            Members.Add(tmp);
            return acc;
        }

        public virtual bool AddNewSubForum(String subject, Member m)
        {
            if (m != null && !this.Members.Contains(m))
                return false;                             // the wanna be moderator is not register
            for (int i = 0; i < this.SubForum.Count; i++)
            {
                if (this.SubForum.ElementAt(i).Name.Equals(subject))
                    return false;

            }
            SubForum s = new SubForum(subject);
            this.SubForum.Add(s);  // new sub forum include subject and moderator

            if (m != null)
                return this.promoteMemberToModerate(m, s);
            else
                return true;

        }

        public virtual IList<SubForum> SubForumList()
        {
            return this.SubForum;
        }

        public virtual Member login(String username, String pass)
        {
            for (int i = 0; i < this.Members.Count; i++)
            {
                if (this.Members.ElementAt(i).username.Equals(username))
                    if (this.Members.ElementAt(i).password.pass.Equals(pass))
                    {
                        Password p = this.Members.ElementAt(i).password;
                        int max = this.policy.MaxMonth;
                        if (p.IsValidTime(max))
                        {
                            if (!this.OnlineMember.Contains(Members.ElementAt(i)))
                            {
                                this.OnlineMember.Add(Members.ElementAt(i));
                            }
                            return this.Members.ElementAt(i);
                        }
                        else
                            return null; // need to send message about no valid pass expiration!!!!!
                    }
                   else
                      return null; // for incorrect pass
            }
            return null; // for no username feet
        }

        public virtual bool DeleteSubForum(SubForum b)
        {
            if (this.SubForum.Contains(b))
            {
                for (int t = 0; t < b.MyThreads.Count; t++)
                {
                    b.MyThreads.ElementAt(t).deleteAllSons();

                }
                return this.SubForum.Remove(b);
            }
            return false;
        }
        public virtual void ChangePolicy(PolicyInterface p)
        {
            this.policy = p;
        }
        public virtual string getname()
        {
            return this.name;
        }
        public virtual IList<Member> getMembers()
        {
            return this.Members;
        }

        public virtual IList<SubForum> getSubForum()
        {
            return this.SubForum;
        }

        public virtual bool IsContain(Post p)
        {
            bool found = false;
            for (int i = 0; i < SubForum.Count && !found; i++)
            {
                found = SubForum.ElementAt(i).IsContain(p);
            }
            return found;
        }

        public virtual void Cancel()
        {

            this.Members = null;
        }

        public virtual int getPolicy()
        {
            return this.policy.getPolicyNumber();
        }
    }
}
