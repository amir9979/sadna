using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class DefaultPolicy : PolicyInterface
    {
        public virtual  Guid _Id{ get; set; }

        public virtual bool CanBeAdmin(Member m)
        {
            return true;
        }

        public virtual bool CanBeModerate(Member m, SubForum b)
        {
            return true;
        }

        public virtual bool IsLegalPass(string password)
        {
            return true;
        }

        public virtual bool CanDoConfirmedOperations(Member m)
        {
            return true;
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

        public virtual int getPolicyNumber()
        {
            return 0;
        }
        public virtual int minPostsToCheck()
        {
            return -1;
        }

        public virtual int minWords()
        {
            return -1;
        }
    }
}
