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
    public class ShowRepositoryTests
    {   
        // seed list of shows
        private List<Show> showList;

        DbContextOptions<DatabaseContext> options;

        [SetUp]
        public void Setup()
        {

            showList = new List<Show>
            {
                new Show { Id = 1, ShowId = 1, Name = "#1 show", Casts = new List<Cast>
                {
                     new Cast { Id = 1, Name = "#1 Cast", Birthday = DateTime.Parse("03-01-1980") }
                } },
                new Show { Id = 2, ShowId = 100, Name = "#2 show", Casts = new List<Cast>
                {
                     new Cast { Id = 2, Name = "#5 Cast", Birthday = DateTime.Parse("03-01-1980") },
                     new Cast { Id = 3, Name = "#9 Cast", Birthday = DateTime.Parse("03-05-1986") }
                } },
                new Show { Id = 3, ShowId = 13457, Name = "#3 show", Casts = new List<Cast>
                {
                     new Cast { Id = 4, Name = "#10 Cast", Birthday = DateTime.Parse("10-01-1950") }
                } }
            };

            options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (var context = new DatabaseContext(options))
            {
                context.AddRange(showList);
                context.SaveChanges();
            }
        }

        [Test]
        public void CreateAsync_ShowIsNull()
        {
            // Given
            Show show = null;

            using (var context = new DatabaseContext(options))
            {
                var showRepository = new ShowRepository(context);
                
                Assert.ThrowsAsync<ArgumentNullException>(async () => await showRepository.CreateAsync(show));
            }
        }

        [Test]
        public async Task CreateAsync_ItemIsAdded()
        {
            // Arrange
            var show = new Show()
            {
                Id = 101,
                Name = "abc"
            };

            // Act
            using (var context = new DatabaseContext(options))
            {
                var showRepository = new ShowRepository(context);
                await showRepository.CreateAsync(show);
                await showRepository.SaveAsync();
            }

            // Assert
            using (var context = new DatabaseContext(options))
            {
                Assert.That(await context.Shows.CountAsync(), Is.EqualTo(4));
                Assert.That((await context.Shows.SingleAsync(q => q.Id == 101)).Name, Is.EqualTo("abc"));
            }
        }
    }
}