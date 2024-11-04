using SilverPE_BOs.Models;
using SilverPE_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverPE_Repository
{
    public class AccountRepo : IAccountRepo
    {
        public BranchAccount GetBranchAccount(string email, string password)=>AccountDAO.Instance.GetBranchAccount(email, password);
    }
}
