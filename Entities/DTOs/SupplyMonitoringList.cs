using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Entities.DTOs
{
    public class SupplyMonitoringList // Класс для формирования результатов запросов на получение списка сгруппированных комплектующих
    {
        public string? SupplierName { get; set; }
        public string? ManufacturerName { get; set; }
        public string? ComponentName { get; set; }
        public string? ComponentStatus { get; set; }
        public string? ShipmentPlan { get; set; }
        public decimal? AveragePrice { get; set; }
        public int? TotalCount { get; set; }
        public string? SellerName { get; set; }
        public int? TenderNumber { get; set; }
        public int? DisplayId { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
