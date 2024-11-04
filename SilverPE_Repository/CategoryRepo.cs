using SilverPE_BOs.Models;
using SilverPE_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverPE_Repository
{
    public class CategoryRepo : ICategoryRepo
    {   
        public List<Category> GetCategories()=>CategoryDAO.Instance.GetCategories();

        public Category GetCategory(string id)=>CategoryDAO.Instance.GetCategory(id);

    }
}
