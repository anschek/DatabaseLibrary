using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> ProcurementState(ProcurementState procurementState)
        {
            using ParsethingContext db = new();
            ProcurementState? def = null;
            bool isSaved = true;

            try
            {
                def = await db.ProcurementStates
                    .Where(p => p.Id == procurementState.Id)
                    .FirstAsync();

                def.Kind = procurementState.Kind;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
