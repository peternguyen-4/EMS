using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public class Notification
    {
        [Key]
        public int notificationID { get; set; }
        public int userID { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime terminationDate { get; set; }
        public bool isActive { get; set; }
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;

        public Notification() { }

        public Notification(int notificationID, int userID, DateTime creationDate, DateTime terminationDate, bool isActive, string title, string description)
        {
            this.notificationID = notificationID;
            this.userID = userID;
            this.creationDate = creationDate;
            this.terminationDate = terminationDate;
            this.isActive = isActive;
            this.title = title;
            this.description = description;
        }
    }
}
