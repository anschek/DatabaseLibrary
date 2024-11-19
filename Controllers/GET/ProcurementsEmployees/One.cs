using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers.GET
{
    public static partial class ProcurementsEmployees
    {
        public static class One
        {
            public static async Task<ProcurementsEmployee?> ByThreePositions(Procurement procurement, string premierPosition, string secondPosition, string thirdPosition) // Получить список тендеров и сотрудников, по id тендера и трем должностям
            {
                using ParsethingContext db = new();
                ProcurementsEmployee? procurementsEmployee = null;

                try
                {
                    procurementsEmployee = await db.ProcurementsEmployees
                    .Include(pe => pe.Employee)
                    .Include(pe => pe.Employee.Position)
                    .Where(pe => pe.ProcurementId == procurement.Id && (pe.Employee.Position.Kind == premierPosition || pe.Employee.Position.Kind == secondPosition || pe.Employee.Position.Kind == thirdPosition))
                    .FirstAsync();
                }
                catch { }

                return procurementsEmployee;
            }
        }
    }
}
