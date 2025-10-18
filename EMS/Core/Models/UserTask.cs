using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public class UserTask
    {
        [Key]
        public int taskID { get; set; }
        public int userID { get; set; }
        public DateTime creationDate { get; set; }
        public bool isActive { get; set; }
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;

        public UserTask() { }

        public UserTask(int taskID, int userID, DateTime creationDate, bool isActive, string title, string description)
        {
            this.taskID = taskID;
            this.userID = userID;
            this.creationDate = creationDate;
            this.isActive = isActive;
            this.title = title;
            this.description = description;
        }
    }
}
