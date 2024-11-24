using DatabaseLibrary.Entities.DTOs;
using DatabaseLibrary.Entities.ProcurementProperties;
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
        public static partial class Procurements
        {
            public static class Count
            {
                public static async Task<int> CalculationQueue() // Очередь расчета (количество)
                {
                    using ParsethingContext db = new();
                    int count = 0;
                    try { count = await Queries.CalculationQueue(db).CountAsync(); }
                    catch { }

                    return count;
                }

                public static async Task<int> ManagersQueue() // Тендеры, не назначенные не конкретного менеджера (количество)
                {
                    using ParsethingContext db = new();
                    int count = 0;
                    try { count = await Queries.ManagersQueue(db).CountAsync(); }
                    catch { }

                    return count;
                }

                public static async Task<int> ByVisa(KindOf kindOf, bool stageCompleted) // Получить список тендеров по визе расчетников, закупки (количество)
                {
                    using ParsethingContext db = new();
                    int count = 0;

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

                        count = await db.Procurements
                            .Where(stagePredicate)
                            .Where(p => p.ProcurementState.Kind == "Выигран 1ч" || p.ProcurementState.Kind == "Выигран 2ч")
                            .CountAsync();
                    }
                    catch { }

                    return count;
                }

                public static async Task<int> ByKind(KindOf kindOf, string? kind = null) // kind остается null только для Application, Judgement и FAS (количество)
                {
                    using ParsethingContext db = new();
                    int count = 0;

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

                        count = await db.Procurements
                            .Include(p => p.ProcurementState)
                            .Include(p => p.ShipmentPlan)
                            .Where(additionalCondition)
                            .CountAsync();
                    }
                    catch { }

                    return count;
                }

                public static async Task<int> Accepted(bool isOverdue) // Получить количество неоплаченных тендеров
                {
                    using ParsethingContext db = new();
                    int count = 0;

                    try
                    {
                        // Предикат срока фильтрует тендеры по полю максимального срока в зависимости от значения переменной "Просрочено"
                        Expression<Func<Procurement, bool>> termPredicate = p => (isOverdue ? p.MaxDueDate < DateTime.Now : p.MaxDueDate > DateTime.Now);

                        count = await db.Procurements
                                .Include(p => p.ProcurementState)
                                .Where(termPredicate)
                                .Where(p => p.ProcurementState.Kind == "Принят")
                                .Where(p => p.RealDueDate == null)
                                .Where(p => (p.Amount ?? 0) < (p.ReserveContractAmount != null && p.ReserveContractAmount != 0 ? p.ReserveContractAmount : p.ContractAmount))
                                .CountAsync();
                    }
                    catch { }

                    return count;
                }

                public static async Task<int> ByDateKind(string procurementStateKind, bool isOverdue, KindOf kindOf) // Получить список тендеров:
                {
                    using ParsethingContext db = new();
                    int count = 0;

                    try
                    {
                        // Предикат типа фильтрует тендеры
                        Expression<Func<Procurement, bool>> kindPredicate = p =>
                            kindOf == KindOf.ContractConclusion
                            ? p.ProcurementState.Kind == "Выигран 1ч" || p.ProcurementState.Kind == "Выигран 2ч" // для даты заключения контракта по выигрышам
                            : p.ProcurementState.Kind == procurementStateKind; // для остальных дат по указанному типу статуса

                        // Прендикат срока фильтрует в соответствии с посроченными датами                                                       
                        Expression<Func<Procurement, bool>> termPredicate = Queries.TermPredicatByDateKind(isOverdue, kindOf);

                        count = await db.Procurements
                                   .Where(kindPredicate)
                                   .Where(termPredicate)
                                   .CountAsync();
                    }
                    catch { }

                    return count;
                }

                public static async Task<int> ByStateAndStartDate(string procurementState, DateTime startDate) // Получить тендеры по статусу и дате
                {
                    using ParsethingContext db = new();
                    int count = 0;

                    try { count = await Queries.ByStateAndStartDate(db, procurementState, startDate, false).CountAsync(); }
                    catch { }

                    return count;
                }
            }
        }
    }
}