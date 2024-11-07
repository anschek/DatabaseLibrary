using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<ComponentState>?> ComponentStates() // Получить все статусы тендеров
        {
            using ParsethingContext db = new();
            List<ComponentState>? componentStates = null;

            try { componentStates = await db.ComponentStates.ToListAsync(); }
            catch { }

            return componentStates;
        }
    }
}
