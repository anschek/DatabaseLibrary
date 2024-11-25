using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<EmployeeNotification>?> EmployeeNotifications(int employeeId) // Получить уведомления для конкретного пользователя
        {
            using ParsethingContext db = new();
            List<EmployeeNotification>? employeeNotifications = null;
            try
            {
                employeeNotifications = await db.EmployeeNotifications
                    .Include(en => en.Notification.Employee)
                    .Where(en => en.EmployeeId == employeeId && !en.IsRead)
                    .OrderBy(en => en.Notification.DateCreated)
                    .ToListAsync();
            }
            catch { }
            return employeeNotifications;
        }
    }
}
