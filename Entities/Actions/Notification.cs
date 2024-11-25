using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Entities.Actions
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime DateCreated { get; set; }
        public int EmployeeId { get; set; }
        public virtual ICollection<EmployeeNotification> EmployeeNotifications { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
    }
}