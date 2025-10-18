using EMS.Core.Data;
using EMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.userName == username);
        }

        public bool ValidateUser(string username, string password)
        {
            var user = GetUserByUsername(username);
            if (user == null)
            {
                return false;
            }

            return user.password == password;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int userID)
        {
            var user = _context.Users.Find(userID);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void ChangePassword(int userID, string newPassword)
        {
            var user = _context.Users.First(u => u.userID == userID);
            user.password = newPassword;
            _context.SaveChanges();
        }
    }
}
