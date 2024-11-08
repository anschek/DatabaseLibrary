using DatabaseLibrary.Entities.ProcurementProperties;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public class ProcurementsGroup
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public decimal TotalAmount { get; set; }
            public int TenderCount { get; set; }
            public List<Procurement> Procurements { get; set; }
        }

        
        public static (List<ProcurementsGroup>?, List<Procurement>?) GroupByProcurementState(string procurementState, List<History> histories)
        {
            using ParsethingContext db = new();

            // Статусы, для которых считается InitialPrice
            var statusesForInitialPrice = new List<string> { "Новый", "Посчитан", "Оформлен", "Отправлен", "Отбой", "Отклонен" };

            // Обработка для статуса "Выигран 2ч"
            if (procurementState == "Выигран 2ч")
            {
                var excludedStatuses = new List<string> { "Проигран", "Отклонен", "Отбой", "Отмена" };

                var validWinningTenders = histories
                    .Where(h => h.Text == "Выигран 2ч")
                    .GroupBy(h => h.EntryId)
                    .Where(group =>
                    {
                        var statuses = group.ToList();
                        var lastWinIndex = statuses.FindLastIndex(h => h.Text == "Выигран 2ч");
                        if (lastWinIndex == -1) return false; // Не найден статус "Выигран 2ч"

                        // Убедимся, что после "Выигран 2ч" нет других исключенных статусов
                        return statuses.Skip(lastWinIndex + 1).All(h => !excludedStatuses.Contains(h.Text));
                    })
                    .Select(group => group.First())
                    .DistinctBy(h => h.EntryId)
                    .ToList();

                var procurements = GetProurementsByHistories(validWinningTenders, db);

                List<ProcurementsGroup> winningTendersByMonth = HistoryProcurementToGroup(validWinningTenders, procurements,
                    new Func<Procurement, decimal>(p =>
                    {
                        // Если есть резервная сумма, используем ее
                        if (p.ReserveContractAmount.HasValue) // Проверяем, что ReserveContractAmount не null
                            return p.ReserveContractAmount.Value; // Берем значение резервной суммы
                        else
                            return p.ContractAmount ?? 0; // Используем ContractAmount или 0, если он null

                    }));

                return (winningTendersByMonth, procurements);
            }

            // Логика для остальных состояний
            var relevantHistories = histories
                .Where(h => h.Text == procurementState)
                .ToList();

            var procurementsForOtherStates = GetProurementsByHistories(relevantHistories, db);

            List<ProcurementsGroup> tendersByMonth = HistoryProcurementToGroup(relevantHistories, procurementsForOtherStates,
                new Func<Procurement, decimal>(p =>
                    {
                        if (p.ReserveContractAmount != null) // Если резервная сумма не null
                            return p.ReserveContractAmount.Value; // Возвращаем значение резервной суммы
                        else if (statusesForInitialPrice.Contains(procurementState)) // Если статус требует InitialPrice
                            return p.InitialPrice; // Возвращаем InitialPrice, который всегда должен иметь значение
                        else
                            return p.ContractAmount ?? 0; // Если ContractAmount null, возвращаем 0

                    }));
            return (tendersByMonth, procurementsForOtherStates);
        }

        private static List<Procurement> GetProurementsByHistories(List<History> histories, ParsethingContext db)
        {
            var procurementIds = histories
                .Select(h => h.EntryId)
                .Distinct()
                .ToList();

            var procurements = db.Procurements
                .Where(p => procurementIds.Contains(p.Id))
                .Include(p => p.ProcurementState)
                .Include(p => p.Law)
                .Include(p => p.Method)
                .Include(p => p.Platform)
                .Include(p => p.TimeZone)
                .Include(p => p.Region)
                .Include(p => p.City)
                .Include(p => p.ShipmentPlan)
                .Include(p => p.Organization)
                .ToList();

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
