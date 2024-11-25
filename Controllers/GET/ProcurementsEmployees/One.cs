using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static partial class GET
    {
        public static partial class ProcurementsEmployees
        {
            public static class One
            {
                public static async Task<ProcurementsEmployee?> ByPositions(Procurement procurement, string[] positions) // Получить список тендеров и сотрудников, по id тендера и должностям
                {
                    using ParsethingContext db = new();
                    ProcurementsEmployee? procurementsEmployee = null;

                    try
                    {
                        procurementsEmployee = await db.ProcurementsEmployees
                        .Include(pe => pe.Employee)
                        .Include(pe => pe.Employee.Position)
                        .Where(pe => pe.ProcurementId == procurement.Id && positions.Contains(pe.Employee.Position.Kind))
                        .FirstAsync();
                    }
                    catch { }

                    return procurementsEmployee;
                }
            }
        }
    }
}