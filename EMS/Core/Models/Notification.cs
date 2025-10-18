using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    internal class Notification
    {
        public int notificationID { get; set; }
        public int userID { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime terminationDate { get; set; }
        public bool isActive { get; set; }
        public string title { get; set; }
        public string description { get; set; }

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
