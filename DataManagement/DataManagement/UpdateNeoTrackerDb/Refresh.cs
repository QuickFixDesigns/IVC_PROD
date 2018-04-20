using DataManagement.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public static class Refresh
    {
        public static void UpdateProjectHeader()
        {
            Console.WriteLine("Projects.LoadDataBase");
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                using (var Genius = new IVCLIVEDBEntities())
                {
                    var list = Neo.Projects.ToList();

                    foreach (var i in list)
                    {
                        var order = Genius.Comms.FirstOrDefault(x => x.No_Com == i.Code);
                        i.Client = order.Fact_A1;
                        i.PurchaseOrder = order.No_Po;
                        Neo.Entry(i).State = EntityState.Modified;
                    }
                    Neo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
    }
}
