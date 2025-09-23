using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Infrastructure.Contexts;
using AP.Demo_Project.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AP.Demo_Project.Unittest
{
    [TestClass]
    public sealed class CityRepositoryTests
    {
        private static DemoContext CreateContext(out string dbName)
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

        private static void DropDatabase(string dbName)
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
            }
        }

        [TestMethod]
        public async Task AddCity_Succeeds_WithValidData_AndLinksToCountry()
        {
            var context = CreateContext(out var dbName);
            try
            {
                var repo = new CityRepository(context);
                var dto = new CityDetailDTO { Name = "Ghent", Population = 260_000, CountryId = 3 };

                var result = await repo.AddCity(dto);

                Assert.AreEqual("Ghent", result.Name);

                var saved = context.Cities.Include(c => c.Country).Single(c => c.Name == "Ghent");
                Assert.AreEqual(3, saved.CountryId);
                Assert.AreEqual("Belgium", saved.Country.Name);
            }
            finally
            {
                context.Dispose();
                DropDatabase(dbName);
            }
        }

        [TestMethod]
        public async Task AddCity_Throws_OnDuplicateName()
        {
            var context = CreateContext(out var dbName);
            try
            {
                var repo = new CityRepository(context);

                await repo.AddCity(new CityDetailDTO { Name = "Bruges", Population = 118_000, CountryId = 3 });

                await Assert.ThrowsExceptionAsync<System.InvalidOperationException>(async () =>
                    await repo.AddCity(new CityDetailDTO { Name = "Bruges", Population = 119_000, CountryId = 3 }));
            }
            finally
            {
                context.Dispose();
                DropDatabase(dbName);
            }
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-5)]
        public async Task AddCity_Throws_OnInvalidCountryId(int countryId)
        {
            var context = CreateContext(out var dbName);
            try
            {
                var repo = new CityRepository(context);

                await Assert.ThrowsExceptionAsync<System.ArgumentOutOfRangeException>(async () =>
                    await repo.AddCity(new CityDetailDTO { Name = "TestCity", Population = 1, CountryId = countryId }));
            }
            finally
            {
                context.Dispose();
                DropDatabase(dbName);
            }
        }

        [DataTestMethod]
        [DataRow(-1L)]
        [DataRow(10_000_000_001L)]
        public async Task AddCity_Throws_OnInvalidPopulation(long population)
        {
            var context = CreateContext(out var dbName);
            try
            {
                var repo = new CityRepository(context);

                await Assert.ThrowsExceptionAsync<System.ArgumentOutOfRangeException>(async () =>
                    await repo.AddCity(new CityDetailDTO { Name = "PopTest", Population = population, CountryId = 1 }));
            }
            finally
            {
                context.Dispose();
                DropDatabase(dbName);
            }
        }

        [TestMethod]
        public async Task AddCity_TrimsName_AndEnforcesUniqueness()
        {
            var context = CreateContext(out var dbName);
            try
            {
                var repo = new CityRepository(context);

                await repo.AddCity(new CityDetailDTO { Name = "Paris", Population = 2_200_000, CountryId = 4 }); // France = 4

                await Assert.ThrowsExceptionAsync<System.InvalidOperationException>(async () =>
                    await repo.AddCity(new CityDetailDTO { Name = "  Paris  ", Population = 2_300_000, CountryId = 4 }));
            }
            finally
            {
                context.Dispose();
                DropDatabase(dbName);
            }
        }
    }
}