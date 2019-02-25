using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OddestOdds.Core.Entities;
using OddestOdds.Infrastructure.Data;
using Xunit;

namespace Tests.Integration
{
    public class RepositoryTests
    {
        private OddestOddsContext _dbContext;

        [Fact]
        public async Task Test_Add_Item_And_Set_Id()
        {
            var repository = GetRepository();
            var odd = GenerateOddMock();

            await repository.AddAsync(odd);

            var newItem = await repository.ListAsync<Odd>();

            Assert.Equal(odd, newItem.FirstOrDefault());
            Assert.True(newItem?.FirstOrDefault()?.Id != Guid.Empty);
        }

        [Fact]
        public async Task Test_Add_Item_And_Set_Id_Fetch_ById()
        {
            var repository = GetRepository();
            var odd = GenerateOddMock();
            odd.Id = Guid.NewGuid();
            await repository.AddAsync(odd);

            var newItem = await repository.GetByIdAsync<Odd>(odd.Id);

            Assert.Equal(odd, newItem);
            Assert.True(newItem?.Id != Guid.Empty);
        }

        [Fact]
        public async Task Test_Update_Item_After_Adding()
        {
            // add an item
            var repository = GetRepository();
            var oddName = Guid.NewGuid().ToString();
            var item = GenerateOddMock();
            item.OddName = oddName;

            await repository.AddAsync(item);

            // detach the item so we get a different instance
            _dbContext.Entry(item).State = EntityState.Detached;

            // fetch the item and update its title
            var newItem = await repository.ListAsync<Odd>();

            newItem.FirstOrDefault(i => i.OddName == oddName);
            Assert.NotNull(newItem);
            Assert.NotSame(item, newItem);

            var newOddName = Guid.NewGuid().ToString();
            newItem.FirstOrDefault().OddName = newOddName;

            // Update the item
            await repository.UpdateAsync<Odd>(newItem.FirstOrDefault());
            var updatedItem = await repository.ListAsync<Odd>();

            updatedItem.FirstOrDefault(i => i.OddName == newOddName);

            Assert.NotNull(updatedItem);
            Assert.NotEqual(item.OddName, updatedItem.FirstOrDefault().OddName);
            Assert.Equal(newItem.FirstOrDefault().Id, updatedItem.FirstOrDefault().Id);
        }

        [Fact]
        public async Task Test_Delete_Item_After_Adding()
        {
            // add an item
            var repository = GetRepository();
            var oddName = Guid.NewGuid().ToString();
            var item = GenerateOddMock();
            item.OddName = oddName;

            await repository.AddAsync(item);

            // delete the item
            await repository.DeleteAsync(item);

            // verify it's no longer there
            Assert.DoesNotContain(await repository.ListAsync<Odd>(),
                i => i.OddName == oddName);
        }

        private OddsRepository GetRepository()
        {
            var options = CreateNewContextOptions();

            _dbContext = new OddestOddsContext(options);
            return new OddsRepository(_dbContext);
        }

        private static DbContextOptions<OddestOddsContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<OddestOddsContext>();
            builder.UseInMemoryDatabase("oddestOdds")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private Odd GenerateOddMock()
        {
            var oddMock = new Mock<Odd>();
            oddMock.Object.Id = Guid.Empty;
            return oddMock.Object;
        }
    }
}
