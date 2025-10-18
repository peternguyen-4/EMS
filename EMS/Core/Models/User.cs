using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public string assignedZone { get; set; } = string.Empty;

        public User() { }

        public User(int userID, string userName, string password, string firstName, string lastName, string role, string assignedZone)
        {
            this.userID = userID;
            this.userName = userName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.role = role;
            this.assignedZone = assignedZone;
        }
    }
}
