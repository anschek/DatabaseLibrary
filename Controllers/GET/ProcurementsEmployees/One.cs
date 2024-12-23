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
                public static async Task<ProcurementsEmployee?> ByPositions(Procurement procurement, string[] positions, string actionType) // Получить список тендеров и сотрудников, по id тендера и должностям
                {
                    using ParsethingContext db = new();
                    ProcurementsEmployee? procurementsEmployee = null;

                    try
                    {
                        procurementsEmployee = await db.ProcurementsEmployees
                        .Include(pe => pe.Employee)
                        .Include(pe => pe.Employee.Position)
                        .Where(pe => pe.ProcurementId == procurement.Id 
                        && positions.Contains(pe.Employee.Position.Kind)
                        && pe.ActionType == actionType)
                        .FirstAsync();
                    }
                    catch { }

                    return procurementsEmployee;
                }

                public static async Task<bool> ByProcurementAndActionType(int procurementId, int employeeId, string actionType) // Узнать, есть ли у сотрудника конкретный тендер в избранном
                {
                    using ParsethingContext db = new();
                    ProcurementsEmployee? procurementsEmployee = null;
                    try
                    {
                        procurementsEmployee = await db.ProcurementsEmployees
                            .Where(pe => pe.ProcurementId == procurementId && pe.EmployeeId == employeeId && pe.ActionType == actionType)
                            .FirstAsync();
                    }
                    catch { }
                    if (procurementsEmployee == null)
                        return false;
                    else
                        return true;
                }
            }
        }
    }
}