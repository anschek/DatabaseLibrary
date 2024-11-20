using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> ProcurementsEmployee(ProcurementsEmployee procurementsEmployee, string[] positions)
        {
            using ParsethingContext db = new();
            ProcurementsEmployee? def = null;
            bool isSaved = true;

            try
            {
                if (procurementsEmployee.Procurement != null && procurementsEmployee.Employee != null)
                {
                    procurementsEmployee.ProcurementId = procurementsEmployee.Procurement.Id;
                    procurementsEmployee.EmployeeId = procurementsEmployee.Employee.Id;
                }
                else throw new Exception();
                procurementsEmployee.Procurement = null;
                procurementsEmployee.Employee = null;
            }
            catch { isSaved = false; }

            try
            {
                def = await db.ProcurementsEmployees
                    .Include(pe => pe.Employee)
                    .Include(pe => pe.Employee.Position)
                    .Where(pe => pe.ProcurementId == procurementsEmployee.ProcurementId && positions.Contains(pe.Employee.Position.Kind))
                    .FirstAsync();

                def.ProcurementId = procurementsEmployee.ProcurementId;
                def.EmployeeId = procurementsEmployee.EmployeeId;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
