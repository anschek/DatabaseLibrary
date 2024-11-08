using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> LegalEntity(LegalEntity legalEntity)
        {
            using ParsethingContext db = new();
            LegalEntity? def = null;
            bool isSaved = true;

            try
            {
                def = await db.LegalEntities
                    .Where(m => m.Id == legalEntity.Id)
                    .FirstAsync();

                def.Name = legalEntity.Name;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
