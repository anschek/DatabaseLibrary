using DatabaseLibrary.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static partial class GET
    {
        public static partial class Procurements
        {
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

                public static async Task<List<Procurement>?> ByKind(KindOf kindOf, string? kind = null) // kind остается null только для Application, Judgement и FAS
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

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

                        procurements = await Queries.All(db)
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

                    try { procurements = await Queries.ByStateAndStartDate(db, procurementState, startDate, true).ToListAsync(); }
                    catch { }

                    return procurements;
                }

                public static async Task<List<Procurement>?> ByDateKind(KindOf kindOf, bool isOverdue, string? procurementStateKind = null) // procurementStateKind не заполняется для ContractConclusion
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
                        Expression<Func<Procurement, bool>> termPredicate = Queries.TermPredicatByDateKind(isOverdue, kindOf);

                        procurements = await Queries.All(db)
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

                        procurements = await Queries.All(db)
                            .Where(stagePredicate)
                            .Where(p => p.ProcurementState.Kind == "Выигран 1ч" || p.ProcurementState.Kind == "Выигран 2ч")
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

                public static async Task<List<Procurement>?> Accepted(bool? isOverdue = null) // Получить список приянятых неоплаченых тендеров
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    try
                    {
                        // Предикат срока фильтрует тендеры по полю максимального срока в зависимости от значения переменной "Просрочено"
                        Expression<Func<Procurement, bool>> termPredicate = isOverdue switch
                        {
                            null => p => p.MaxDueDate != null,          // любой срок, если isOverdue не указано
                            true => p => p.MaxDueDate < DateTime.Now,  // просрочено
                            false => p => p.MaxDueDate > DateTime.Now // в срок
                        };

                        procurements = await Queries.All(db)
                           .Where(termPredicate)
                           .Where(p => p.ProcurementState.Kind == "Принят")
                           .Where(p => p.RealDueDate == null)
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
                    { procurements = await Queries.CalculationQueue(db).ToListAsync(); }
                    catch { }

                    return procurements;
                }

                public static async Task<List<Procurement>?> ManagersQueue()
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;
                    try { procurements = await Queries.ManagersQueue(db).ToListAsync(); }
                    catch { }

                    return procurements;
                }

                public static async Task<List<Procurement>?> SearchQuery(
                string searchIds, string searchNumber, string searchLaw, string searchProcurementState, string searchProcurementStateSecond,
                string searchInn, string searchEmployeeName, string searchOrganizationName,
                string searchLegalEntity, string dateType, string searchStartDate,
                string searchEndDate, string sortBy, bool ascending,
                string searchComponentCalculation, string searchShipmentPlan, bool? searchWaitingList,
                string searchContractNumber)
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;

                    var procurementQuery = db.Procurements.AsQueryable();

                    var ids = searchIds.Split(',')
                                       .Select(id => int.TryParse(id.Trim(), out int intId) ? intId : (int?)null)
                                       .Where(id => id.HasValue)
                                       .Select(id => id.Value)
                                       .ToList();

                    if (ids.Count == 0 && string.IsNullOrEmpty(searchEmployeeName)
                        && string.IsNullOrEmpty(searchNumber) && string.IsNullOrEmpty(searchLaw)
                        && string.IsNullOrEmpty(searchProcurementState) && string.IsNullOrEmpty(searchInn)
                        && string.IsNullOrEmpty(searchOrganizationName) && string.IsNullOrEmpty(searchLegalEntity)
                        && string.IsNullOrEmpty(dateType) && string.IsNullOrEmpty(searchComponentCalculation)
                        && string.IsNullOrEmpty(searchShipmentPlan) && searchWaitingList != true
                        && string.IsNullOrEmpty(searchContractNumber))
                        return new List<Procurement>();

                    if (ids.Count > 0)
                    {
                        var nullableIds = ids.Select(id => (int?)id).ToList();
                        procurementQuery = procurementQuery.Where(p => p.DisplayId.HasValue && nullableIds.Contains(p.DisplayId));
                    }

                    if (!string.IsNullOrEmpty(searchNumber))
                        procurementQuery = procurementQuery.Where(p => p.Number == searchNumber);
                    if (!string.IsNullOrEmpty(searchLaw))
                        procurementQuery = procurementQuery.Where(p => p.Law.Number == searchLaw);
                    if (!string.IsNullOrEmpty(searchProcurementState) && dateType != "HistoryDate")
                        procurementQuery = procurementQuery.Where(p => p.ProcurementState.Kind == searchProcurementState);
                    if (!string.IsNullOrEmpty(searchInn))
                        procurementQuery = procurementQuery.Where(p => p.Inn == searchInn);
                    if (!string.IsNullOrEmpty(searchOrganizationName))
                        procurementQuery = procurementQuery.Where(p => p.Organization.Name.Contains(searchOrganizationName));
                    if (!string.IsNullOrEmpty(searchEmployeeName))
                    {
                        var query = db.ProcurementsEmployees
                                      .Where(pe => pe.Employee.FullName == searchEmployeeName
                                       && pe.ActionType == "Appoint")
                                      .Select(pe => pe.ProcurementId)
                                      .ToList();

                        if (query.Count == 0)
                            return new List<Procurement>();

                        procurementQuery = procurementQuery.Where(p => query.Contains(p.Id));
                    }
                    if (!string.IsNullOrEmpty(searchLegalEntity))
                        procurementQuery = procurementQuery.Where(p => p.LegalEntity.Name.Contains(searchLegalEntity));

                    if (!string.IsNullOrEmpty(dateType))
                    {
                        DateTime? startDateTime = null;
                        DateTime? endDateTime = null;

                        if (!string.IsNullOrEmpty(searchStartDate) && DateTime.TryParse(searchStartDate, out DateTime parsedStartDate))
                            startDateTime = parsedStartDate;

                        if (!string.IsNullOrEmpty(searchEndDate) && DateTime.TryParse(searchEndDate, out DateTime parsedEndDate))
                            endDateTime = parsedEndDate.AddDays(1);

                        if (startDateTime.HasValue && endDateTime.HasValue)
                        {
                            switch (dateType)
                            {
                                case "StartDate":
                                    procurementQuery = procurementQuery.Where(p => p.StartDate >= startDateTime && p.StartDate < endDateTime);
                                    break;
                                case "Deadline":
                                    procurementQuery = procurementQuery.Where(p => p.Deadline >= startDateTime && p.Deadline < endDateTime);
                                    break;
                                case "ResultDate":
                                    procurementQuery = procurementQuery.Where(p => p.ResultDate >= startDateTime && p.ResultDate < endDateTime);
                                    break;
                                case "SigningDeadline":
                                    procurementQuery = procurementQuery.Where(p => p.SigningDeadline >= startDateTime && p.SigningDeadline < endDateTime);
                                    break;
                                case "ActualDeliveryDate":
                                    procurementQuery = procurementQuery.Where(p => p.ActualDeliveryDate >= startDateTime && p.ActualDeliveryDate < endDateTime);
                                    break;
                                case "MaxAcceptanceDate":
                                    procurementQuery = procurementQuery.Where(p => p.MaxAcceptanceDate >= startDateTime && p.MaxAcceptanceDate < endDateTime);
                                    break;
                                case "HistoryDate":
                                    if (!string.IsNullOrEmpty(searchProcurementState))
                                    {
                                        var procurementIds = db.Histories
                                            .Where(h => h.Text == searchProcurementState &&
                                                        h.Date >= startDateTime && h.Date < endDateTime)
                                            .Select(h => h.EntryId)
                                            .Distinct()
                                            .ToList();

                                        procurementQuery = procurementQuery.Where(p => procurementIds.Contains(p.Id));
                                    }

                                    // Если `searchProcurementStateSecond` не пустое, добавляем фильтрацию по ProcurementState.Kind
                                    if (!string.IsNullOrEmpty(searchProcurementStateSecond))
                                    {
                                        procurementQuery = procurementQuery.Where(p => p.ProcurementState.Kind == searchProcurementStateSecond);
                                    }
                                    break;
                                case "PayDate":
                                    procurementQuery = procurementQuery.Where(p => p.RealDueDate >= startDateTime && p.RealDueDate <= endDateTime && p.ProcurementState.Kind == "Принят");
                                    break;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(searchShipmentPlan))
                        procurementQuery = procurementQuery.Where(p => p.ShipmentPlan.Kind == searchShipmentPlan);
                    if (!string.IsNullOrEmpty(searchComponentCalculation))
                    {
                        var query = db.ComponentCalculations
                                      .Where(cc => cc.ComponentName.ToLower().Contains(searchComponentCalculation.ToLower()) || cc.ComponentNamePurchase.ToLower().Contains(searchComponentCalculation.ToLower()))
                                      .Select(cc => cc.ProcurementId)
                                      .ToList();

                        if (query.Count == 0)
                            return new List<Procurement>();

                        procurementQuery = procurementQuery.Where(p => query.Contains(p.Id));
                    }
                    if (searchWaitingList.HasValue)
                        if (searchWaitingList.Value)
                            procurementQuery = procurementQuery.Where(p => p.WaitingList == true);
                    if (!string.IsNullOrEmpty(searchContractNumber))
                        procurementQuery = procurementQuery.Where(p => p.ContractNumber.Contains(searchContractNumber));
                    procurementQuery = procurementQuery
                        .Include(p => p.ProcurementState)
                        .Include(p => p.Law)
                        .Include(p => p.Method)
                        .Include(p => p.Platform)
                        .Include(p => p.TimeZone)
                        .Include(p => p.Region)
                        .Include(p => p.City)
                        .Include(p => p.ShipmentPlan)
                        .Include(p => p.Organization);

                    procurements = await procurementQuery
                        .ToListAsync();

                    return procurements;
                }
            }
        }
    }
}