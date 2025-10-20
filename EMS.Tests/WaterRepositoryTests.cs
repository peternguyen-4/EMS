using EMS.Core.Data;
using EMS.Core.Models;
using EMS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace EMS.Tests
{
    [TestFixture]
    public class WaterRepositoryTests
    {
        private AppDbContext _context;
        private WaterRepository _repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Water")
                .Options;

            _context = new AppDbContext(options);
            _repo = new WaterRepository(_context);

            // Seed sample data
            _context.WaterSamples.Add(new WaterData
            {
                sampleID = 1,
                date = new System.DateTime(2025, 1, 1),
                pH = 7.2f,
                dissolvedOxygen = 8.1f,
                salinity = 3.4f,
                turbidity = 1.1f,
                hardness = 2.2f,
                eutrophicPotential = 0.4f
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
            Assert.That(result.pH, Is.EqualTo(7.2).Within(0.0001f));
        }

        [Test]
        public void Add_ShouldIncreaseCount()
        {
            _repo.Add(new WaterData
            {
                sampleID = 2,
                date = new System.DateTime(2025, 1, 2),
                pH = 6.9f,
                dissolvedOxygen = 7.9f,
                salinity = 3.2f,
                turbidity = 1.0f,
                hardness = 2.1f,
                eutrophicPotential = 0.3f
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
