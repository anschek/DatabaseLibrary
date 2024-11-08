using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> PredefinedComponent(PredefinedComponent predefinedComponent)
        {
            using ParsethingContext db = new();
            PredefinedComponent? def = null;
            bool isSaved = true;

            try
            {
                def = await db.PredefinedComponents
                    .Where(p => p.Id == predefinedComponent.Id)
                    .FirstAsync();

                def.ComponentName = predefinedComponent.ComponentName;
                def.ManufacturerId = predefinedComponent.ManufacturerId;
                def.ComponentTypeId = predefinedComponent.ComponentTypeId;
                def.Price = predefinedComponent.Price;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
