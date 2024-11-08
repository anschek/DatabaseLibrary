using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class DELETE
    {
        public static async Task<bool> LegalEntity(LegalEntity legalEntity)
        {
            using ParsethingContext db = new();
            bool isSaved = true;
            try
            {
                _ = db.LegalEntities.Remove(legalEntity);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
