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
            public static class Count
            {
                public static async Task<int> ByStateAndStartDate(string procurementState, DateTime startDate, int employeeId, string actionType)// Получить количество тендеров по статусу и конкретному сотруднику по дате
                {
                    using ParsethingContext db = new();
                    int count = 0;

                    try
                    {
                        var validProcurementIds = Queries.validProcurementIds(db, procurementState, startDate);

                        count = await db.ProcurementsEmployees
                            .CountAsync(pe => validProcurementIds.Contains(pe.ProcurementId) 
                            && pe.EmployeeId == employeeId
                            && pe.ActionType == actionType);
                    }
                    catch { }

                    return count;
                }

                public static async Task<int> ByKind(KindOf kindOf, int employeeId, string actionType, string? kind = null) // kind остается null только для Application, ExecutionState, WarrantyState, Judgement и FAS
                {
                    using ParsethingContext db = new();
                    int count = 0;

                    try
                    {
                        Expression<Func<ProcurementsEmployee, bool>> kindPredicate = Queries.KindPredicate(kindOf, kind);

                        IQueryable<ProcurementsEmployee> procurementsEmployeesQuery = db.ProcurementsEmployees
                            .Include(pe => pe.Employee)
                            .Include(pe => pe.Procurement)
                            .Include(pe => pe.Procurement.ProcurementState);

                        if (kindOf == KindOf.ShipmentPlane) procurementsEmployeesQuery = procurementsEmployeesQuery.Include(pe => pe.Procurement.ShipmentPlan);
                        else if (kindOf == KindOf.ExecutionState) procurementsEmployeesQuery = procurementsEmployeesQuery.Include(pe => pe.Procurement.ExecutionState);
                        else if (kindOf == KindOf.WarrantyState || kindOf == KindOf.CorrectionDate) procurementsEmployeesQuery = procurementsEmployeesQuery.Include(pe => pe.Procurement.WarrantyState);

                        count = await procurementsEmployeesQuery
                            .Where(pe => pe.Employee.Id == employeeId) // по id сотрудника
                            .Where(pe => pe.ActionType == actionType) // по actionType
                            .Where(kindPredicate) // и предикату типа
                            .CountAsync();
                    }
                    catch { }

                    return count;
                }

                public static async Task<int> ByDateKind(string procurementStateKind, bool isOverdue, KindOf kindOf, int employeeId, string actionType) // Получить список тендеров и сотрудников по:
                {
                    using ParsethingContext db = new();
                    int count = 0;

                    try
                    {
                        // Предикат типа фильтрует тендеры сотрудников
                        Expression<Func<ProcurementsEmployee, bool>> kindPredicate = pe =>
                            kindOf == KindOf.ContractConclusion
                            ? pe.Procurement.ProcurementState.Kind == "Выигран 1ч" || pe.Procurement.ProcurementState.Kind == "Выигран 2ч" // для даты заключения контракта по выигрышам
                            : pe.Procurement.ProcurementState.Kind == procurementStateKind; // для остальных дат по указанному типу статуса

                        // Прендикат срока фильтрует в соответствии с датами
                        Expression<Func<ProcurementsEmployee, bool>> termPredicate = Queries.TermPredicate(kindOf, isOverdue);

                        count = await db.ProcurementsEmployees
                                .Include(pe => pe.Employee)
                                .Include(pe => pe.Procurement)
                                .Include(pe => pe.Procurement.ProcurementState)
                                .Where(pe => pe.Employee.Id == employeeId)
                                .Where(pe => pe.ActionType == actionType)
                                .Where(kindPredicate)
                                .Where(termPredicate)
                                .CountAsync();
                    }
                    catch { }

                    return count;
                }

                public static async Task<int> Accepted(int employeeId, string actionType, bool? isOverdue = null) // Получить тендеры, назначнные на конкретного сотрудника
                {
                    using ParsethingContext db = new();
                    int count = 0;

                    try
                    {
                        // Предикат срока фильтрует тендеры по полю максимального срока в зависимости от значения переменной "Просрочено"
                        Expression<Func<ProcurementsEmployee, bool>> termPredicate = isOverdue switch
                        {
                            null => pe => pe.Procurement.MaxDueDate != null,          // любой срок, если isOverdue не указано
                            true => pe => pe.Procurement.MaxDueDate < DateTime.Now,  // просрочено
                            false => pe => pe.Procurement.MaxDueDate > DateTime.Now // в срок
                        };

                        count = await db.ProcurementsEmployees
                            .Include(pe => pe.Employee)
                            .Include(pe => pe.Procurement)
                            .Include(pe => pe.Procurement.ProcurementState)
                            .Where(pe => pe.Employee.Id == employeeId)
                            .Where(pe => pe.ActionType == actionType)
                            .Where(pe => pe.Procurement.ProcurementState.Kind == "Принят")
                            .Where(pe => pe.Procurement.RealDueDate == null)
                            .Where(termPredicate)
                            .Where(pe => (pe.Procurement.Amount ?? 0) < (pe.Procurement.ReserveContractAmount != null && pe.Procurement.ReserveContractAmount != 0 ? pe.Procurement.ReserveContractAmount : pe.Procurement.ContractAmount))
                            .CountAsync();
                    }
                    catch { }

                    return count;
                }

                public static async Task<int> ByVisa(KindOf kindOf, bool stageCompleted, int employeeId, string actionType) // Получить количество тендеров, назначенных на конкретного сотрудника по визе расчета или закупки
                {
                    using ParsethingContext db = new();
                    int count = 0;

                    try
                    {
                        Expression<Func<ProcurementsEmployee, bool>> stagePredicate = Queries.StagePredicateByVisa(kindOf, stageCompleted);
                        
                        count = await db.ProcurementsEmployees
                            .Include(pe => pe.Employee)
                            .Include(pe => pe.Procurement)
                            .Include(pe => pe.Procurement.ShipmentPlan)
                            .Where(pe => pe.Employee.Id == employeeId)
                            .Where(pe => pe.ActionType == actionType)
                            .Where (stagePredicate)
                            .Where(pe => pe.Procurement.ProcurementState.Kind == "Выигран 1ч" || pe.Procurement.ProcurementState.Kind == "Выигран 2ч")
                            .CountAsync();

                    }
                    catch { }

                    return count;
                }
            }
        }
    }
}