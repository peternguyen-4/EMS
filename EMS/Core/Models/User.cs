using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string role { get; set; }
        public string assignedZone { get; set; }

        public User(int userID, string userName, string password, string firstName, string lastName, string role, string assignedZone)
        {
            UserID = userID;
            this.userName = userName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.role = role;
            this.assignedZone = assignedZone;
        }
    }
}
