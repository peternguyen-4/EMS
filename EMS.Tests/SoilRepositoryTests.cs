using EMS.Core.Data;
using EMS.Core.Models;
using EMS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EMS.Tests
{
    [TestFixture]
    public class SoilRepositoryTests
    {
        private AppDbContext _context;
        private SoilRepository _repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Soil")
                .Options;

            _context = new AppDbContext(options);
            _repo = new SoilRepository(_context);

            // Seed sample data
            _context.SoilSamples.Add(new SoilData
            {
                sampleID = 1,
                date = new System.DateTime(2025, 1, 1),
                pH = 6.8f,
                firmness = 5,
                density = 8.7f,
                moisture = 6.8f,
                nitrogen = 20.1f,
                organicMatter = 12.3f
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
            Assert.That(result.pH, Is.EqualTo(6.8f).Within(0.0001f));
        }

        [Test]
        public void Add_ShouldIncreaseCount()
        {
            _repo.Add(new SoilData
            {
                sampleID = 2,
                date = new System.DateTime(2025, 1, 2),
                pH = 7.0f,
                firmness = 4,
                density = 2.7f,
                moisture = 4.5f,
                nitrogen = 10.3f,
                organicMatter = 8.8f
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
