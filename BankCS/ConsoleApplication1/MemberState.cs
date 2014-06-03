using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public interface MemberState
    {
		 Guid Id { get; set; }
         bool DeletePost(Member m, Post p);
    }
}
