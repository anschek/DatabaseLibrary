using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static async Task<bool> ComponentCalculation(ComponentCalculation componentCalculation)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                _ = await db.ComponentCalculations.AddAsync(componentCalculation);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
