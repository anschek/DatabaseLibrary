using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static async Task<bool> ProcurementState(ProcurementState procurementState)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                _ = await db.ProcurementStates.AddAsync(procurementState);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
