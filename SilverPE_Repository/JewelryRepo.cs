using SilverPE_BOs.Models;
using SilverPE_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverPE_Repository
{
    public class JewelryRepo : IJewelryRepo
    {
        public List<SilverJewelry> GetSilvers() => JewelryDAO.Instance.GetSilvers();

        public SilverJewelry GetSilverJewerly(string id) => JewelryDAO.Instance.GetSilverJewerly(id);

        public bool addJewelry(SilverJewelry silverJewelry) => JewelryDAO.Instance.addJewelry(silverJewelry);

        public bool deleteJewelry(string jewlryid) =>JewelryDAO.Instance.deleteJewelry(jewlryid);

        public bool updateJewelry(SilverJewelry silverJewelry) => JewelryDAO.Instance.updateJewelry(silverJewelry);
    }
}
