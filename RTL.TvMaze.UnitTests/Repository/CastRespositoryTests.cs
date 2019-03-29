using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RTL.TvMaze.DbContexts;
using RTL.TvMaze.Models;
using RTL.TvMaze.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class CastRepositoryTests
    {
        DbContextOptions<DatabaseContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }

        [Test]
        public void CreateAsync_ShowIsNull()
        {
            // Given
            Cast cast = null;

            using (var context = new DatabaseContext(options))
            {
                var castRepository = new CastRepository(context);
                
                Assert.ThrowsAsync<ArgumentNullException>(async () => await castRepository.CreateCastAsync(cast));
            }
        }
    }
}