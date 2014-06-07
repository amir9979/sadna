using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public interface PolicyInterface
    {
		 Guid Id { get; set; }

         bool CanBeAdmin(Member m);

         bool CanBeModerate(Member m, SubForum b);

         bool IsLegalPass(string password);

         bool CanDoConfirmedOperations(Member m); // check if user type is confirmed

         int minPostsToCheck();

         int minWords();

         int getPolicyNumber();

         int MaxMonth { get; set; }

    }
}
