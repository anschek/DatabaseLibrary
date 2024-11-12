using DatabaseLibrary.Entities.DTOs;
using DatabaseLibrary.Entities.ProcurementProperties;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Histories
        {
            public static async Task<List<History>?> Many() // Получить истории
            {
                using ParsethingContext db = new();
                List<History>? histories = null;

                try { histories = await db.Histories.ToListAsync(); }
                catch { }

                return histories;
            }
            public static async Task<List<History>?> ManyByProcurement(int procurementId) // Получить историю изменений статусов тендеров
            {
                using ParsethingContext db = new();
                List<History>? histories = null;

                try
                {
                    histories = await db.Histories
                        .Include(h => h.Employee)
                        .Where(h => h.EntryId == procurementId && h.EntityType == "Procurement")
                        .ToListAsync();
                }
                catch { }

                return histories;
            }
            public static async Task<int> CountNewStatusByProcurement(int procurementId)
            {
                using ParsethingContext db = new();
                int count = 0;
                try
                {
                    count = await db.Histories
                                .Where(h => h.EntryId == procurementId && h.EntityType == "Procurement" && h.Text == "Новый")
                                .CountAsync();
                }
                catch { }

                return count;
            }

            // Получение группировок по историям за один статус
            public static async Task<List<ProcurementsGroup>> GroupByProcurementState(string procurementState, List<History> histories)
            {
                using ParsethingContext db = new();

                // Статусы, для которых считается InitialPrice
                var statusesForInitialPrice = new List<string> { "Новый", "Посчитан", "Оформлен", "Отправлен", "Отбой", "Отклонен" };

                // 1. Обработка для статуса "Выигран 2ч"
                if (procurementState == "Выигран 2ч")
                {
                    var excludedStatuses = new List<string> { "Проигран", "Отклонен", "Отбой", "Отмена" };

                    // Получаем все EntryIds из истории, где присутствует статус "Выигран 2ч"
                    var winningEntryIds = histories
                        .Where(h => h.Text == "Выигран 2ч")
                        .Select(h => h.EntryId)
                        .Distinct()
                        .ToList();

                    // Извлекаем тендеры, у которых есть статус "Выигран 2ч" и ProcurementState.Kind не относится к исключенным статусам
                    var procurements = await GetProurementsByIds(winningEntryIds, db, p => !excludedStatuses.Contains(p.ProcurementState.Kind));

                    var winningProcurementsByMonth = histories.Where(h => winningEntryIds.Contains(h.EntryId) && h.Text == "Выигран 2ч").ToList();

                    // Делаем группировки по историям, тендерам, указываем формула расчета суммы
                    return HistoryProcurementToGroup(winningProcurementsByMonth, procurements,
                        p => p.ReserveContractAmount ?? p.ContractAmount ?? 0);
                }

                // 2. Логика для остальных состояний
                var relevantHistories = histories
                    .Where(h => h.Text == procurementState)
                    .ToList();

                var procurementIds = relevantHistories
                    .Select(h => h.EntryId)
                    .Distinct()
                    .ToList();

                // Извлекаем тендеры, у которых статус procurementState
                var procurementsForOtherStates = await GetProurementsByIds(procurementIds, db);

                // Делаем группировки по историям, тендерам, указываем формула расчета суммы
                return HistoryProcurementToGroup(relevantHistories, procurementsForOtherStates, p =>
                {
                    if (p.ReserveContractAmount != null) // Если резервная сумма не null
                        return p.ReserveContractAmount.Value; // Возвращаем значение резервной суммы
                    else if (statusesForInitialPrice.Contains(procurementState)) // Если статус требует InitialPrice
                        return p.InitialPrice; // Возвращаем InitialPrice, который всегда должен иметь значение
                    else
                        return p.ContractAmount ?? 0; // Если ContractAmount null, возвращаем 0
                });
            }

            // Получение группировок по историям за все статусы
            public static async Task<Dictionary<string, List<ProcurementsGroup>>> GroupByProcurementState(List<History> histories)
            {
                //using ParsethingContext db = new();

                // Статусы, для которых делаются группировки
                var targetStatuses = new List<string> { "Выигран 2ч", "Отправлен", "Оформлен", "Посчитан", "Новый", "Отбой", "Отклонен" };
                // Статусы, исключаемые для "Выигран 2ч"
                var excludedStatuses = new List<string> { "Проигран", "Отклонен", "Отбой", "Отмена" };
                // Статусы, для которых считается InitialPrice
                var statusesForInitialPrice = new List<string> { "Новый", "Посчитан", "Оформлен", "Отправлен", "Отбой", "Отклонен" };
                // Безопасная для потоков коллекция
                var resultGroups = new ConcurrentDictionary<string, List<ProcurementsGroup>>();

                // Дождемся обработки каждого статуса
                await Task.WhenAll(targetStatuses.Select(procurementState =>
                Task.Run(async () =>
                {
                    using ParsethingContext db = new(); // Создаем новый экземпляр для каждой задачи, т.к. контекст не потокобезопасен

                    List<ProcurementsGroup> resultGroup;
                    if (procurementState == "Выигран 2ч") // 1. Обработка для статуса "Выигран 2ч"
                    {
                        // Получаем все EntryIds из истории, где присутствует статус "Выигран 2ч"
                        var winningEntryIds = histories
                            .AsParallel()
                            .Where(h => h.Text == "Выигран 2ч")
                            .Select(h => h.EntryId)
                            .Distinct()
                            .ToList();

                        // Извлекаем тендеры, у которых есть статус "Выигран 2ч" и ProcurementState.Kind не относится к исключенным статусам
                        var procurements = await GetProurementsByIds(winningEntryIds, db, p => !excludedStatuses.Contains(p.ProcurementState.Kind));

                        var winningProcurementsByMonth = histories.AsParallel().Where(h => winningEntryIds.Contains(h.EntryId) && h.Text == "Выигран 2ч").ToList();

                        // Делаем группировки по историям, тендерам, указываем формула расчета суммы
                        resultGroup = HistoryProcurementToGroup(winningProcurementsByMonth, procurements,
                            p => p.ReserveContractAmount ?? p.ContractAmount ?? 0);
                    }
                    else // 2. Логика для остальных состояний
                    {
                        var relevantHistories = histories
                            .AsParallel()
                            .Where(h => h.Text == procurementState)
                            .ToList();

                        var procurementIds = relevantHistories
                            .AsParallel()
                            .Select(h => h.EntryId)
                            .Distinct()
                            .ToList();

                        // Извлекаем тендеры, у которых статус procurementState
                        var procurementsForOtherStates = await GetProurementsByIds(procurementIds, db);

                        // Делаем группировки по историям, тендерам, указываем формула расчета суммы
                        resultGroup = HistoryProcurementToGroup(relevantHistories, procurementsForOtherStates, p =>
                        {
                            if (p.ReserveContractAmount != null) // Если резервная сумма не null
                                return p.ReserveContractAmount.Value; // Возвращаем значение резервной суммы
                            else if (statusesForInitialPrice.Contains(procurementState)) // Если статус требует InitialPrice
                                return p.InitialPrice; // Возвращаем InitialPrice, который всегда должен иметь значение
                            else
                                return p.ContractAmount ?? 0; // Если ContractAmount null, возвращаем 0
                        });
                    }
                    resultGroups.TryAdd(procurementState, resultGroup);
                })));

                return resultGroups.ToDictionary(dict => dict.Key, dict => dict.Value);
            }

            private static async Task<List<Procurement>> GetProurementsByIds(
                List<int> procurementIds, 
                ParsethingContext db, 
                Expression<Func<Procurement, bool>> additionalCondition = null // Expression для возможности перевода делегата в sql
                )
            {
                // Дополнительно условие по умолчанию не должно влиять на результат при конъюнкции
                additionalCondition ??= (procurement => true);

                var procurements = await db.Procurements
                    .Where(p => procurementIds.Contains(p.Id))
                    .Where(additionalCondition)                 // задается новое условие, если не указано, никак не влияет
                    .Include(p => p.ProcurementState)
                    .Include(p => p.Law)
                    .Include(p => p.Method)
                    .Include(p => p.Platform)
                    .Include(p => p.TimeZone)
                    .Include(p => p.Region)
                    .Include(p => p.City)
                    .Include(p => p.ShipmentPlan)
                    .Include(p => p.Organization)
                    .ToListAsync();

                return procurements;
            }

            private static List<ProcurementsGroup> HistoryProcurementToGroup(List<History> histories, List<Procurement> procurement, Func<Procurement, decimal> calcTotalAmount)
            {
                List<ProcurementsGroup> tendersByMonth = histories
                   .GroupBy(tender => new { tender.Date.Year, tender.Date.Month })// Группировка по годам и месяцам
                   .Select(group =>
                   {
                       var procurementIdsInGroup = group.Select(h => h.EntryId).Distinct().ToList();
                       var procurementsInGroup = procurement
                           .Where(p => procurementIdsInGroup.Contains(p.Id))
                           .ToList();

                       // Подсчитываем сумму и количество тендеров
                       decimal totalAmount = procurementsInGroup.Sum(calcTotalAmount);

                       int tenderCount = procurementsInGroup.Count;

                       return new ProcurementsGroup { Year = group.Key.Year, Month = group.Key.Month, TotalAmount = totalAmount, TenderCount = tenderCount, Procurements = procurementsInGroup }; // Включаем список тендеров
                   })
                   .OrderBy(entry => entry.Year)
                   .ThenBy(entry => entry.Month)
                   .ToList();

                return tendersByMonth;
            }

        }

    }
}
