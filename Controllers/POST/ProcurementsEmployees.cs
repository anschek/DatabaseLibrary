using DatabaseLibrary.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static class ProcurementsEmployees
        {
            public static async Task<bool> One(ProcurementsEmployee procurementsEmployee)
            {
                using ParsethingContext db = new();
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

                    _ = await db.ProcurementsEmployees.AddAsync(procurementsEmployee);
                    _ = await db.SaveChangesAsync();
                }
                catch { isSaved = false; }

                return isSaved;
            }
        }
        public static async Task<bool> OneByEmployee(int employeeId)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                var procurementToAssign = await db.Procurements
                    .Include(p => p.ProcurementState)
                        .Include(p => p.Law)
                        .Where(p => p.ProcurementState.Kind == "Новый"
                                    && !db.ProcurementsEmployees.Any(pe => pe.ProcurementId == p.Id))
                        .OrderBy(p => p.Deadline)
                        .ThenBy(p => p.Law.Number)  // Сортировка по номеру закона
                        .ThenBy(p => p.Object.Contains("компьютер") ? 1 : 0)  // Проверка на наличие слова "компьютер"
                        .ThenBy(p => p.Object.Contains("системный") ? 1 : 0)  // Проверка на наличие слова "системный"
                        .ThenBy(p => p.Object.Contains("моноблок") ? 1 : 0)   // Проверка на наличие слова "моноблок"
                        .ThenBy(p => p.Object.Contains("автоматизирован") ? 1 : 0)  // Проверка на наличие слова "автоматизирован"
                        .ThenBy(p => p.Object.Contains("монитор") ? 1 : 0)    // Проверка на наличие слова "монитор"
                        .ThenBy(p => p.Object.Contains("ноутбук") ? 1 : 0)    // Проверка на наличие слова "ноутбук"
                        .FirstOrDefaultAsync();

                if (procurementToAssign != null)
                    if (procurementToAssign != null)
                    {
                        ProcurementsEmployee procurementEmployee = new ProcurementsEmployee
                        {
                            ProcurementId = procurementToAssign.Id,
                            EmployeeId = employeeId
                        };

                        await db.ProcurementsEmployees.AddAsync(procurementEmployee);
                        await db.SaveChangesAsync();
                    }
            }
            catch { isSaved = false; }

            return isSaved;
        }

        public static async Task<bool> OneByPositions(ProcurementsEmployee procurementsEmployee, string[] positions)
        {
            using ParsethingContext db = new();
            bool isSaved = true;
            ProcurementsEmployee? def = null;
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
            }
            catch { }
            try
            {
                if (def == null)
                {
                    _ = await db.ProcurementsEmployees.AddAsync(procurementsEmployee);
                    _ = await db.SaveChangesAsync();
                }
                else if (!await PUT.ProcurementsEmployee(procurementsEmployee, positions))
                    throw new Exception();
            }
            catch { isSaved = false; }


            return isSaved;
        }
    }
}
