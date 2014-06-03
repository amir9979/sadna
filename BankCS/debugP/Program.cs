using BridgeForum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace debugP
{
    class Program
    {
        static void Main(string[] args)
        {

            



            bridgeForum forum = new real();
            forum.init("Ferguson", "scottishAccent", "fer@gmsil.com", "alex ferguson", "england.manchster");
            forum.createForum("Ferguson", "scottishAccent", "Manchester United", 5);
            Int64 i = forum.register("Manchester United", "giggsy", "11", "ryan giggs", "giggs@manU.com");
            forum.mailConfirm("giggsy", "11", "Manchester United", i);
            i = forum.register("Manchester United", "vida", "15", "Nemanja Vidić", "vidic@manU.com");
            forum.mailConfirm("vida", "15", "Manchester United", i);
            forum.memberConnect("Manchester United", "Ferguson", "scottishAccent");
            forum.promoteMemberToAdmin("Ferguson", "scottishAccent", "Manchester United", "giggsy");
            forum.memberConnect("Manchester United", "giggsy", "11");

            forum.memberConnect("Manchester United", "vida", "15");
            forum.createSubForum("Manchester United", "Champ19ns", "new record: 19 primer league titles", "vida", "giggsy", "11");
            i = forum.register("Manchester United", "wazza", "10", "wayne rooney", "rooney@manU.com");
            forum.mailConfirm("wazza", "10", "Manchester United", i);
            forum.memberConnect("Manchester United", "wazza", "10");

            forum.addPost("Manchester United", "Champ19ns", "thropy", "i got my 10 thropy", "vida", "15");
            forum.deletePost("wazza", "10", "Manchester United", "Champ19ns", "thropy", "i got my 10 thropy");
           // Assert.AreEqual(forum.postsList("Manchester United", "Champ19ns").Count, 1, 0, "normal member cant remove this post");
            forum.addPost("Manchester United", "Champ19ns", "assists", "i am with 19 asissts", "wazza", "10");
            forum.deletePost("wazza", "10", "Manchester United", "Champ19ns", "assists", "i am with 19 asissts");
            int x = forum.postsList("Manchester United", "Champ19ns").Count;
         //   Assert.AreEqual(x, 1, "normal member remove his post");
            forum.deletePost("giggsy", "11", "Manchester United", "Champ19ns", "thropy", "i got my 10 thropy");
          //  Assert.AreEqual(forum.postsList("Manchester United", "Champ19ns").Count, 0, 0, "giggsy can do everything");
            forum.memberDisConnect("Manchester United", "wazza", "10");
            forum.memberDisConnect("Manchester United", "vida", "15");
            forum.memberDisConnect("Manchester United", "giggsy", "11");
            forum.memberDisConnect("Manchester United", "Ferguson", "scottishAccent");

        }
    }
}
