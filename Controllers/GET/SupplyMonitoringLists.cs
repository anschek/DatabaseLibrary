using DatabaseLibrary.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static partial class GET
    {
        public static async Task<List<SupplyMonitoringList>> GetSupplyMonitoringLists(List<Procurement> procurements, List<string> componentStatuses)
        {
            using ParsethingContext db = new();
            List<SupplyMonitoringList> supplyMonitoringLists = new();

            try
            {
                var tenderIds = procurements.Select(p => p.Id).ToList();

                var query = from cc in db.ComponentCalculations
                            join s  in db.Sellers on cc.SellerIdPurchase equals s.Id into sellers
                            from s  in sellers.DefaultIfEmpty()
                            join m  in db.Manufacturers on cc.ManufacturerIdPurchase equals m.Id into manufacturers
                            from m  in manufacturers.DefaultIfEmpty()
                            join cs in db.ComponentStates on cc.ComponentStateId equals cs.Id
                            join p  in db.Procurements on cc.ProcurementId equals p.Id
                            where tenderIds.Contains(cc.ProcurementId) &&
                                  (componentStatuses == null || componentStatuses.Contains(cs.Kind)) &&
                                  (cc.IsDeleted == false || cc.IsDeleted == null)
                            select new
                            {
                                cc,
                                s,
                                m,
                                cs,
                                p
                            };

                var queryResult = await query.ToListAsync();

                supplyMonitoringLists = queryResult
                    .Select(item => new SupplyMonitoringList
                    {
                        SupplierName = item.s?.Name ?? "Без поставщика",
                        ManufacturerName = item.m?.ManufacturerName ?? "Без производителя",
                        ComponentName = item.cc.ComponentNamePurchase,
                        ComponentStatus = item.cs.Kind,
                        AveragePrice = item.cc.PricePurchase,
                        TotalCount = item.cc.CountPurchase,
                        SellerName = item.s?.Name ?? "Не указан",
                        TenderNumber = item.cc.ProcurementId,
                        DisplayId = item.p.DisplayId,
                        TotalAmount = item.cc.PricePurchase * item.cc.CountPurchase
                    })
                    .OrderBy(s => s.SupplierName == "Без поставщика" ? "" : s.SupplierName)
                    .ThenBy(s => s.SupplierName)
                    .ToList();
            }
            catch { }

            return supplyMonitoringLists;
        }
    }

}
