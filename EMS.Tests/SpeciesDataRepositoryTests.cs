using EMS.Core.Data;
using EMS.Core.Models;
using EMS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace EMS.Tests
{
    [TestFixture]
    public class SpeciesDataRepositoryTests
    {
        private AppDbContext _context;
        private SpeciesDataRepository _repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Species")
                .Options;

            _context = new AppDbContext(options);
            _repo = new SpeciesDataRepository(_context);

            // Seed Species
            _context.Species.Add(new Species { speciesID = 1, speciesName = "Koala" });
            _context.SaveChanges();

            // Seed SpeciesData
            _context.SpeciesData.Add(new SpeciesData
            {
                sampleID = 1,
                date = new DateTime(2025, 1, 1),
                speciesID = 1,
                populationCount = 50,
                scatCount = 5,
                reproductiveFactor = 1.2f,
                knownHabitats = "Forest",
                healthConcerns = "None",
                additionalNotes = ""
            });
            _context.SaveChanges();
        }

        [Test]
        public void GetAll_ShouldReturnItems()
        {
            var result = _repo.GetAll();
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetById_ShouldReturnCorrectItem()
        {
            var result = _repo.GetById(1);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.populationCount, Is.EqualTo(50));
        }

        [Test]
        public void Add_ShouldIncreaseCount()
        {
            _repo.Add(new SpeciesData
            {
                sampleID = 2,
                date = new DateTime(2025, 1, 2),
                speciesID = 1,
                populationCount = 45,
                scatCount = 4,
                reproductiveFactor = 1.1f,
                knownHabitats = "Grassland",
                healthConcerns = "Minor",
                additionalNotes = "Test note"
            });

            var all = _repo.GetAll();
            Assert.That(all.Count, Is.EqualTo(2));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
