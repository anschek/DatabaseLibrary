using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Entities.Actions
{
    public partial class EmployeeNotification
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int NotificationId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public virtual Notification Notification { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
    }
}