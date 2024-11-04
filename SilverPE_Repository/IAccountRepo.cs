using SilverPE_BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverPE_Repository
{
    public interface IAccountRepo
    {
        public BranchAccount GetBranchAccount(string email,string password);

    }
}
