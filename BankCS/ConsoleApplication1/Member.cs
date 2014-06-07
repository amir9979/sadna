using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
public class Member : User
{
    public virtual MemberState state{ get; set; }

    public virtual IList<Post> MemberPosts{ get; set; }

    public virtual IList<Member> Friends{ get; set; }
    //                                 added by shimon & idan
    public virtual String username{ get; set; }
    public virtual Password password{ get; set; }
    public virtual String fullname{ get; set; }
    public virtual String mail{ get; set; }
    public virtual String type{ get; set; }

    //                                 added by shimon & idan
	public Member()
    {
	}
	
    public Member(String username, String password, String mail, String fullname, Forum f ,String t) : base(f)
    {
        //                                 added by shimon & idan
        this.username = username;
       // this.password = password;
        this.password = new Password(password,new List<string>(),new List<string>(),DateTime.Now);
        this.fullname = fullname;
        this.mail = mail;
        this.type = t;
        //                                 added by yaniv shimon & idan4


        this.Friends = new List<Member>();
        this.MemberPosts = new List<Post>();
        state = new Normal();
    }

    public virtual Post AddNewPost(Post p, Post father)
    { 
        if( father!=null)
            father.addComment(p);
        this.MemberPosts.Add(p);

        return p;
    }

    public virtual bool delPost(Post p)
    { //no need subforum
        return MemberPosts.Remove(p) || this.Getstate() is Admin;
    }
    public virtual Post AddNewThread(SubForum s, Post p)
    {
        s.AddNewThread(p);
        this.MemberPosts.Add(p);
        return p;


    }

    public virtual void SetNotConfToRegular()
    {
        this.type = "Regular";
    }

    public virtual Member addFriend(Member friend)
    {
        if (friend == this) return null;
        Friends.Add(friend);
        return friend;
    }

    public virtual void ChangeMemberState(MemberState u)
    {
        state = u;
    }

    public virtual bool loggOut()
    {
        return true;   // future optional condition for loggout
    }



    public virtual IList<Post> GetMemberPosts()
    {
        return MemberPosts;
    }

    public virtual IList<Member> GetFriends()
    {
        return Friends;
    }

    public virtual MemberState Getstate()
    {
        return state;
    }

    public virtual String Gettype()
    {
        return type; 
    }
}
}
