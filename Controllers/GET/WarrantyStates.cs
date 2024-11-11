using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<WarrantyState>?> WarrantyStates() // Получить список статусов БГ гарантии
        {
            using ParsethingContext db = new();
            List<WarrantyState>? warrantyStates = null;

            try { warrantyStates = await db.WarrantyStates.ToListAsync(); }
            catch { }

            return warrantyStates;
        }
    }
}
