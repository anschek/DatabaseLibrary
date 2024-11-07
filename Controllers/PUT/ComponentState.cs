using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> ComponentState(ComponentState componentState)
        {
            using ParsethingContext db = new();
            ComponentState? def = null;
            bool isSaved = true;

            try
            {
                def = await db.ComponentStates
                    .Where(cs => cs.Id == componentState.Id)
                    .FirstAsync();

                def.Kind = componentState.Kind;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
