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
            public static class Queries
            {
                public static IQueryable<Procurement> All()
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

                public static IQueryable<Procurement> CalculationQueue() // Очередь расчета
                {
                    using ParsethingContext db = new();
                    return db.Procurements
                            .Include(p => p.ProcurementState)
                            .Include(p => p.Law)
                            .Where(p => p.ProcurementState.Kind == "Новый" && !db.ProcurementsEmployees.Any(pe => pe.ProcurementId == p.Id));

                }

                public static IQueryable<Procurement> ManagersQueue() // Тендеры, не назначенные не конкретного менеджера
                {
                    using ParsethingContext db = new();
                    return db.Procurements
                            .Include(p => p.ProcurementState)
                            .Include(p => p.Law)
                            .Where(p => p.ProcurementState.Kind == "Выигран 1ч" || p.ProcurementState.Kind == "Выигран 2ч" && !db.ProcurementsEmployees.Any(pe => pe.ProcurementId == p.Id && pe.Employee.Position.Id == 8));
                }

                public static Expression<Func<Procurement, bool>> TermPredicatByDateKind(bool isOverdue, KindOf kindOf)
                {
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
                    return termPredicate;
                }

                public static IQueryable<Procurement> ByStateAndStartDate(string procurementState, DateTime startDate, bool allInclusive)
                {
                    using ParsethingContext db = new();
                    IQueryable<int> validProcurementIds;

                    if (procurementState == "Выигран 1ч")
                    {
                        var excludedStatuses = new List<string> { "Проигран", "Отклонен", "Отмена" };

                        validProcurementIds = db.Histories
                            .Where(h => h.Text == procurementState && h.Date >= startDate)
                            .GroupBy(h => h.EntryId)
                            .Where(g => !g.Any(h => excludedStatuses.Contains(h.Text)))
                            .Select(g => g.Key)
                            .Distinct();
                    }
                    else
                    {
                        validProcurementIds = db.Histories
                            .Where(h => h.Text == procurementState && h.Date >= startDate)
                            .Select(h => h.EntryId)
                            .Distinct();
                    }

                    // При настройке "всю включено", подтягиваются все смежные таблицы
                    return (allInclusive ? All() : db.Procurements)
                        .Where(p => validProcurementIds.Contains(p.Id));
                }
            }
        }
    }
}