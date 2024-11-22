using DatabaseLibrary.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static partial class GET
    {
        public static partial class ProcurementsEmployees
        {
            public static class Many
            {
                public static async Task<List<ProcurementsEmployee>?> ByStateAndStartDate(string procurementState, DateTime startDate, int employeeId) // Получить тендеры по статусу у конкретного сотрудника по дате
                {
                    using ParsethingContext db = new();
                    List<ProcurementsEmployee>? procurementsEmployees = null;

                    try
                    {
                        var validProcurementIds = Queries.validProcurementIds(procurementState, startDate);

                        procurementsEmployees = await db.ProcurementsEmployees
                            .Include(pe => pe.Procurement)
                            .Include(pe => pe.Procurement.ProcurementState)
                            .Include(pe => pe.Procurement.Method)
                            .Include(pe => pe.Procurement.Region)
                            .Include(pe => pe.Employee)
                            .Include(pe => pe.Procurement.Law)
                            .Include(pe => pe.Employee.Position)
                            .Where(pe => validProcurementIds.Contains(pe.ProcurementId) && pe.EmployeeId == employeeId)
                            .ToListAsync();
                    }
                    catch { }

                    return procurementsEmployees;
                }

                public static async Task<List<ProcurementsEmployee>?> ByState(int employeeId, string procurementStateKind) // Получить список тендеров и сотудиков, по статусу и id сотрудника
                {
                    List<ProcurementsEmployee>? procurements = null;

                    try
                    {
                        procurements = await Queries.All()
                            .Where(pe => pe.Procurement.ProcurementState != null && pe.Procurement.ProcurementState.Kind == procurementStateKind)
                            .Where(pe => pe.Procurement.Applications != true)
                            .Where(pe => pe.Employee.Id == employeeId)
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

                public static async Task<List<ProcurementsEmployee>?> ByEmployee(int employeeId) // Получть список тендеров и сотрудников по id сотрудника
                {
                    using ParsethingContext db = new();
                    List<ProcurementsEmployee>? procurements = null;

                    try
                    {
                        procurements = await Queries.All()
                            .Where(pe => pe.Procurement.ProcurementState != null)
                            .Where(pe => pe.Employee.Id == employeeId)
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

                public static async Task<List<ProcurementsEmployee>?> ByProcurement(int procurementId) // Получть список тендеров и сотрудников по id тендера
                {
                    using ParsethingContext db = new();
                    List<ProcurementsEmployee>? procurements = null;

                    try
                    {
                        procurements = await db.ProcurementsEmployees
                            .Include(pe => pe.Employee)
                            .Where(pe => pe.Procurement.Id == procurementId)
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

                public static async Task<List<ProcurementsEmployee>?> ByKind(KindOf kindOf, int employeeId, string? kind = null) // kind остается null только для Application, ExecutionState, WarrantyState, Judgement и FAS
                {
                    List<ProcurementsEmployee>? procurementsEmployees = null;

                    try
                    {
                        Expression<Func<ProcurementsEmployee, bool>> kindPredicate = Queries.KindPredicate(kindOf, kind);

                        IQueryable<ProcurementsEmployee> procurementsEmployeesQuery = Queries.All()
                           .Include(pe => pe.Procurement.ShipmentPlan);

                        if (kindOf == KindOf.ExecutionState) procurementsEmployeesQuery = procurementsEmployeesQuery.Include(pe => pe.Procurement.ExecutionState);
                        else if (kindOf == KindOf.WarrantyState || kindOf == KindOf.CorrectionDate) procurementsEmployeesQuery = procurementsEmployeesQuery.Include(pe => pe.Procurement.WarrantyState);

                        procurementsEmployees = await procurementsEmployeesQuery
                            .Where(pe => pe.Employee.Id == employeeId) // по id сотрудника
                            .Where(kindPredicate).ToListAsync(); // и предикату типв
                    }
                    catch { }

                    return procurementsEmployees;
                }

                public static async Task<List<ProcurementsEmployee>?> ByDateKind(string procurementStateKind, bool isOverdue, KindOf kindOf, int employeeId) // Получить список тендеров и сотрудников по:
                {
                    List<ProcurementsEmployee>? procurementsEmployees = null;

                    try
                    {
                        // Предикат типа фильтрует тендеры сотрудников
                        Expression<Func<ProcurementsEmployee, bool>> kindPredicate = pe =>
                            kindOf == KindOf.ContractConclusion
                            ? pe.Procurement.ProcurementState.Kind == "Выигран 1ч" || pe.Procurement.ProcurementState.Kind == "Выигран 2ч" // для даты заключения контракта по выигрышам
                            : pe.Procurement.ProcurementState.Kind == procurementStateKind; // для остальных дат по указанному типу статуса

                        // Прендикат срока фильтрует в соответствии с датами
                        Expression<Func<ProcurementsEmployee, bool>> termPredicate = Queries.TermPredicate(kindOf, isOverdue);

                        procurementsEmployees = await Queries.All()
                            .Include(pe => pe.Procurement.ShipmentPlan)
                            .Where(pe => pe.Employee.Id == employeeId)
                            .Where(kindPredicate)
                            .Where(termPredicate)
                            .ToListAsync();
                    }
                    catch { }

                    return procurementsEmployees;
                }

                public static async Task<List<ProcurementsEmployee>?> Accepted(int employeeId, bool? isOverdue = null) // Получить тендеры, назначнные на конкретного сотрудника
                {
                    List<ProcurementsEmployee>? procurementsEmployees = null;

                    try
                    {
                        // Предикат срока фильтрует тендеры по полю максимального срока в зависимости от значения переменной "Просрочено"
                        Expression<Func<ProcurementsEmployee, bool>> termPredicate = isOverdue switch
                        {
                            null => pe => pe.Procurement.MaxDueDate != null,          // любой срок, если isOverdue не указано
                            true => pe => pe.Procurement.MaxDueDate < DateTime.Now,  // просрочено
                            false => pe => pe.Procurement.MaxDueDate > DateTime.Now // в срок
                        };

                        procurementsEmployees = await Queries.All()
                            .Include(pe => pe.Procurement.ShipmentPlan)
                            .Where(pe => pe.Employee.Id == employeeId)
                            .Where(pe => pe.Procurement.ProcurementState.Kind == "Принят")
                            .Where(pe => pe.Procurement.RealDueDate == null)
                            .Where(termPredicate)
                            .Where(pe => (pe.Procurement.Amount ?? 0) < (pe.Procurement.ReserveContractAmount != null && pe.Procurement.ReserveContractAmount != 0 ? pe.Procurement.ReserveContractAmount : pe.Procurement.ContractAmount))
                            .ToListAsync();
                    }
                    catch { }

                    return procurementsEmployees;
                }

                public static async Task<List<ProcurementsEmployee>?> ByVisa(KindOf kindOf, bool stageCompleted, int employeeId) // Получить тендеры, назначенные на конкретного сотрудника по визе расчета или закупки
                {
                    List<ProcurementsEmployee>? procurementsEmployees = null;

                    try
                    {
                        Expression<Func<ProcurementsEmployee, bool>> stagePredicate = Queries.StagePredicateByVisa(kindOf, stageCompleted);
                        
                        procurementsEmployees = await Queries.All()
                            .Include(pe => pe.Procurement.ShipmentPlan)
                            .Where(pe => pe.Employee.Id == employeeId)
                            .Where(pe => pe.Procurement.ProcurementState.Kind == "Выигран 1ч" || pe.Procurement.ProcurementState.Kind == "Выигран 2ч")
                            .ToListAsync();

                    }
                    catch { }

                    return procurementsEmployees;
                }
            }
        }
    }
}