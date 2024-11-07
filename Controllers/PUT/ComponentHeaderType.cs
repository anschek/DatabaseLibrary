using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> ComponentHeaderType(ComponentHeaderType componentHeaderType)
        {
            using ParsethingContext db = new();
            ComponentHeaderType? def = null;
            bool isSaved = true;

            try
            {
                def = await db.ComponentHeaderTypes
                    .Where(ct => ct.Id == componentHeaderType.Id)
                    .FirstAsync();

                def.Kind = componentHeaderType.Kind;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
