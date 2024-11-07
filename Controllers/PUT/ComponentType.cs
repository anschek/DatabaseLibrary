using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> ComponentType(ComponentType componentType)
        {
            using ParsethingContext db = new();
            ComponentType? def = null;
            bool isSaved = true;

            try
            {
                def = await db.ComponentTypes
                    .Where(ct => ct.Id == componentType.Id)
                    .FirstAsync();

                def.Kind = componentType.Kind;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
