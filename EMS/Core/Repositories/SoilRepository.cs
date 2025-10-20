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
    public class SoilRepository : IRepository<SoilData>, IExportable
    {
        private readonly AppDbContext _context = new AppDbContext();

        public string Name => "Soil";

        public List<SoilData> GetAll()
        {
            return _context.SoilSamples.ToList();
        }

        public IEnumerable<object> GetAllForExport()
        {
            return GetAll();
        }

        public SoilData? GetById(int sampleID)
        {
            return _context.SoilSamples.Find(sampleID);
        }

        public void Add(SoilData soil)
        {
            _context.SoilSamples.Add(soil);
            _context.SaveChanges();
        }

        public void Update(SoilData soil)
        {
            _context.SoilSamples.Update(soil);
            _context.SaveChanges();
        }

        public void Delete(int sampleID)
        {
            var soil = _context.SoilSamples.Find(sampleID);
            if (soil != null)
            {
                _context.SoilSamples.Remove(soil);
                _context.SaveChanges();
            }
        }
    }
}
