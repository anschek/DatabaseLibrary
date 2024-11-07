using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static async Task<bool> ComponentState(ComponentState componentState)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                _ = await db.ComponentStates.AddAsync(componentState);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
