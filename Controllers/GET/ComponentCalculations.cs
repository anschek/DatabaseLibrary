using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class ComponentCalculations
        {
            public static async Task<List<ComponentCalculation>?> ManyByKind(string kind) // Получить список комплектующих по их статусу 
            {
                using ParsethingContext db = new();
                List<ComponentCalculation> componentCalculations = null;

                try
                {
                    componentCalculations = await db.ComponentCalculations
                        .Include(cc => cc.ComponentState)
                        .Include(cc => cc.ComponentHeaderType)
                        .Include(cc => cc.Procurement)
                        .Include(cc => cc.Procurement.Law)
                        .Include(cc => cc.Procurement.ProcurementState)
                        .Include(cc => cc.Procurement.Region)
                        .Include(cc => cc.Procurement.City)
                        .Include(cc => cc.Procurement.Method)
                        .Include(cc => cc.Procurement.Platform)
                        .Include(cc => cc.Procurement.TimeZone)
                        .Include(cc => cc.Procurement.Organization)
                        .Include(cc => cc.Procurement.ShipmentPlan)
                        .Include(cc => cc.Manufacturer)
                        .Include(cc => cc.Manufacturer.ManufacturerCountry)
                        .Include(cc => cc.ComponentType)
                        .Include(cc => cc.Seller)
                        .Where(cc => cc.ComponentState.Kind == kind)
                        .ToListAsync();
                }
                catch { }

                return componentCalculations;
            }

            public static async Task<List<ComponentCalculation>?> ManyByKindAndEmployee(string kind, int employeeId) //  Получить список комплектующих по их статусу и id сотрудника
            {
                using ParsethingContext db = new();
                List<ComponentCalculation> componentCalculations = null;

                try
                {
                    var procurementIds = await db.ProcurementsEmployees
                .Where(pe => pe.EmployeeId == employeeId)
                .Select(pe => pe.ProcurementId)
                .ToListAsync();

                    componentCalculations = await db.ComponentCalculations
                        .Include(cc => cc.ComponentState)
                        .Include(cc => cc.ComponentHeaderType)
                        .Include(cc => cc.Procurement)
                            .ThenInclude(p => p.Law)
                        .Include(cc => cc.Procurement)
                            .ThenInclude(p => p.ProcurementState)
                        .Include(cc => cc.Procurement)
                            .ThenInclude(p => p.Region)
                            .Include(cc => cc.Procurement)
                            .ThenInclude(p => p.City)
                        .Include(cc => cc.Procurement)
                            .ThenInclude(p => p.Method)
                        .Include(cc => cc.Procurement)
                            .ThenInclude(p => p.Platform)
                        .Include(cc => cc.Procurement)
                            .ThenInclude(p => p.TimeZone)
                        .Include(cc => cc.Procurement)
                            .ThenInclude(p => p.Organization)
                        .Include(cc => cc.Manufacturer)
                            .ThenInclude(m => m.ManufacturerCountry)
                        .Include(cc => cc.ComponentType)
                        .Include(cc => cc.Seller)
                        .Where(cc => cc.ComponentState.Kind == kind && procurementIds.Contains(cc.ProcurementId))
                        .ToListAsync();
                }
                catch { }

                return componentCalculations;
            }

            public static async Task<List<ComponentCalculation>?> ManyByProcurement(int procurementId) // Получить список комплектующих по конкретному тендеру
            {
                using ParsethingContext db = new();
                List<ComponentCalculation> componentCalculations = null;

                try
                {
                    componentCalculations = await db.ComponentCalculations
                        .Include(cc => cc.ComponentState)
                        .Include(cc => cc.ComponentHeaderType)
                        .Include(cc => cc.Procurement)
                        .Include(cc => cc.Procurement.Law)
                        .Include(cc => cc.Procurement.ProcurementState)
                        .Include(cc => cc.Procurement.Region)
                        .Include(cc => cc.Procurement.City)
                        .Include(cc => cc.Procurement.Method)
                        .Include(cc => cc.Procurement.Platform)
                        .Include(cc => cc.Procurement.TimeZone)
                        .Include(cc => cc.Procurement.Organization)
                        .Include(cc => cc.Manufacturer)
                            .ThenInclude(m => m.ManufacturerCountry)
                        .Include(cc => cc.ComponentType)
                        .Include(cc => cc.Seller)
                        .Where(cc => cc.ProcurementId == procurementId)
                        .ToListAsync();
                }
                catch { }

                return componentCalculations;
            }

            public static async Task<List<ComponentCalculation>?> ManyByProcurementsAndStatuses(List<int?> procurementIds, List<string> componentStatuses) // Получить список комплектующих по списку Id и статусов товара
            {
                using ParsethingContext db = new();
                List<ComponentCalculation> componentCalculations = null;

                try
                {
                    componentCalculations = await db.ComponentCalculations
                        .Include(cc => cc.ComponentState)
                        .Include(cc => cc.ComponentHeaderType)
                        .Include(cc => cc.Procurement)
                        .Include(cc => cc.Procurement.Law)
                        .Include(cc => cc.Procurement.ProcurementState)
                        .Include(cc => cc.Procurement.Region)
                        .Include(cc => cc.Procurement.City)
                        .Include(cc => cc.Procurement.Method)
                        .Include(cc => cc.Procurement.Platform)
                        .Include(cc => cc.Procurement.TimeZone)
                        .Include(cc => cc.Procurement.Organization)
                        .Include(cc => cc.Manufacturer)
                            .ThenInclude(m => m.ManufacturerCountry)
                        .Include(cc => cc.ComponentType)
                        .Include(cc => cc.Seller)
                        .Where(cc => procurementIds.Contains(cc.ProcurementId) && componentStatuses.Contains(cc.ComponentState.Kind))
                        .ToListAsync();
                }
                catch { }

                return componentCalculations;

            }

            public static async Task<List<ComponentCalculation>?> ManyByOneProcurementAndStatuses(int procurementId, List<string> componentStatuses) // Получить список комплектующих по конкретному тендеру и определенным статусам
            {
                using ParsethingContext db = new();
                List<ComponentCalculation> componentCalculations = null;

                try
                {
                    componentCalculations = await db.ComponentCalculations
                        .Include(cc => cc.ComponentState)
                        .Include(cc => cc.ComponentHeaderType)
                        .Include(cc => cc.Procurement)
                        .Include(cc => cc.Procurement.Law)
                        .Include(cc => cc.Procurement.ProcurementState)
                        .Include(cc => cc.Procurement.Region)
                        .Include(cc => cc.Procurement.City)
                        .Include(cc => cc.Procurement.Method)
                        .Include(cc => cc.Procurement.Platform)
                        .Include(cc => cc.Procurement.TimeZone)
                        .Include(cc => cc.Procurement.Organization)
                        .Include(cc => cc.Manufacturer)
                            .ThenInclude(m => m.ManufacturerCountry)
                        .Include(cc => cc.ComponentType)
                        .Include(cc => cc.Seller)
                        .Where(cc => cc.ProcurementId == procurementId)
                        .ToListAsync();
                }
                catch { }

                return componentCalculations;
            }

            public static async Task<int> CountByProcurement(int procurementId) // Получить количество позиций по id тендера
            {
                using ParsethingContext db = new();
                int count = 0;

                try
                {
                    count = await db.ComponentCalculations
                        .Where(cc => cc.ProcurementId == procurementId && cc.IsHeader == false && cc.IsAdded == false)
                        .CountAsync();
                }
                catch { }

                return count;
            }
        }
    }
}
