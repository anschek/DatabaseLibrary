using DatabaseLibrary.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers.GET
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

                    procurements = await Queries.All()
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

                try { procurements = await Queries.ByStateAndStartDate(procurementState, startDate, true).ToListAsync(); }
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
                    Expression<Func<Procurement, bool>> termPredicate = Queries.TermPredicatByDateKind(isOverdue, kindOf);

                    procurements = await Queries.All()
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

                    procurements = await Queries.All()
                        .Where(stagePredicate)
                        .Where(p => p.ProcurementState.Kind == "Выигран 1ч" || p.ProcurementState.Kind == "Выигран 2ч")
                        .ToListAsync();
                }
                catch { }

                return procurements;
            }

            //public static async Task<List<Procurement>?> AcceptedByOverdue(bool isOverdue) // Получить список приянятых неоплаченых тендеров
            //{
            //    using ParsethingContext db = new();
            //    List<Procurement>? procurements = null;

            //    try
            //    {
            //        // Предикат срока фильтрует тендеры по полю максимального срока в зависимости от значения переменной "Просрочено"
            //        Expression<Func<Procurement, bool>> termPredicate =
            //            p => (isOverdue ? p.MaxDueDate < DateTime.Now : p.MaxDueDate > DateTime.Now);

            //        procurements = await Queries.All()
            //           .Where(termPredicate)
            //           .Where(p => p.ProcurementState.Kind == "Принят")
            //           .Where(p => p.RealDueDate == null)
            //           .Where(p => (p.Amount ?? 0) < (p.ReserveContractAmount != null && p.ReserveContractAmount != 0 ? p.ReserveContractAmount : p.ContractAmount))
            //           .ToListAsync();
            //    }
            //    catch { }

            //    return procurements;
            //}

            //public static async Task<List<Procurement>?> NotPaid() // Получить список неоплаченных тендеров
            //{
            //    using ParsethingContext db = new();
            //    List<Procurement>? procurements = null;

            //    try
            //    {
            //        procurements = await Queries.All()
            //        .Where(p => p.ProcurementState.Kind == "Принят")
            //        .Where(p => p.RealDueDate == null)
            //        .Where(p => p.MaxDueDate != null)
            //        .Where(p => (p.Amount ?? 0) < (p.ReserveContractAmount != null && p.ReserveContractAmount != 0 ? p.ReserveContractAmount : p.ContractAmount))
            //        .ToListAsync();
            //    }
            //    catch { }

            //    return procurements;
            //}

            // not paid + by(bool)
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

                    procurements = await Queries.All()
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
                List<Procurement>? procurements = null;
                try
                { procurements = await Queries.CalculationQueue().ToListAsync(); }
                catch { }

                return procurements;
            }

            public static async Task<List<Procurement>?> ManagersQueue()
            {
                List<Procurement>? procurements = null;
                try { procurements = await Queries.ManagersQueue().ToListAsync(); }
                catch { }

                return procurements;
            }
        }
    }
}
