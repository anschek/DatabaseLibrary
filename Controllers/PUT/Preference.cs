using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> Preference(Preference preference)
        {
            using ParsethingContext db = new();
            Preference? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Preferences
                    .Where(p => p.Id == preference.Id)
                    .FirstAsync();

                def.Kind = preference.Kind;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
