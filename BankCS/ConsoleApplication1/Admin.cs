using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
public class Admin : MemberState
{
        public virtual  Guid _Id{ get; set; }

    public virtual Forum forum{ get; set; }
	
    public virtual bool AddNewSubForum(String name,Member m){
        return forum.AddNewSubForum(name,m);
    }
	public Admin()
    {}
    public Admin(Forum f)
    {
        this.forum = f;
    }

    public virtual bool DeletePost(Member m, Post p)
    {
        return true;
    }

    public virtual void ChangePolicy(Policy p){
        forum.ChangePolicy(p);
    }
    //public Boolean ChangeConstraint(String s){
   // } 
    //public Boolean ChangeProperties(String font, String size, String color){}
    public virtual void AddNewModerator(Member m,SubForum s) {
        m.ChangeMemberState(new Moderator(s));
    }
    public virtual void setadminated(Forum f)
    {
        this.forum = f;
    }
    public virtual Forum getForum()
    {
        return this.forum;
    }
        public virtual Guid Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

}
}
