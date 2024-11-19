using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers.GET
{
    public static partial class ProcurementsEmployees
    {
        public static class Queries
        {
            public static IQueryable<ProcurementsEmployee> All()
            {
                using ParsethingContext db = new();
                return db.ProcurementsEmployees
                    .Include(pe => pe.Procurement)
                    .Include(pe => pe.Procurement.ProcurementState)
                    .Include(pe => pe.Employee)
                    .Include(pe => pe.Procurement.Law)
                    .Include(pe => pe.Procurement.Method)
                    .Include(pe => pe.Procurement.Platform)
                    .Include(pe => pe.Procurement.TimeZone)
                    .Include(pe => pe.Procurement.Region)
                    .Include(pe => pe.Procurement.City)
                    .Include(pe => pe.Procurement.Organization);
            }
        }
    }
}
