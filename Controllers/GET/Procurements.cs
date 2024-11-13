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
                        procurement = await db.Procurements
                            .Include(p => p.ProcurementState)
                            .Include(p => p.Law)
                            .Include(p => p.Method)
                            .Include(p => p.Platform)
                            .Include(p => p.TimeZone)
                            .Include(p => p.Region)
                            .Include(p => p.ShipmentPlan)
                            .Include(p => p.Organization)
                            .Include(p => p.City)
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

                public static async Task<List<Procurement>?> ByKind(string kind, KindOf kindOf) // Получить список тендеров по: 
                {
                    using ParsethingContext db = new();
                    List<Procurement>? procurements = null;
                    Expression<Func<Procurement, bool>> additionalCondition = p => true;

                    try
                    {
                        switch (kindOf)
                        {
                            case KindOf.ProcurementState: // Статусу
                                additionalCondition = 
                                    p => p.Applications != true &&
                                         p.ProcurementState != null &&
                                         p.ProcurementState.Kind == kind;

                                break;
                            case KindOf.ShipmentPlane: // Плану отгрузки
                                additionalCondition = 
                                    p => p.Applications != true &&
                                         p.ShipmentPlan != null && 
                                         p.ShipmentPlan.Kind == kind &&
                                         p.ProcurementState.Kind == "Выигран 2ч";

                                break;
                            case KindOf.Applications: // Выигранным по заявкам
                                additionalCondition =
                                    p => p.Applications == true;

                                break;
                            case KindOf.CorrectionDate: // Тендерам на исправлении
                                additionalCondition =
                                    p => p.CorrectionDate != null && 
                                    p.ProcurementState.Kind == kind;

                                break;
                        }

                        procurements = await db.Procurements
                            .Include(p => p.ProcurementState)
                            .Include(p => p.Law)
                            .Include(p => p.Method)
                            .Include(p => p.Platform)
                            .Include(p => p.TimeZone)
                            .Include(p => p.Organization)
                            .Include(p => p.Region)
                            .Include(p => p.City)
                            .Include(p => p.ShipmentPlan)
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

                        procurements = await db.Procurements
                            .Include(p => p.ProcurementState)
                            .Include(p => p.Law)
                            .Include(p => p.Method)
                            .Include(p => p.Platform)
                            .Include(p => p.Organization)
                            .Include(p => p.TimeZone)
                            .Include(p => p.Region)
                            .Include(p => p.City)
                            .Include(p => p.ShipmentPlan)
                            .Where(p => validProcurementIds.Contains(p.Id))
                            .ToListAsync();
                    }
                    catch { }

                    return procurements;
                }

            }

        }
    }
}
