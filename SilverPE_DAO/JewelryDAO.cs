using Microsoft.EntityFrameworkCore;
using SilverPE_BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverPE_DAO
{
    public class JewelryDAO
    {   
        private SilverJewelry2023DbContext context;
        private static JewelryDAO instance;
        public static JewelryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JewelryDAO();
                }
                return instance;
            }
        }
        public JewelryDAO() 
        {
            context = new SilverJewelry2023DbContext();
        }

        public List<SilverJewelry> GetSilvers()
        {
            return context.SilverJewelries.Include(x => x.Category).ToList();
        }

        public bool addJewelry(SilverJewelry silverJewelry)
        {
            bool result = false;
            SilverJewelry silver = this.GetSilverJewerly(silverJewelry.SilverJewelryId);
            if (silver == null)
            {
                try
                {
                    context.SilverJewelries.Add(silverJewelry);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }
        public bool updateJewelry(SilverJewelry silverJewelry)
        {
            bool result = false;
            SilverJewelry silver = this.GetSilverJewerly(silverJewelry.SilverJewelryId);
            if (silver != null)
            {
                try
                {
                    context.Entry(silver).CurrentValues.SetValues(silverJewelry);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }
        public bool deleteJewelry(string jewelryId)
        {
            bool result = false;
            SilverJewelry silver = this.GetSilverJewerly(jewelryId);
            if (silver != null)
            {
                try
                {
                    context.SilverJewelries.Remove(silver);
                    context.SaveChanges();
                    result = true; // Cập nhật result nếu xoá thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting jewelry: {ex.Message}"); // Log ngoại lệ
                }
            }
            return result;
        }


        public SilverJewelry GetSilverJewerly(string id)
        {
            return context.SilverJewelries.Include(x => x.Category).FirstOrDefault(m => m.SilverJewelryId.Equals(id));
        }
    }
}
