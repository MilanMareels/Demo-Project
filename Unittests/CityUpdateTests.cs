using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Infrastructure.Contexts;
using AP.Demo_Project.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Unittests
{
    [TestClass]
    public sealed class CityUpdateTests
    {
        private DemoContext? _context;
        private CityRepository? _repository;
        private string? _dbName;
        private int _seededCityId;

        
        private DemoContext CreateContext(out string dbName)
        {
            dbName = $"DemoProject_Tests_{Guid.NewGuid():N}";
            var connString = $"Server=(localdb)\\mssqllocaldb;Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true";

            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseSqlServer(connString)
                .Options;

            var context = new DemoContext(options);
            context.Database.EnsureCreated(); // creates schema + seeding
            return context;
        }

        
        private void DropDatabase(string dbName)
        {
            try
            {
                var connString = $"Server=(localdb)\\mssqllocaldb;Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true";
                var options = new DbContextOptionsBuilder<DemoContext>()
                    .UseSqlServer(connString)
                    .Options;

                using var ctx = new DemoContext(options);
                ctx.Database.EnsureDeleted();
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }

        [TestInitialize]
        public void Setup()
        {
            _context = CreateContext(out _dbName);
            _repository = new CityRepository(_context);

            var city = new AP.Demo_Project.Domain.City
            {
                Name = "TestCity",
                Population = 1000,
                CountryId = 1
            };
            _context.Cities.Add(city);
            _context.SaveChanges();

            _seededCityId = city.Id;
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_dbName != null)
                DropDatabase(_dbName);
        }

        [TestMethod]
        public async Task UpdateCity_ShouldUpdateSuccessfully()
        {
            // Arrange
            var updateDto = new CityUpdateDTO
            {
                Id = _seededCityId,
                Population = 5000,
                CountryId = 2
            };

            // Act
            var result = await _repository!.UpdateCity(updateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateDto.Population, result.Population);
            Assert.AreEqual(updateDto.CountryId, result.CountryId);

            var updatedEntity = await _context!.Cities.FindAsync(updateDto.Id);
            Assert.AreEqual(5000, updatedEntity!.Population);
            Assert.AreEqual(2, updatedEntity.CountryId);
        }

        [TestMethod]
        public async Task UpdateCity_ShouldThrow_WhenPopulationInvalid()
        {
            // Arrange
            var updateDto = new CityUpdateDTO
            {
                Id = _seededCityId,
                Population = 0, // Invalid
                CountryId = 1
            };

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
                () => _repository!.UpdateCity(updateDto)
            );

            Assert.IsTrue(ex.Message.Contains("Population"));
        }

        [TestMethod]
        public async Task UpdateCity_ShouldThrow_WhenCountryIdInvalid()
        {
            // Arrange
            var updateDto = new CityUpdateDTO
            {
                Id = _seededCityId,
                Population = 1000,
                CountryId = 0 // Invalid
            };

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
                () => _repository!.UpdateCity(updateDto)
            );

            Assert.IsTrue(ex.Message.Contains("CountryId"));
        }

        [TestMethod]
        public async Task UpdateCity_ShouldThrow_WhenCityNotFound()
        {
            // Arrange
            var updateDto = new CityUpdateDTO
            {
                Id = 999, // Non-existent
                Population = 1000,
                CountryId = 1
            };

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<KeyNotFoundException>(
                () => _repository!.UpdateCity(updateDto)
            );

            Assert.AreEqual("City not found.", ex.Message);
        }
    }
}
