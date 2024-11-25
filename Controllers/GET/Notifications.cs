using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Notifications
        {
            public static async Task<List<Notification>?> Many(int employeeId) // Получить уведомления для конкретного пользователя
            {
                using ParsethingContext db = new();
                List<Notification>? notifications = null;
                try
                {
                    notifications = await db.EmployeeNotifications
                        .Where(en => en.EmployeeId == employeeId && !en.IsRead)
                        .OrderByDescending(en => en.Notification.DateCreated)
                        .Select(en => en.Notification)
                        .ToListAsync();
                }
                catch { }
                return notifications;
            }

            public static async Task<bool> EmployeeHasUnread(int employeeId)
            {
                using ParsethingContext db = new();
                bool hasUnreadNotifications = false;
                try
                {
                    hasUnreadNotifications = await db.EmployeeNotifications
                        .AnyAsync(en => en.EmployeeId == employeeId && !en.IsRead);
                }
                catch { }
                return hasUnreadNotifications;
            }

        }

    }
}

