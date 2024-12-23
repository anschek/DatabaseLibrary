using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class DELETE
    {
        public static async Task<bool> ProcurementsEmployee(ProcurementsEmployee procurementsEmployee)
        {
            using ParsethingContext db = new();
            bool isSaved = true;
            try
            {
                var procurementToDelete = db.ProcurementsEmployees.FirstOrDefault(pe => pe.ProcurementId == procurementsEmployee.ProcurementId && pe.EmployeeId == procurementsEmployee.EmployeeId && pe.ActionType == procurementsEmployee.ActionType);
                if (procurementsEmployee != null)
                {
                    _ = db.ProcurementsEmployees.Remove(procurementToDelete);
                    _ = await db.SaveChangesAsync();
                }
            }
            catch { isSaved = false; }
            return isSaved;
        }
    }
}
