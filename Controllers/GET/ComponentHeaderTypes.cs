using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<ComponentHeaderType>?> ComponentHeaderTypes() // Получить список заголовков расчета и закупки
        {
            using ParsethingContext db = new();
            List<ComponentHeaderType>? componentHeaderTypes = null;

            try { componentHeaderTypes = await db.ComponentHeaderTypes.ToListAsync(); }
            catch { }

            return componentHeaderTypes;
        }
    }
}
