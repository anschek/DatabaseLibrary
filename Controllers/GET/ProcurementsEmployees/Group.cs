using DatabaseLibrary.Entities.DTOs;
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
            public static class Group
            {
                public static async Task<List<ProcurementsEmployeesGrouping>?> ByEmployee(int employeeId) // Получить информацию по тому, сколько тендеров назначено на конкретного сотрудника
                {
                    using ParsethingContext db = new();
                    List<ProcurementsEmployeesGrouping>? procurementsEmployees = null;
                    try
                    {
                        procurementsEmployees = await db.ProcurementsEmployees
                            .Include(pe => pe.Procurement.Law)
                            .Include(pe => pe.Employee)
                            .Include(pe => pe.Procurement.Method)
                            .Include(pe => pe.Procurement.Region)
                            .Include(pe => pe.Procurement)
                            .Where(pe => pe.EmployeeId == employeeId)
                            .GroupBy(pe => pe.Employee.FullName)
                            .Select(g => new ProcurementsEmployeesGrouping
                            {
                                Id = g.Key,
                                CountOfProcurements = g.Count(),
                                Procurements = g.Select(pe => pe.Procurement).ToList() // Добавлено
                            })
                            .ToListAsync();

                    }
                    catch { }


                    return procurementsEmployees;
                }

                public static async Task<List<ProcurementsEmployeesGrouping>?> ByPositionsAndStates(string[] positions, string[] procurementStates)
                {
                    using ParsethingContext db = new();
                    List<ProcurementsEmployeesGrouping>? procurementsEmployees = null;
                    try
                    {
                        procurementsEmployees = await Queries.AllForGrouping(db)
                            .Where(pe => positions.Contains(pe.Employee.Position.Kind))
                            .Where(pe => procurementStates.Contains(pe.Procurement.ProcurementState.Kind))
                            .Where(pe => pe.Procurement.Applications != true)
                            .Where(pe => !(pe.Procurement.ProcurementState.Kind == "Принят" && pe.Procurement.RealDueDate != null))
                            .GroupBy(pe => pe.Employee.FullName)
                            .Select(g => new ProcurementsEmployeesGrouping
                            {
                                Id = g.Key,
                                CountOfProcurements = g.Count(),
                                Procurements = g.Select(pe => pe.Procurement).ToList()
                            })
                            .ToListAsync();

                    }
                    catch { }

                    return procurementsEmployees;
                }

                public static async Task<List<ProcurementsEmployeesGrouping>?> ByPositions(string[] positions) // Получить список сотруников и тендеров, которые у них в работе (по массиву должностей) 
                {
                    using ParsethingContext db = new();
                    List<ProcurementsEmployeesGrouping>? procurementsEmployees = null;
                    try
                    {
                        procurementsEmployees = await Queries.AllForGrouping(db)
                            .Where(pe => positions.Contains(pe.Employee.Position.Kind))
                            .GroupBy(pe => pe.Employee.FullName)
                            .Select(g => new ProcurementsEmployeesGrouping
                            {
                                Id = g.Key,
                                CountOfProcurements = g.Count(),
                                Procurements = g.Select(pe => pe.Procurement).ToList() // Добавлено
                            })
                            .ToListAsync();
                    }
                    catch { }

                    return procurementsEmployees;
                }

                public static async Task<List<ProcurementsEmployeesGrouping>?> ByMethod() // Получить список отправленных тендеров групированных по методам проведения
                {
                    using ParsethingContext db = new();
                    List<ProcurementsEmployeesGrouping>? procurementsEmployees = null;

                    try
                    {
                        procurementsEmployees = await db.Procurements
                            .Include(p => p.Law)
                            .Include(p => p.Method)
                            .Include(p => p.ProcurementState)
                            .Where(p => p.Method != null)
                            .Where(p => p.ProcurementState.Kind == "Отправлен")
                            .GroupBy(p => p.Method.Text)
                            .Select(g => new ProcurementsEmployeesGrouping
                            {
                                Id = g.Key,
                                CountOfProcurements = g.Count(),
                                Procurements = g.ToList() // Добавлено
                            })
                            .ToListAsync();
                    }
                    catch { }

                    return procurementsEmployees;
                }
            }
        }
    }
}