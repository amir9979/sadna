using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConsoleApplication1;
using DataTypes;

namespace test
{
    public class ForumSystemImp : ForumSystem
    {
        string forum = null;

        NTree<PostInfo> tree;
        int postnum;
        MemberInfo memb;

        public ForumSystemImp()
        {
            tree = new NTree<PostInfo>(null);
            memb = new MemberInfo{id = Int2Guid(2),username = "yaniv"};
            postnum = 0;
            
            for (int i = 0; i < 10; i++)
            {
                tree.addChild(new PostInfo { id = Int2Guid(postnum), msg = "post" + postnum++, owner = memb });
                for (int j = 0; j < 10; j++)
                {
                    tree.getChild(i).addChild(new PostInfo { id = Int2Guid(postnum), msg = "post" + postnum++, owner = memb });
                }
            }
        }

        public override UserInfo entry(string ForumName)
        {
            forum = ForumName;
            return new UserInfo { id = (new Guest()).Id };
        }

        public override bool UpdatePolicyParams(UserInfo u, ForumInfo f, int minword, int maxmont, List<String> legg)
        {
            
            return true;
        }

        public override bool SetPolicy(int index, string ForumName)
        {
            throw new NotImplementedException();
        }

        private static Guid Int2Guid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public override long Registration(string ForumName, string name, string pass, string mail, string fullname)
        {
            throw new NotImplementedException();
        }

        public override UserInfo login(string username, string pass, UserInfo u)
        {
            if ((username == ("login1" + forum) || username == ("login2" + forum)) && pass == "test")
                return new UserInfo { id = (new Guest()).Id };
            return null;
        }

        public override UserInfo loggout(UserInfo u)
        {
            return new UserInfo { id = (new Guest()).Id };
        }

        public override bool AddNewSubForum(UserInfo u, string subject, MemberInfo moderator)
        {
            throw new NotImplementedException();
        }

        public override IList<SubForumInfo> WatchAllSubForumInfo(UserInfo u)
        {
            if (forum == "testforum1")
            {
                return new List<SubForumInfo> { new SubForumInfo{id = Int2Guid(1),Name = "testforum1SubForum1"}
                                        ,  new SubForumInfo{id = Int2Guid(1),Name = "testforum1SubForum2"}
                                        };
            }
            if (forum == "testforum2")
            {
                return new List<SubForumInfo> { new SubForumInfo{id = Int2Guid(1),Name = "testforum2SubForum1"}
                                        ,  new SubForumInfo{id = Int2Guid(1),Name = "testforum2SubForum2"}
                };
            }
            return null;

        }

        public override List<PostInfo> WatchAllThreads(UserInfo u, SubForumInfo s)
        {
            List<PostInfo> result = new List<PostInfo>();
            for (int i = 0; i < tree.childsCount(); i++)
            {
                result.Add(tree.getChild(i).getData());
            }
            return result;
        }

        public override List<PostInfo> WatchAllComments(UserInfo u, PostInfo s)
        {
            NTree<PostInfo> node = null;
            List<PostInfo> result = new List<PostInfo>();
            tree.traverse(delegate(NTree<PostInfo> curnode)
            {
                if (curnode.getData() != null && curnode.getData().id == s.id) node = curnode; 
            });
            if (node == null) return null;
            for (int i = 0; i < node.childsCount(); i++)
            {
                result.Add(node.getChild(i).getData());
            }
            return result;
        }

        public override bool PublishNewThread(UserInfo u, string msg, SubForumInfo s)

        {
            if (msg == "") return false;
            tree.addChild(new PostInfo { id = Int2Guid(postnum++), msg = msg, owner = memb });
            return true;
        }

        public override bool PublishCommentPost(UserInfo u, string msg, PostInfo p)
        {
            NTree<PostInfo> node = null;
            List<PostInfo> result = new List<PostInfo>();
            if (msg == "") return false;
            tree.traverse(delegate(NTree<PostInfo> curnode)
            {
                if (curnode.getData() != null && curnode.getData().id == p.id) node = curnode;
            });
            if (node == null) return false; ;
            node.addChild(new PostInfo { id = Int2Guid(postnum++), msg = msg, owner = memb });
            return true;
        }

        public override int checkHowMuchMemberType(UserInfo u)
        {
            throw new NotImplementedException();
        }

        public override bool addNewType(UserInfo u, string newType)
        {
            throw new NotImplementedException();
        }

        public override bool promoteMemberToAdmin(UserInfo u, MemberInfo m)
        {
            throw new NotImplementedException();
        }

        /*public override bool EmailConfirm(long ConfNumber, UserInfo u)
        {
            throw new NotImplementedException();
        }*/

        public override bool deleteType(UserInfo u, string newType)
        {
            throw new NotImplementedException();
        }

        public override bool deletePost(UserInfo u, PostInfo p)
        {
            throw new NotImplementedException();
        }

        public override bool SPlogin(string superusername, string superpass)
        {
            throw new NotImplementedException();
        }

        public override List<ForumInfo> WatchAllForums(UserInfo u)
        {
            return new List<ForumInfo> { new ForumInfo{id = Int2Guid(1),name = "testforum1"}
                                        ,  new ForumInfo{id = Int2Guid(1),name = "testforum2"}
                                        };
        }

        public override bool BuildForum(UserInfo u, string name)
        {
            throw new NotImplementedException();
        }

        public override void CancelForum(UserInfo u, ForumInfo f)
        {
            throw new NotImplementedException();
        }

        public override ForumInfo GetForumByName(UserInfo u, string forum)
        {
            throw new NotImplementedException();
        }

        delegate void TreeVisitor<T>(NTree<T> nodeData);

        class NTree<T>
        {
            T data;
            List<NTree<T>> children;

            public NTree(T data)
            {
                this.data = data;
                children = new List<NTree<T>>();
            }

            public void addChild(T data)
            {
                children.Add(new NTree<T>(data));
            }

            public NTree<T> getChild(int i)
            {
                return children[i];
            }

            public T getData()
            {
                return data;
            }

            public int childsCount()
            {
                return children.Count;
            }

            public void traverse( TreeVisitor<T> visitor)
            {
                visitor(this);
                foreach (NTree<T> kid in this.children)
                    kid.traverse(visitor);
            }
        }

        public override bool promoteMemberToModerator(UserInfo u, MemberInfo moder, SubForumInfo s)
        {
            throw new NotImplementedException();
        }

        public override bool EmailConfirm(long ConfNumber, UserInfo u, string username)
        {
            throw new NotImplementedException();
        }

        public override List<PostInfo> WatchAllMemberPost(UserInfo u, MemberInfo m)
        {
            throw new NotImplementedException();
        }

        public override int HowManyForums(UserInfo u)
        {
            throw new NotImplementedException();
        }

        public override List<MemberInfo> WatchAllMembers(UserInfo _usr, ForumInfo forumInfo)
        {
            throw new NotImplementedException();
        }

    }
}