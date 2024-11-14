using DatabaseLibrary.Entities.DTOs;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Procurements
        {
            public static class One
            {
                public static async Task<Procurement?> ByNumber(string number) // Получить тендер
                {
                    using ParsethingContext db = new();
                    Procurement? procurement = null;

                    try
                    {
                        procurement = await db.Procurements
                            .Where(p => p.Number == number)
                            .FirstAsync();
                    }
                    catch { }

                    return procurement;
                }
                public static async Task<Procurement?> ById(int id) // Получить тендер по id
                {
                    using ParsethingContext db = new();
                    Procurement? procurement = null;

                    try
                    {
                        procurement = await All()
                            .Where(p => p.Id == id)
                            .FirstAsync();
                    }
                    catch { }

                    return procurement;
                }

                public static async Task<DeletedProcurement?> Deleted(string number) // Получить удаленную закупку по номеру
                {
                    using ParsethingContext db = new();
                    DeletedProcurement? deletedProcurement = null;

                    try
                    {
                        deletedProcurement = await db.DeletedProcurements
                            .Where(dp => dp.Number == number)
                            .FirstAsync();
                    }
                    catch { }

                    return deletedProcurement;
                }
            }
            private static IQueryable<Procurement> All()
            {
                using ParsethingContext db = new();

                return db.Procurements
                       .Include(p => p.ProcurementState)
                       .Include(p => p.Law)
                       .Include(p => p.Method)
                       .Include(p => p.Platform)
                       .Include(p => p.TimeZone)
                       .Include(p => p.Region)
                       .Include(p => p.ShipmentPlan)
                       .Include(p => p.Organization)
                       .Include(p => p.City);
            }

            public static class Many
            {

                public static async Task<List<Procurement>?> Sources() // Получить список полученных парсингом тендеров
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    try
                    {
                        procurements = await db.Procurements
                            .Include(p => p.ProcurementState)
                            .Include(p => p.Law)
                            .Include(p => p.Organization)
                            .Include(p => p.Method)
                            .Include(p => p.Platform)
                            .Include(p => p.TimeZone)
                            .Where(p => p.ProcurementState != null && p.ProcurementState.Kind == "Получен")
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

                public static async Task<List<Procurement>> WithComponentStates(List<Procurement> procurements)
                {
                    using ParsethingContext db = new();
                    try
                    {
                        var procurementIds = procurements.Select(p => p.Id).ToList();
                        var componentCalculations = await db.ComponentCalculations
                            .Where(cc => procurementIds.Contains(cc.ProcurementId))
                            .Where(cc => cc.IsDeleted == false)
                            .Include(cc => cc.ComponentState)
                            .ToListAsync();

                        var groupedComponentCalculations = componentCalculations
                            .GroupBy(cc => cc.ProcurementId)
                            .ToDictionary(g => g.Key, g => g.ToList());

                        foreach (var procurement in procurements)
                        {
                            if (groupedComponentCalculations.ContainsKey(procurement.Id))
                            {
                                var calculations = groupedComponentCalculations[procurement.Id];
                                procurement.ComponentStates = new ObservableCollection<ComponentStateCount>(
                                    calculations
                                        .Where(cc => cc.ComponentState != null) // Добавляем проверку на null
                                        .GroupBy(cc => cc.ComponentState?.Kind) // Используем безопасную навигацию для ComponentState
                                        .Select(group => new ComponentStateCount
                                        {
                                            State = group.Key ?? "Unknown", // Используем значение по умолчанию, если group.Key равен null
                                            Count = group.Count()
                                        })
                                );
                            }
                            else
                            {
                                // Если компоненты отсутствуют, устанавливаем пустую коллекцию
                                procurement.ComponentStates = new ObservableCollection<ComponentStateCount>();
                            }
                        }
                    }
                    catch { }

                    return procurements;
                }

                // todo add fas and j
                public static async Task<List<Procurement>?> ByKind(KindOf kindOf, string? kind=null) // kind остается null только для Application, Judgement и FAS
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;
                    //Expression<Func<Procurement, bool>> additionalCondition = p => true;

                    try
                    {
                        Expression<Func<Procurement, bool>> additionalCondition = kindOf switch
                        {
                            KindOf.ProcurementState => // Статусу
                                    p => p.Applications != true &&
                                         p.ProcurementState != null &&
                                         p.ProcurementState.Kind == kind,

                            KindOf.ShipmentPlane => // Плану отгрузки
                                    p => p.Applications != true &&
                                         p.ShipmentPlan != null &&
                                         p.ShipmentPlan.Kind == kind &&
                                         p.ProcurementState.Kind == "Выигран 2ч",

                            KindOf.CorrectionDate => // Тендерам на исправлении
                                    p => p.CorrectionDate != null &&
                                    p.ProcurementState.Kind == kind,

                            KindOf.Applications => // Выигранным по заявкам
                                    p => p.Applications == true,

                            KindOf.Judgement => // В суде
                                    p => p.Judgment == true,

                            KindOf.FAS => // В ФАС
                                    p => p.Fas == true,

                            _ => throw new ArgumentException($"KindOf.{kindOf.ToString()} is not supported for this method")
                        };

                        procurements = await All()
                            .Where(additionalCondition)
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }
                public static async Task<List<Procurement>?> ByStateAndStartDate(string procurementState, DateTime startDate) // Получить тендеры по статусу и дате
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    try
                    {
                        List<int> validProcurementIds;

                        if (procurementState == "Выигран 1ч")
                        {
                            var excludedStatuses = new List<string> { "Проигран", "Отклонен", "Отмена" };

                            validProcurementIds = await db.Histories
                                .Where(h => h.Text == procurementState && h.Date >= startDate)
                                .GroupBy(h => h.EntryId)
                                .Where(g => !g.Any(h => excludedStatuses.Contains(h.Text)))
                                .Select(g => g.Key)
                                .Distinct()
                                .ToListAsync();
                        }
                        else
                        {
                            validProcurementIds = await db.Histories
                                .Where(h => h.Text == procurementState && h.Date >= startDate)
                                .Select(h => h.EntryId)
                                .Distinct()
                                .ToListAsync();
                        }

                        procurements = await All()
                            .Where(p => validProcurementIds.Contains(p.Id))
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

                public static async Task<List<Procurement>?> ByDateKind(string procurementStateKind, bool isOverdue, KindOf kindOf) // Получить список тендеров:
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    try
                    {
                        // Предикат типа фильтрует тендеры
                        Expression<Func<Procurement, bool>> kindPredicate = p =>
                            kindOf == KindOf.ContractConclusion
                            ? p.ProcurementState.Kind == "Выигран 1ч" || p.ProcurementState.Kind == "Выигран 2ч" // для даты заключения контракта по выигрышам
                            : p.ProcurementState.Kind == procurementStateKind; // для остальных дат по указанному типу статуса

                        // Прендикат срока фильтрует в соответствии с посроченными датами
                        Expression<Func<Procurement, bool>> termPredicate = kindOf switch
                        {
                            // Дата окончания подачи заявок
                            KindOf.Deadline =>
                                // Просроченных и непросроченных по статусу
                                p => (isOverdue ? p.Deadline < DateTime.Now : p.Deadline > DateTime.Now),

                            // Дата начала подачи заявок
                            KindOf.StartDate =>
                                // Просроченных и непросроченных по статусу
                                p => (isOverdue ? p.StartDate < DateTime.Now : p.StartDate > DateTime.Now),

                            // Дата подведения итогов
                            KindOf.ResultDate =>
                                // Просроченных и непросроченных по статусу
                                p => (isOverdue ? p.ResultDate < DateTime.Now : p.ResultDate > DateTime.Now),

                            // По дате заключения контракта
                            KindOf.ContractConclusion =>
                                // Просроченных и непросроченных по статусу
                                p => (isOverdue ? p.ConclusionDate != null : p.ConclusionDate == null),

                            // Остальные типы не поддерживаются
                            _ => throw new ArgumentException($"KindOf.{kindOf.ToString()} is not supported for this method")
                        };

                        procurements = await All()
                                   .Where(kindPredicate)
                                   .Where(termPredicate)
                                   .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

                public static async Task<List<Procurement>?> ByVisa(KindOf kindOf, bool stageCompleted) // Получить список тендеров по визе расчетников, закупки 
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    try
                    {
                        Expression<Func<Procurement, bool>> stagePredicate = kindOf switch
                        {
                            KindOf.Calculating => // По визе расчета
                            p => (stageCompleted// Этап завершен или нет
                            ? p.Calculating == true
                            : p.Calculating == false || p.Calculating == null),

                            KindOf.Purchase => // По визе закупки
                            p => (stageCompleted // Этап завершен или нет
                            ? p.Purchase == true
                            : p.Purchase == false || p.Purchase == null)
                            && p.Calculating == true,

                            _ => throw new ArgumentException($"KindOf.{kindOf.ToString()} is not supported for this method")
                        }; 

                        procurements = await All()
                            .Where(stagePredicate)
                            .Where(p => p.ProcurementState.Kind == "Выигран 1ч" || p.ProcurementState.Kind == "Выигран 2ч")
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

                public static async Task<List<Procurement>?> AcceptedByOverdue(bool isOverdue) // Получить список приянятых неоплаченых тендеров
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    try
                    {
                        // Предикат срока фильтрует тендеры по полю максимального срока в зависимости от значения переменной "Просрочено"
                        Expression<Func<Procurement, bool>> termPredicate =
                            p => (isOverdue ? p.MaxDueDate < DateTime.Now : p.MaxDueDate > DateTime.Now);

                        procurements = await All()
                           .Where(termPredicate)
                           .Where(p => p.ProcurementState.Kind == "Принят")
                           .Where(p => p.RealDueDate == null)
                           .Where(p => (p.Amount ?? 0) < (p.ReserveContractAmount != null && p.ReserveContractAmount != 0 ? p.ReserveContractAmount : p.ContractAmount))
                           .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

                public static async Task<List<Procurement>?> NotPaid() // Получить список неоплаченных тендеров
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    try
                    {
                        procurements = await All()
                        .Where(p => p.ProcurementState.Kind == "Принят")
                        .Where(p => p.RealDueDate == null)
                        .Where(p => p.MaxDueDate != null)
                        .Where(p => (p.Amount ?? 0) < (p.ReserveContractAmount != null && p.ReserveContractAmount != 0 ? p.ReserveContractAmount : p.ContractAmount))
                        .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }
                public static async Task<List<Procurement>?> CalculationQueue() // Очередь расчета
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    try
                    {
                        procurements = await db.Procurements
                            .Include(p => p.ProcurementState)
                            .Include(p => p.Law)
                            .Where(p => p.ProcurementState.Kind == "Новый" && !db.ProcurementsEmployees.Any(pe => pe.ProcurementId == p.Id))
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }
                public static async Task<List<Procurement>?> ManagersQueue()
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    try
                    {
                        procurements = await db.Procurements
                            .Include(p => p.ProcurementState)
                            .Include(p => p.Law)
                            .Where(p => p.ProcurementState.Kind == "Выигран 1ч" || p.ProcurementState.Kind == "Выигран 2ч" && !db.ProcurementsEmployees.Any(pe => pe.ProcurementId == p.Id && pe.Employee.Position.Id == 8))
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }
            }

        }
    }
}
