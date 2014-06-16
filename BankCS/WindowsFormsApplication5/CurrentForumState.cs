using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication5
{
    public class CurrentForumState
    {
        public CurrentForumState() { currentPolicyInfo = new PolicyInfo(); }
        public CurrentForumState(ForumInfo myForum, SubForumInfo currentSubForumInfo, PostInfo currentPostInfo)
        {
            this.myForum = myForum;
            this.currentSubForumInfo = currentSubForumInfo;
            this.currentPostInfo = currentPostInfo;
            
        }


        public ForumInfo myForum { get; set; }
        public SubForumInfo currentSubForumInfo { get; set; }
        public PostInfo currentPostInfo { get; set; }
		public List<ForumInfo> allForum { get; set; }
        public List<SubForumInfo> allSubForum { get; set; }
        public List<MemberInfo> allMembers { get; set; }
        public PolicyInfo currentPolicyInfo { get; set; }


    }
}
