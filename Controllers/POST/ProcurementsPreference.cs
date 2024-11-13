using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static async Task<bool> ProcurementsPreference(ProcurementsPreference procurementsPreference)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                if (procurementsPreference.Procurement != null && procurementsPreference.Preference != null)
                {
                    procurementsPreference.ProcurementId = procurementsPreference.Procurement.Id;
                    procurementsPreference.PreferenceId = procurementsPreference.Preference.Id;
                }
                else throw new Exception();
                procurementsPreference.Procurement = null;
                procurementsPreference.Preference = null;

                _ = await db.ProcurementsPreferences.AddAsync(procurementsPreference);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
