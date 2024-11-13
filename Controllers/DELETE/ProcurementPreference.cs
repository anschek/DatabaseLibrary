using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class DELETE
    {
        public static async Task<bool> ProcurementPreference(ProcurementsPreference procurementsPreference)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                var procurementToDelete = await db.ProcurementsPreferences.FirstOrDefaultAsync(pp => pp.ProcurementId == procurementsPreference.ProcurementId && pp.PreferenceId == procurementsPreference.PreferenceId);
                if (procurementsPreference != null)
                {
                    _ = db.ProcurementsPreferences.Remove(procurementToDelete);
                    _ = await db.SaveChangesAsync();
                }
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
