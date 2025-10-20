using EMS.Core.Data;
using EMS.Core.Models;
using EMS.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Repositories
{
    public class WaterRepository : IRepository<WaterData>, IExportable
    {
        private readonly AppDbContext _context = new AppDbContext();

        public string Name => "Water";

        public List<WaterData> GetAll()
        {
            return _context.WaterSamples.ToList();
        }

        public IEnumerable<object> GetAllForExport()
        {
            return GetAll();
        }

        public WaterData? GetById(int sampleID)
        {
            return _context.WaterSamples.Find(sampleID);
        }

        public void Add(WaterData water)
        {
            _context.WaterSamples.Add(water);
            _context.SaveChanges();
        }

        public void Update(WaterData water)
        {
            _context.WaterSamples.Update(water);
            _context.SaveChanges();
        }

        public void Delete(int sampleID)
        {
            var water = _context.WaterSamples.Find(sampleID);
            if (water != null)
            {
                _context.WaterSamples.Remove(water);
                _context.SaveChanges();
            }
        }
    }
}
