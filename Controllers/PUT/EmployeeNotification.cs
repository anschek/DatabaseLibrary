using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> EmployeeNotification(EmployeeNotification employeeNotification)
        {
            using ParsethingContext db = new();
            EmployeeNotification? def = null;
            bool isSaved = true;
            try
            {
                def = await db.EmployeeNotifications
                    .Where(en => en.Id == employeeNotification.Id)
                    .FirstAsync();
                def.IsRead = true;
                def.DateRead = DateTime.Now;
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }
            return isSaved;
        }
    }
}
