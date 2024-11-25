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
            public static async Task<bool> HasUnread(int employeeId)
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

