using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<PredefinedComponent>?> PredefinedComponents() // Получить список заготовленных позиций
        {
            using ParsethingContext db = new();
            List<PredefinedComponent>? predefinedComponents = null;

            try { predefinedComponents = await db.PredefinedComponents.ToListAsync(); }
            catch { }

            return predefinedComponents;
        }
    }
}
