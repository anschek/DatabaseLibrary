using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<LegalEntity>?> LegalEntities() // Получить список юридических лиц
        {
            using ParsethingContext db = new();
            List<LegalEntity>? legalEntities = null;

            try { legalEntities = await db.LegalEntities.ToListAsync(); }
            catch { }

            return legalEntities;
        }
    }
}
