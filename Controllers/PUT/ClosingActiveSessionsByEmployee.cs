using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> ClosingActiveSessionsByEmployee(int employeeId)
        {
            using ParsethingContext db = new();
            Procurement? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Procurements
                    .FirstOrDefaultAsync(p => p.CalculatingUserId == employeeId || p.PurchaseUserId == employeeId || p.ProcurementUserId == employeeId);

                if (def != null)
                {
                    if (def.CalculatingUserId == employeeId)
                    {
                        def.CalculatingUserId = null;
                        def.IsCalculationBlocked = false;
                    }

                    if (def.PurchaseUserId == employeeId)
                    {
                        def.PurchaseUserId = null;
                        def.IsPurchaseBlocked = false;
                    }

                    if (def.ProcurementUserId == employeeId)
                    {
                        def.ProcurementUserId = null;
                        def.IsProcurementBlocked = false;
                    }

                    _ = await db.SaveChangesAsync();
                }
                else
                {
                    isSaved = false;
                }
            }
            catch
            {
                isSaved = false;
            }

            return isSaved;
        }
    }
}
