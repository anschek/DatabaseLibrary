using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<ProcurementsPreference>?> ProcurementsPreferences(int procurementId) // Получить преференции по конкретному тендеру
        {
            using ParsethingContext db = new();
            List<ProcurementsPreference>? procurementsPreferences = null;

            try
            {
                procurementsPreferences = await db.ProcurementsPreferences
                    .Include(pp => pp.Procurement)
                    .Include(pp => pp.Preference)
                    .Where(pe => pe.ProcurementId == procurementId)
                    .ToListAsync();
            }
            catch { }

            return procurementsPreferences;
        }
    }
}
