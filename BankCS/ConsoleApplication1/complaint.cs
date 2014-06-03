using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Complaint
    {
		public virtual Guid Id { get; set; }
        public virtual User TheComplainer{ get; set; }
        public virtual string complaint{ get; set; }

        public Complaint(): this(null, "")
        {
            
        }
        public Complaint(User u, string c)
        {
            this.TheComplainer = u;
            this.complaint = c;
        }

        public virtual void EditComplaint(string s){
            this.complaint=s;
        }

    }
     
}
