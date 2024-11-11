using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<ShipmentPlan>?> ShipmentPlans() // Получить список планов отгрузки
        {
            using ParsethingContext db = new();
            List<ShipmentPlan>? shipmentPlans = null;

            try { shipmentPlans = await db.ShipmentPlans.ToListAsync(); }
            catch { }

            return shipmentPlans;
        }
    }
}
