using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<ComponentType>?> ComponentTypes() // Получить все типы комплектующих 
        {
            using ParsethingContext db = new();
            List<ComponentType>? componentTypes = null;

            try
            {
                componentTypes = await db.ComponentTypes
                    .Include(ct => ct.PredefinedComponent)
                    .ToListAsync();
            }
            catch { }

            return componentTypes;
        }
    }
}
