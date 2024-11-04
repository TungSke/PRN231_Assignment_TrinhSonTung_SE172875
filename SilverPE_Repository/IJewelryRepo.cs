using SilverPE_BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverPE_Repository
{
    public interface IJewelryRepo
    {
        public List<SilverJewelry> GetSilvers();
        public bool addJewelry(SilverJewelry silverJewelry);
        public bool updateJewelry(SilverJewelry silverJewelry);
        public bool deleteJewelry(string jewlryid);

        SilverJewelry GetSilverJewerly(string id);
    }
}
