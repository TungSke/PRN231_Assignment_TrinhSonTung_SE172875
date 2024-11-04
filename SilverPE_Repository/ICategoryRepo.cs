using SilverPE_BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverPE_Repository
{
    public interface ICategoryRepo
    {
        public Category GetCategory(string id);

        public List<Category> GetCategories();
      
    }
}
