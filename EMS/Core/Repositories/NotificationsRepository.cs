using EMS.Core.Data;
using EMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Repositories
{
    public class NotificationsRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public List<Notification> GetAll()
        {
            return _context.Notifications.ToList();
        }

        public Notification? GetById(int notificationID)
        {
            return _context.Notifications.Find(notificationID);
        }

        public List<Notification> GetActiveByUser(int userID)
        {
            return _context.Notifications
                .Where(n => n.userID == userID && n.isActive)
                .OrderByDescending(n => n.creationDate)
                .ToList();
        }

        public void Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public void Update(Notification notification)
        {
            _context.Notifications.Update(notification);
            _context.SaveChanges();
        }

        public void Terminate(int notificationID)
        {
            var notification = _context.Notifications.Find(notificationID);
            if (notification != null)
            {
                notification.isActive = false;
                notification.terminationDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void Delete(int notificationID)
        {
            var notification = _context.Notifications.Find(notificationID);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                _context.SaveChanges();
            }
        }
    }
}
