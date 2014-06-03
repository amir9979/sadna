using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class Policy : PolicyInterface
    {
		public virtual  Guid _Id{ get; set; }
        public virtual int MaxModerators{ get; set; }
		public Policy() : this(0)
        {}
        public Policy(int max)
        {
            this.MaxModerators = 500;
        }
        public virtual bool CanBeAdmin(Member m)
        {
            /*

            if (m.type.Equals("Gold") || m.type.Equals("Silver"))
            {   // will change according to specific policy
                return true;
            }
          return false;
             * 
             * 
             
             */

            return true;
        }

        public virtual bool CanDoConfirmedOperations(Member m)
        {
            return !(m.type.Equals("Not Confirmed"));
        }
        public virtual bool CanBeModerate(Member m, SubForum b)
        {
            // if m.type==silver && b.NumOfCurrModertors < maximun moderats
            return true;   // will change according to specific policy
        }



        
        public virtual bool IsLegalPass(string password)
        {
            if (password.Length > 4 && hasUpperCase(password))  // for example minimun 4 charcter , at least 1 capital
                return true;
            return false;
        }

        bool hasUpperCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                    return true;
            }
            return false;
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
            return MaxModerators;
        }
    }
}
