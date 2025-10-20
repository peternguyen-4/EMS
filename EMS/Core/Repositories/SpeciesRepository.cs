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
    public class SpeciesRepository : IRepository<Species>
    {
        private readonly AppDbContext _context = new AppDbContext();

        public List<Species> GetAll()
        {
            return _context.Species.ToList();
        }

        public Species? GetById(int speciesID)
        {
            return _context.Species.Find(speciesID);
        }

        public void Add(Species species)
        {
            _context.Species.Add(species);
            _context.SaveChanges();
        }

        public void Update(Species species)
        {
            _context.Species.Update(species);
            _context.SaveChanges();
        }

        public void Delete(int speciesID)
        {
            var species = _context.Species.Find(speciesID);
            if (species != null)
            {
                _context.Species.Remove(species);
                _context.SaveChanges();
            }
        }
    }
}
