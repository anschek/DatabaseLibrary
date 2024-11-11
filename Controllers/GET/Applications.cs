using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Applications
        {
            public static async Task<List<Procurement>?> Many(int? procurementId)
            {
                using ParsethingContext db = new();
                List<Procurement>? procurements = null;

                try
                {
                    procurements = await db.Procurements
                        .Include(p => p.ProcurementState)
                        .Include(p => p.Law)
                        .Include(p => p.Method)
                        .Include(p => p.Platform)
                        .Include(p => p.Organization)
                        .Include(p => p.TimeZone)
                        .Include(p => p.Region)
                        .Include(p => p.City)
                        .Include(p => p.ShipmentPlan)
                        .Where(p => p.ParentProcurementId == procurementId)
                        .ToListAsync();
                }
                catch { }
                return procurements;
            }

            public static async Task<int> Number(int? procurementId) // получить номер создаваемой заявки при ее создании
            {
                using ParsethingContext db = new();
                int number = 0;

                try{ number = await db.Procurements.CountAsync(p => p.ParentProcurementId == procurementId); }
                catch { }

                return number + 1;
            }
            public static async Task<int> Count(int procurementId) // получить количество заявок по id тендера
            {
                using ParsethingContext db = new();
                int number = 0;

                try { number = await db.Procurements.CountAsync(p => p.ParentProcurementId == procurementId); }
                catch { }

                return number;
            }
        }       
    }
}
