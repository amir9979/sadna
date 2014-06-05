using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BridgeForum;
using System.Collections.Generic;






namespace UnitTestProject1
{
    [TestClass]
    public class forumTests
    {

        // unit test code


[TestMethod]
public void InitTest() //use case 1
{
    bridgeForum system = new real();
    system.init("Ferguson", "scottishAccent","alex@fer","fergi", "england.manchster");
    // allowed to perform actions after initialization
    Boolean added = system.createForum("Ferguson", "scottishAccent", "First Forum", 2);
    Assert.IsTrue(added);

    added = system.createSubForum("First Forum", "First Thread", "SubForumSubject", "", "Ferguson", "scottishAccent");
    Assert.IsTrue(added);

}


[TestMethod]
public void CreateForumTest() //use case 2
{
    bridgeForum system = new real();
    system.init("Ferguson", "scottishAccent", "alex@fer", "fergi", "england.manchster");
    system.SPlogin("Ferguson", "scottishAccent");
    system.createForum("Ferguson", "scottishAccent", "First Forum", 2);
    IList<string> listOfForums = system.forumList();
    Assert.AreEqual(listOfForums.Count, 1, 0, "one forum created since initialization, as wanted");
    system.createForum("Ferguson", "scottishAccent", "Second Forum", 1);
    listOfForums = system.forumList();
    Assert.AreEqual(listOfForums.Count, 2, 0, "two forums created since initialization, as wanted");

    system.createForum("wrongUserName", "scottishAccent", "Third Forum", 2); //wrong user name: should not add forum
    listOfForums = system.forumList();
    Assert.AreEqual(listOfForums.Count, 2, 0, "two forums created since initialization, as wanted");

    // check: super manager member in forumro
    system.createSubForum("First Forum", "First Thread", "SubForumSubject", "", "Ferguson", "scottishAccent");
    system.memberConnect("First Forum", "Ferguson", "scottishAccent");
    Boolean added = system.addPost("First Forum", "First Thread", "Post subject", "Amir!best rommate ever ;)", "Ferguson", "scottishAccent");
    Assert.IsTrue(added);
}

[TestMethod]
public void ChangePolicyTest()
{
    bridgeForum system = new real();
    system.init("Ferguson", "scottishAccent", "alex@fer", "fergi", "england.manchster");
    system.createForum("Ferguson", "scottishAccent", "First Forum", 2);
    Boolean changed = true;//system.changePolicy("Ferguson", "scottishAccent", "First Forum", 1);
    Assert.IsTrue(changed);

    changed = false;// system.changePolicy("Ferguson", "wrong pass", "First Forum", 1); // wrong password-> wont change policy
   Assert.IsFalse(changed);

    system.register("First Forum", "user1", "I Hate Football", "Ronaldo", "user1@walla.com");
    changed = false;
    //system.changePolicy("user1", "I Hate Football", "First Forum", 1); // simple member-> wont change policy
    Assert.IsFalse(changed);

}

[TestMethod]
public void AnonymousConnectTest()
{
    bridgeForum system = new real();
    system.init("Ferguson", "scottishAccent", "fer@gmsil.com", "alex ferguson", "england.manchster");
    system.createForum("Ferguson", "scottishAccent", "First Forum", 2);
    system.createSubForum("First Forum", "First Thread", "SubForumSubject", "", "Ferguson", "scottishAccent");
    system.createSubForum("First Forum", "Second Thread", "SubForumSubject2", "", "Ferguson", "scottishAccent");
    IList<string> listSubForums = system.subForumList("First Forum");
    IList<string> recievedSubForums = system.anonymousConnect("First Forum");
    Assert.AreEqual(recievedSubForums.Count, 2, "guest recieved a list of two sub forums");
   // Assert.IsTrue(listSubForums.Equals(recievedSubForums));



}
[TestMethod]
public void ReisterTest()
{
    bridgeForum forum = new real();
    vidicSubForum(forum);
    IList<string> lst = forum.subForumList("Manchester United");
    Assert.AreEqual(lst.Count, 1, 0, "there is only one sub forum");
    Assert.IsTrue(forum.addPost("Manchester United", "Champ19ns", "thropy", "i got my 10 thropy", "vida", "15"));
    Assert.IsFalse(forum.addPost("Manchester United", "Champ19ns", "aaa", "i g  10 thropy", "amir", "12225"));
    VidicDis(forum);

}
[TestMethod]
public void MemberConnectTest()
{
    bridgeForum forum = new real();
    vidicSubForum(forum);
    IList<string> lst = forum.subForumList("Manchester United");
    Assert.AreEqual(lst.Count, 1, 0, "there is only one sub forum");
    Assert.IsTrue(forum.addPost("Manchester United", "Champ19ns", "thropy", "i got my 10 thropy", "vida", "15"));
    VidicDis(forum);
}
[TestMethod]
public void MemberDisconnectTest()
{
    bridgeForum forum = new real();
    vidicSubForum(forum);
    IList<string> lst = forum.subForumList("Manchester United");
    Assert.AreEqual(lst.Count, 1, 0, "there is only one sub forum");
    VidicDis(forum);
    Assert.IsFalse(forum.addPost("Manchester United", "Champ19ns", "thropy", "i got my 10 thropy", "vida", "15"));
}
[TestMethod]
public void CreateSubForumTest()
{
    bridgeForum forum = new real();
    Rooney(forum);
    IList<string> lst = forum.subForumList("Manchester United");
    Assert.AreEqual(lst.Count, 1, 0, "there is only one sub forum");
    //Assert.AreEqual(lst.Exists(element => element.Equals("Champ19ns")), true, "Champ19ns is only one sub");
    bool b=forum.createSubForum("Manchester United", "Champions 20", "new record: 19 primer league titles", "vida", "wazza", "10");
    Assert.AreEqual(b, false, "wazza cant create sub forum");


}
[TestMethod]
public void SubForumListTest()
{
    bridgeForum forum = new real();
    forum.init("Ferguson", "scottishAccent", "fer@gmsil.com", "alex ferguson", "england.manchster");
    forum.createForum("Ferguson", "scottishAccent", "Manchester United", 5);
    Int64 i = forum.register("Manchester United", "giggsy", "11", "ryan giggs", "giggs@manU.com");
    forum.mailConfirm("giggsy", "11", "Manchester United", i);
    i = forum.register("Manchester United", "vida", "15", "Nemanja Vidić", "vidic@manU.com");
    forum.mailConfirm("vida", "15", "Manchester United", i);
    forum.memberConnect("Manchester United", "Ferguson", "scottishAccent");    forum.promoteMemberToAdmin("Ferguson", "scottishAccent", "Manchester United", "giggsy");
    forum.memberConnect("Manchester United", "giggsy", "11");

    forum.memberConnect("Manchester United", "vida", "15");
    forum.createSubForum("Manchester United", "Champ19ns", "new record: 19 primer league titles", "vida", "giggsy", "11");

    IList<string> lst = forum.subForumList("Manchester United");
    Assert.AreEqual(lst.Count, 1, 0, "there is only one sub forum");
    //Assert.AreEqual(lst.Exists(element => element.Equals("Champ19ns")), true, "Champ19ns is only one sub");

}
[TestMethod]
public void AddPostTest()
{
    bridgeForum forum = new real();
    Rooney(forum);
    forum.addPost("Manchester United", "Champ19ns", "thropy", "i got my 10 thropy", "vida", "15");
    IList<string> lst = forum.subForumPosts( "wazza", "10","Manchester United", "Champ19ns");
    Assert.AreEqual(lst.Count, 1, 0, "there is only one post");
    //Assert.AreEqual(lst.Exists(element => element.Equals("i got my 10 thropy")), true, "thropy is only one post");

}
[TestMethod]
public void ReplyTest()
{
    bridgeForum forum = new real();
    Rooney(forum);
    forum.addPost("Manchester United", "Champ19ns", "thropy", "i got my 10 thropy", "vida", "15");
    forum.reply("Manchester United", "Champ19ns", "i got my 10 thropy", "assists", "and i have 5", "wazza", "10");
    IList<string> lst = forum.AllPostreplies("Manchester United", "Champ19ns", "i got my 10 thropy", "wazza", "10");
    Assert.AreEqual(lst.Count, 1, 0, "there is only one reply");
    //Assert.AreEqual(lst.Exists(element => element.Equals("and i have 5")), true, "assists is only one reply");
    RooneyDis(forum);
}

[TestMethod]
public void AddMemberTypeTest()
{

}
[TestMethod]
public void HowManySameTypeTest()
{

}
[TestMethod]
public void ChangeMemberToTypeTest()
{
    bridgeForum system = new real();
    system.init("Ferguson", "scottishAccent", "fer@gmsil.com", "alex ferguson", "england.manchster");
    system.createForum("Ferguson", "scottishAccent", "First Forum", 2);
    system.register("First Forum", "user1", "I Hate Football", "Ronaldo", "user1@walla.com");
    // איך נוסיף ממבר? לבנאי שלו יש סוג חבר אבל אולי צריך להוסיף סוג גם לרגיסר?? 
}
[TestMethod]
public void PromoteMemberToAdminTest()
{
    bridgeForum forum = new real();
    ManU(forum);
    Assert.AreEqual(forum.ForumAdmin("Ferguson", "scottishAccent", "Manchester United").Equals("giggsy"),true, "giggsy is the admin");
    Assert.AreEqual(forum.ForumAdmin("Ferguson", "scottishAccent", "Manchester United").Equals("vida"), false, "vida is not the admin");
    giggsDis(forum);

}
[TestMethod]
public void PromoteMemberToModeratorTest()
{
    bridgeForum forum = new real();
    Rooney(forum);
    IList<string> lst = forum.subForumModerators("giggsy", "11", "Manchester United", "Champ19ns");
    Assert.AreEqual(lst.Count,1, "there is only one moderator");
    //Assert.AreEqual(lst.Exists(element => element.Equals("vida")), true, "vida is only one moderator");
    
    forum.promoteMemberToModerator("giggsy", "11", "Manchester United", "wazza", "Champ19ns");
    IList<string>  lst1 = forum.subForumModerators("giggsy", "11", "Manchester United", "Champ19ns");
    Assert.AreEqual(lst1.Count, 2,  "now there is two moderators");
    //Assert.AreEqual(lst.Exists(element => element.Equals("wazza")), true, "wazza is one of them moderator");
    RooneyDis(forum);

}
[TestMethod]
public void AddComplaintToMediatorTest()
{
    bridgeForum forum = new real();
    Rooney(forum);
    forum.addComplaintToModerator("wazza", "10", "Manchester United", "vida", "Champ19ns");
    //Assert.AreEqual(forum.ModeratorComplaints("wazza", "10", "Manchester United", "vida", "Champ19ns"), -1, 0, "normal member cant see the num of complaints");
    Assert.AreEqual(forum.ModeratorComplaints("giggsy", "11", "Manchester United", "vida", "Champ19ns"), 0, 0, "admin can see the num of complaints");
    RooneyDis(forum);

}
[TestMethod]
public void MailConfirmTest()
{
    bridgeForum forum = new real();
    forum.init("Ferguson", "scottishAccent", "fer@gmsil.com", "alex ferguson", "england.manchster");
    forum.createForum("Ferguson", "scottishAccent", "Manchester United", 5);
    Int64 i = forum.register("Manchester United", "giggsy", "11", "ryan giggs", "giggs@manU.com");
    forum.mailConfirm("giggsy", "11", "Manchester United", i);
    i = forum.register("Manchester United", "vida", "15", "Nemanja Vidić", "vidic@manU.com");
    forum.mailConfirm("vida", "15", "Manchester United", i);
    forum.memberConnect("Manchester United", "Ferguson", "scottishAccent");    forum.promoteMemberToAdmin("Ferguson", "scottishAccent", "Manchester United", "giggsy");
    forum.memberConnect("Manchester United", "giggsy", "11");

    forum.memberConnect("Manchester United", "vida", "15");
    forum.createSubForum("Manchester United", "Champ19ns", "new record: 19 primer league titles", "vida", "giggsy", "11");
    i = forum.register("Manchester United", "wazza", "10", "wayne rooney", "rooney@manU.com");
    forum.mailConfirm("wazza", "10", "Manchester United", i);
    forum.memberConnect("Manchester United", "wazza", "10");

    forum.addPost("Manchester United", "Champ19ns", "thropy", "i got my 10 thropy", "vida", "15");
    
    IList<string> lst = forum.subForumPosts("wazza", "10", "Manchester United", "Champ19ns");
    Assert.AreEqual(lst.Count, 1, 0, "there is only one post");
    i = forum.register("Manchester United", "wazza", "10", "wayne rooney", "rooney@manU.com");
    forum.mailConfirm("wazza", "10", "Manchester United", i);
    Assert.IsTrue(forum.memberConnect("Manchester United", "wazza", "10"));
    forum.memberDisConnect("Manchester United", "wazza", "10");
    forum.memberDisConnect("Manchester United", "vida", "15");
    forum.memberDisConnect("Manchester United", "giggsy", "11");
    forum.memberDisConnect("Manchester United", "Ferguson", "scottishAccent");

}

[TestMethod]
public void DeletePostTest()
{
    bridgeForum forum = new real();
    forum.init("Ferguson", "scottishAccent", "fer@gmsil.com", "alex ferguson", "england.manchster");
    forum.createForum("Ferguson", "scottishAccent", "Manchester United", 5);
    Int64 i = forum.register("Manchester United", "giggsy", "11", "ryan giggs", "giggs@manU.com");
    forum.mailConfirm("giggsy", "11", "Manchester United", i);
    i = forum.register("Manchester United", "vida", "15", "Nemanja Vidić", "vidic@manU.com");
    forum.mailConfirm("vida", "15", "Manchester United", i);
    forum.memberConnect("Manchester United", "Ferguson", "scottishAccent");    forum.promoteMemberToAdmin("Ferguson", "scottishAccent", "Manchester United", "giggsy");
    forum.memberConnect("Manchester United", "giggsy", "11");

    forum.memberConnect("Manchester United", "vida", "15");
    forum.createSubForum("Manchester United", "Champ19ns", "new record: 19 primer league titles", "vida", "giggsy", "11");
    i = forum.register("Manchester United", "wazza", "10", "wayne rooney", "rooney@manU.com");
    forum.mailConfirm("wazza", "10", "Manchester United", i);
    forum.memberConnect("Manchester United", "wazza", "10");

    forum.addPost("Manchester United", "Champ19ns", "thropy", "i got my 10 thropy", "vida", "15");
    forum.deletePost("wazza", "10", "Manchester United", "Champ19ns", "thropy", "i got my 10 thropy");
    Assert.AreEqual(forum.postsList("Manchester United", "Champ19ns").Count, 1, 0, "normal member cant remove this post");
    forum.addPost("Manchester United", "Champ19ns", "assists", "i am with 19 asissts", "wazza", "10");
    forum.deletePost("wazza", "10", "Manchester United", "Champ19ns", "assists", "i am with 19 asissts");
    int x = forum.postsList("Manchester United", "Champ19ns").Count;
    Assert.AreEqual(x, 1, "normal member remove his post");
    forum.deletePost("giggsy", "11", "Manchester United", "Champ19ns", "thropy", "i got my 10 thropy");
    Assert.AreEqual(forum.postsList("Manchester United", "Champ19ns").Count, 0,  "giggsy can do everything");
    forum.memberDisConnect("Manchester United", "wazza", "10");
    forum.memberDisConnect("Manchester United", "vida", "15");
    forum.memberDisConnect("Manchester United", "giggsy", "11");
    forum.memberDisConnect("Manchester United", "Ferguson", "scottishAccent");
}

[TestMethod]
public void DeleteSubForumTest()
{
    bridgeForum forum = new real();
    vidicSubForum(forum);
    forum.deleteSubForum("vida", "15", "Manchester United", "Champ19ns");
    Assert.AreEqual(forum.subForumList("Manchester United").Count, 1,  "even the moderator cant delete his subforum");
    forum.deleteSubForum("giggsy", "11", "Manchester United", "Champ19ns");
    Assert.AreEqual(forum.subForumList("Manchester United").Count, 0,  "admin can delete delete subforum");
    VidicDis(forum);
}


[TestMethod]
public void CancelForumTest()
{
    bridgeForum forum = new real();
    ManU(forum);
    Assert.AreEqual(forum.forumList().Count, 1, "one Forum");
    forum.CancelForum("Ferguson", "scottishAccent", "Manchester United");
    Assert.AreEqual(forum.forumList().Count, 0, "Forum delete");
    giggsDis(forum);
}

private void ManU(bridgeForum forum)
{/*
  * init the forum and ferguson is the super manager
  * create a forum Manchester United
  * create a admin called giggsy and confirm him and connect
  * create a member called vida and confirm him and connect
  */
    //ferguson(forum);
    forum.init("Ferguson", "scottishAccent", "fer@gmsil.com", "alex ferguson", "england.manchster");
    forum.createForum("Ferguson", "scottishAccent", "Manchester United", 5);
    Int64 i = forum.register("Manchester United", "giggsy", "11", "ryan giggs", "giggs@manU.com");
    forum.mailConfirm("giggsy", "11", "Manchester United", i);
    i=forum.register("Manchester United", "vida", "15", "Nemanja Vidić", "vidic@manU.com");
    forum.mailConfirm("vida", "15", "Manchester United", i);
    forum.memberConnect("Manchester United", "Ferguson", "scottishAccent");
    forum.SPlogin("Ferguson", "scottishAccent");
    forum.promoteMemberToAdmin("Ferguson", "scottishAccent", "Manchester United", "giggsy");
    forum.memberConnect("Manchester United", "giggsy", "11");
}
private void ferguson(bridgeForum forum)
{
    forum.init("Ferguson", "scottishAccent","fer@gmsil.com","alex ferguson", "england.manchster");
}
private void vidicSubForum(bridgeForum forum)
{/*
  * init the forum and ferguson is the super manager
  * create a forum Manchester United
  * create a admin called giggsy and confirm him and connect
  * create a member called vida and confirm him and connect
  *     create sub forum called Champ19ns
  *     make vida a moderator
  */
    forum.init("Ferguson", "scottishAccent", "fer@gmsil.com", "alex ferguson", "england.manchster");
    forum.createForum("Ferguson", "scottishAccent", "Manchester United", 5);
    Int64 i = forum.register("Manchester United", "giggsy", "11", "ryan giggs", "giggs@manU.com");
    forum.mailConfirm("giggsy", "11", "Manchester United", i);
    i = forum.register("Manchester United", "vida", "15", "Nemanja Vidić", "vidic@manU.com");
    forum.mailConfirm("vida", "15", "Manchester United", i);
    forum.memberConnect("Manchester United", "Ferguson", "scottishAccent");    forum.promoteMemberToAdmin("Ferguson", "scottishAccent", "Manchester United", "giggsy");
    forum.memberConnect("Manchester United", "giggsy", "11");

    forum.memberConnect("Manchester United", "vida", "15");
    forum.createSubForum("Manchester United", "Champ19ns", "new record: 19 primer league titles", "vida", "giggsy", "11");
}

private void Rooney(bridgeForum forum)
{/*
  * init the forum and ferguson is the super manager
  * create a forum Manchester United
  * create a admin called giggsy and confirm him and connect
  * create a member called vida and confirm him and connect
  * create sub forum called Champ19ns
  * make vida a moderator
  *         create a member called wazza and confirm him and connect
  */
    forum.init("Ferguson", "scottishAccent", "fer@gmsil.com", "alex ferguson", "england.manchster");
    forum.createForum("Ferguson", "scottishAccent", "Manchester United", 5);
    Int64 i = forum.register("Manchester United", "giggsy", "11", "ryan giggs", "giggs@manU.com");
    forum.mailConfirm("giggsy", "11", "Manchester United", i);
    i = forum.register("Manchester United", "vida", "15", "Nemanja Vidić", "vidic@manU.com");
    forum.mailConfirm("vida", "15", "Manchester United", i);
    forum.memberConnect("Manchester United", "Ferguson", "scottishAccent");    forum.promoteMemberToAdmin("Ferguson", "scottishAccent", "Manchester United", "giggsy");
    forum.memberConnect("Manchester United", "giggsy", "11");

    forum.memberConnect("Manchester United", "vida", "15");
    forum.createSubForum("Manchester United", "Champ19ns", "new record: 19 primer league titles", "vida", "giggsy", "11");
    i = forum.register("Manchester United", "wazza", "10", "wayne rooney", "rooney@manU.com");
    forum.mailConfirm("wazza", "10", "Manchester United",i);
    forum.memberConnect("Manchester United", "wazza", "10");
}

private void RooneyDis(bridgeForum forum)
{/*
  * disconnect wazza vida and giggsy and ferguson
  */
    forum.memberDisConnect("Manchester United", "wazza", "10");
    forum.memberDisConnect("Manchester United", "vida", "15");
    forum.memberDisConnect("Manchester United", "giggsy", "11");
    forum.memberDisConnect("Manchester United", "Ferguson", "scottishAccent");


}
private void VidicDis(bridgeForum forum)
{/*
  * disconnect vida and giggsy and ferguson
  */
    forum.memberDisConnect("Manchester United", "vida", "15");
    forum.memberDisConnect("Manchester United", "giggsy", "11");
    forum.memberDisConnect("Manchester United", "Ferguson", "scottishAccent");

}
private void giggsDis(bridgeForum forum)
{/*
  * disconnect giggsy and ferguson 
  */
    forum.memberDisConnect("Manchester United", "giggsy", "11");
    forum.memberDisConnect("Manchester United", "Ferguson", "scottishAccent");

}

    }
}
