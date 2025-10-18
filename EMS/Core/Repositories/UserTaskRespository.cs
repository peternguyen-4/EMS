using EMS.Core.Data;
using EMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Repositories
{
    public class UserTaskRespository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public List<UserTask> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public UserTask? GetById(int taskID)
        {
            return _context.Tasks.Find(taskID);
        }

        public List<UserTask> GetActiveByUser(int userID)
        {
            return _context.Tasks
                .Where(t => t.userID == userID && t.isActive)
                .OrderByDescending(t => t.creationDate)
                .ToList();
        }

        public void Add(UserTask task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void Update(UserTask task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void Terminate(int taskID)
        {
            var task = _context.Tasks.Find(taskID);
            if (task != null)
            {
                task.isActive = false;
                _context.SaveChanges();
            }
        }

        public void Delete(int taskID)
        {
            var task = _context.Tasks.Find(taskID);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
