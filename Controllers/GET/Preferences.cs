using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<Preference>?> Preferences() // Получить список префренций
        {
            using ParsethingContext db = new();
            List<Preference>? preferences = null;

            try { preferences = await db.Preferences.ToListAsync(); }
            catch { }

            return preferences;
        }
    }
}
