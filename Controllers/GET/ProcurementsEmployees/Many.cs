using DatabaseLibrary.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers.GET
{
    public static partial class ProcurementsEmployees
    {
        public static class Many
        {
            public static async Task<List<ProcurementsEmployee>?> ByStateAndStartDateAndEmployee(string procurementState, DateTime startDate, int employeeId) // Получить тендеры по статусу у конкретного сотрудника по дате
            {
                using ParsethingContext db = new();
                List<ProcurementsEmployee>? procurementsEmployees = null;

                try
                {
                    List<int> validProcurementIds;

                    if (procurementState == "Выигран 1ч")
                    {
                        var excludedStatuses = new List<string> { "Проигран", "Отклонен", "Отмена" };

                        validProcurementIds = db.Histories
                            .Where(h => h.Text == procurementState && h.Date >= startDate)
                            .GroupBy(h => h.EntryId)
                            .Where(g => !g.Any(h => excludedStatuses.Contains(h.Text)))
                            .Select(g => g.Key)
                            .Distinct()
                            .ToList();
                    }
                    else
                    {
                        validProcurementIds = db.Histories
                            .Where(h => h.Text == procurementState && h.Date >= startDate)
                            .Select(h => h.EntryId)
                            .Distinct()
                            .ToList();
                    }

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

            public static async Task<List<ProcurementsEmployee>?> ByStateAndEmployee(int employeeId, string procurementStateKind) // Получить список тендеров и сотудиков, по статусу и id сотрудника
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

            public static async Task<List<ProcurementsEmployee>?> ByKindAndEmployee(KindOf kindOf, int employeeId, string? kind=null) // kind остается null только для Application, ExecutionState, WarrantyState, Judgement и FAS
            {
                List<ProcurementsEmployee>? procurementsEmployees = null;

                try
                {
                    // предикат типа фильтрует тендеры сотрудников по:
                    Expression<Func<ProcurementsEmployee, bool>> kindPredicate = kindOf switch
                    {
                        KindOf.ProcurementState => // Статусу тендера
                            pe => pe.Procurement.ProcurementState != null &&
                                pe.Procurement.ProcurementState.Kind == kind &&
                                pe.Procurement.Applications != true,

                        KindOf.ShipmentPlane =>   // Плану отгрузки, конкретным статусам
                            pe => pe.Procurement.ShipmentPlan != null &&
                                pe.Procurement.ShipmentPlan.Kind == kind &&
                                pe.Procurement.ProcurementState.Kind == "Выигран 2ч" &&
                                pe.Procurement.Applications != true,

                        KindOf.Applications => // Положительному наличию статуса "по заявкам"
                            pe => pe.Procurement.Applications == true &&
                                new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка" }.Contains(pe.Procurement.ProcurementState.Kind),

                        KindOf.ExecutionState => // Статусу обеспечения заявки, конкретным статусам тендера
                            pe => new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка" }.Contains(pe.Procurement.ProcurementState.Kind) &&
                                new string[] { "Запрошена БГ", "На согласовании заказчика", "Внесение правок", "Согласована БГ", "Ожидает оплаты", "Деньги(Возвратные)" }.Contains(pe.Procurement.ExecutionState.Kind),

                        KindOf.WarrantyState => // Статусу обеспечения гарантии, конкретным статусам тендера
                            pe => new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка" }.Contains(pe.Procurement.ProcurementState.Kind) &&
                                new string[] { "Запрошена БГ", "На согласовании заказчика", "Внесение правок", "Согласована БГ", "Ожидает оплаты", "Деньги(Возвратные)" }.Contains(pe.Procurement.WarrantyState.Kind),

                        KindOf.CorrectionDate => // Статусу обеспечения гарантии, конкретным статусам тендера
                            pe => pe.Procurement.ProcurementState.Kind == kind &&
                                pe.Procurement.CorrectionDate != null,

                        KindOf.Judgement => // Суд
                            pe => pe.Procurement.Judgment == true,                        
                        
                        KindOf.FAS => // ФАС
                            pe => pe.Procurement.Fas == true,

                        _ => throw new ArgumentException($"KindOf.{kindOf.ToString()} is not supported for this method")
                    };

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

            public static async Task<List<ProcurementsEmployee>?> ByDateKindAndEmployee(string procurementStateKind, bool isOverdue, KindOf kindOf, int employeeId) // Получить список тендеров и сотрудников по:
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
                    Expression<Func<ProcurementsEmployee, bool>> termPredicate = kindOf switch
                    {
                        KindOf.Deadline => // Дате окончания подачи заявок
                            pe => (isOverdue ? pe.Procurement.Deadline < DateTime.Now : pe.Procurement.Deadline > DateTime.Now),

                        KindOf.StartDate => // Дате начала подачи заявок
                            pe => (isOverdue ? pe.Procurement.StartDate < DateTime.Now : pe.Procurement.StartDate > DateTime.Now),

                        KindOf.ContractConclusion => // Дате подписания контракта
                            pe => pe.Procurement.Applications != true &&
                                (isOverdue ? pe.Procurement.ConclusionDate != null : pe.Procurement.ConclusionDate == null),

                        // Остальные типы не поддерживаются
                        _ => throw new ArgumentException($"KindOf.{kindOf.ToString()} is not supported for this method")
                    };

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

            public static async Task<List<ProcurementsEmployee>?> Accepted(int employeeId, bool? isOverdue=null) // Получить тендеры, назначнные на конкретного сотрудника
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

            public static async Task<List<ProcurementsEmployee>?> ByVisaAndEmployee( KindOf kindOf, bool stageCompleted, int employeeId) // Получить тендеры, назначенные на конкретного сотрудника по визе расчета или закупки
            {
                using ParsethingContext db = new();
                List<ProcurementsEmployee>? procurementsEmployees = null;

                try
                {
                    Expression<Func<ProcurementsEmployee, bool>> stagePredicate = kindOf switch
                    {
                        KindOf.Calculating => // По визе расчета
                            pe => (stageCompleted
                            ? pe.Procurement.Calculating == true
                            : pe.Procurement.Calculating == false || pe.Procurement.Calculating == null),

                        KindOf.Purchase => // По визе закупки
                            pe => (stageCompleted
                            ? pe.Procurement.Purchase == true
                            : pe.Procurement.Purchase == false || pe.Procurement.Purchase == null)
                            && pe.Procurement.Calculating == true,

                        _ => throw new ArgumentException($"KindOf.{kindOf.ToString()} is not supported for this method")

                    };
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
