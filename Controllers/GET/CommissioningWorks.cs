using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<CommisioningWork>?> CommissioningWorks() // Получить список пусконаладочных работ
        {
            using ParsethingContext db = new();
            List<CommisioningWork>? commissioningWorks = null;

            try { commissioningWorks = await db.CommisioningWorks.ToListAsync(); }
            catch { }

            return commissioningWorks;
        }
    }
}
