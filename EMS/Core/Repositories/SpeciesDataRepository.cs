using EMS.Core.Data;
using EMS.Core.Models;
using EMS.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Repositories
{
    public class SpeciesDataRepository : IRepository<SpeciesData>, IExportable
    {
        private readonly AppDbContext _context = new AppDbContext();

        public string Name => "Species";

        public List<SpeciesData> GetAll()
        {
            return _context.SpeciesData.Include(sd => sd.species).ToList();
        }

        public IEnumerable<object> GetAllForExport()
        {
            return GetAll().Select(sd => new
            {
                sd.sampleID,
                sd.date,
                Species = sd.species.speciesName, 
                sd.populationCount,
                sd.scatCount,
                sd.reproductiveFactor,
                sd.knownHabitats,
                sd.healthConcerns,
                sd.additionalNotes
            }).ToList();
        }


        public SpeciesData? GetById(int sampleID)
        {
            return _context.SpeciesData.Find(sampleID);
        }

        public List<SpeciesData> GetDataBySpecies(int speciesID)
        {
            return _context.SpeciesData
                .Where(sd => sd.speciesID == speciesID)
                .Include(sd => sd.species)
                .ToList();
        }

        public void Add(SpeciesData sd)
        {
            var speciesExists = _context.Species.Any(s => s.speciesID == sd.speciesID);
            if (!speciesExists)
            {
                throw new Exception("Species does not exist");
            }

            _context.SpeciesData.Add(sd);
            _context.SaveChanges();
        }

        public void Update(SpeciesData sd)
        {
            _context.SpeciesData.Update(sd);
            _context.SaveChanges();
        }

        public void Delete(int sampleID)
        {
            var sd = _context.SpeciesData.Find(sampleID);
            if (sd != null)
            {
                _context.SpeciesData.Remove(sd);
                _context.SaveChanges();
            }
        }
    }
}
