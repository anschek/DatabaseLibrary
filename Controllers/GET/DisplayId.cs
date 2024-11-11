using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class DisplayId
        {
            public static async Task<int?> Max() // получить максимальный DisplayId
            {
                using ParsethingContext db = new();
                int? number = 0;

                try
                {
                    number = await db.Procurements
                        .MaxAsync(p => (int?)p.DisplayId);
                }
                catch { }

                return number;
            }
            public static async Task<int?> First(int? procurementId) // получить DisplayId по ProcurementId
            {
                using ParsethingContext db = new();
                int? number = 0;

                try
                {
                    number = await db.Procurements
                        .Where(p => p.Id == procurementId)
                        .Select(p => p.DisplayId)
                        .FirstOrDefaultAsync();
                }
                catch { }

                return number;
            }
        }
    }
}
