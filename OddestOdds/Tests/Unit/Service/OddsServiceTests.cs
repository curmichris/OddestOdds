using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NSubstitute;
using NUnit.Framework;
using OddestOdds.Core.Entities;
using OddestOdds.Core.Interfaces;
using OddestOdds.Infrastructure.Entities;
using OddestOdds.Service.DTOs;
using OddestOdds.Service.Implementation;
using OddestOdds.Web.Controllers;
using OddestOdds.Web.Hubs;

namespace Tests.Unit.Service
{
    public class OddsServiceTests
    {
        private IOddsRepository _oddsRepository;
        private OddsService sut;

        [SetUp]
        public void Setup()
        {
            _oddsRepository = Substitute.For<IOddsRepository>();
            sut = new OddsService(_oddsRepository);
        }

        [Test]
        public async Task Test_GetById_ReturnsValidObject()
        {
            var mockOdd = GenerateOddMock();
            var id = Guid.NewGuid();
            mockOdd.Id = id;

            _oddsRepository.GetByIdAsync<Odd>(id).Returns(Task.FromResult(mockOdd));

            var result = await sut.GetByIdAsync(id);

            Assert.IsInstanceOf<OddDto>(result);
            Assert.AreEqual(result.Id, id);
        }

        [Test]
        public async Task Test_List_ReturnsValidObject()
        {
            var mockOdd = new List<Odd>()
            {
                GenerateOddMock(),
                GenerateOddMock()
            };

            var id = Guid.NewGuid();
            mockOdd[1].Id = id;

            _oddsRepository.ListAsync<Odd>().Returns(mockOdd);

            var result = await sut.ListAsync();

            Assert.IsInstanceOf<List<OddDto>>(result);
            Assert.AreEqual(result[1].Id, id);
        }

        [Test]
        public async Task Test_List_Includes_ReturnsValidObject()
        {
            var mockOdd = new List<Odd>()
            {
                GenerateOddMock(),
                GenerateOddMock()
            };

            var id = Guid.NewGuid();
            var valuesId = Guid.NewGuid();
            mockOdd[1].Id = id;
            mockOdd[1].OddValues.Id = valuesId;
            mockOdd[0].OddValues.Id = Guid.NewGuid();
            _oddsRepository.ListAsync<Odd>(Arg.Any<string[]>()).Returns(Task.FromResult(mockOdd));

            var result = await sut.ListAsync(new[] { "OddValues" });

            Assert.IsInstanceOf<List<OddDto>>(result);
            Assert.AreEqual(result[1].Id, id);
            Assert.AreEqual(result[1].OddValues.Id, valuesId);
        }

        [Test]
        public async Task Test_Add_ReturnsValidObject()
        {
            var mockOdd = GenerateOddMock();
            var mockOddDto = GenerateOddDtoMock();
            var id = Guid.NewGuid();
            var valuesId = Guid.NewGuid();
            mockOdd.Id = id;
            mockOdd.OddValues.Id = valuesId;

            _oddsRepository.AddAsync<Odd>(Arg.Any<Odd>()).Returns(Task.FromResult(mockOdd));

            var result = await sut.AddAsync(mockOddDto);

            Assert.IsInstanceOf<OddDto>(result);
            Assert.AreEqual(result.Id, id);
            Assert.AreEqual(result.OddValues.Id, valuesId);
        }

        [Test]
        public async Task Test_Update_Is_Completed_Successfully()
        {
            var mockOdd = GenerateOddMock();
            mockOdd.HomeTeamName = "Expected Home";
            mockOdd.AwayTeamName = "Expected Away";

            var mockOddExpectedDto = GenerateOddDtoMock();
            mockOddExpectedDto.HomeTeamName = "Expected Home";
            mockOddExpectedDto.AwayTeamName = "Expected Away";

            _oddsRepository.UpdateAsync(Arg.Any<Odd>()).Returns(Task.FromResult(mockOdd));
            _oddsRepository.UpdateAsync(Arg.Any<OddValue>()).Returns(Task.FromResult(mockOdd));

            var result = await sut.UpdateAsync(mockOddExpectedDto);

            Assert.AreEqual(result.HomeTeamName, mockOddExpectedDto.HomeTeamName);
            Assert.AreEqual(result.AwayTeamName, mockOddExpectedDto.AwayTeamName);
        }

        [Test]
        public async Task Test_Delete_Object_Returns_Deleted_Id()
        {
            var id = Guid.NewGuid();
            var mockOdd = GenerateOddMock();
            mockOdd.Id = id;
            var mockOddDtoToDelete = GenerateOddDtoMock();
            mockOddDtoToDelete.Id = id;
            _oddsRepository.DeleteAsync(Arg.Any<Odd>()).Returns(Task.FromResult(mockOdd));

            var result = await sut.DeleteAsync(mockOddDtoToDelete);

            Assert.AreEqual(result.Id, id);
        }

        [Test]
        public async Task Test_Delete_Guid_Returns_Deleted_Id()
        {
            var id = Guid.NewGuid();
            var mockOdd = GenerateOddMock();
            mockOdd.Id = id;
            var mockOddDtoToDelete = GenerateOddDtoMock();
            mockOddDtoToDelete.Id = id;
            _oddsRepository.DeleteAsync(Arg.Any<Odd>()).Returns(Task.FromResult(mockOdd));

            var result = await sut.DeleteAsync(mockOddDtoToDelete.Id);

            Assert.AreEqual(result, id);
        }


        private Odd GenerateOddMock()
        {
            var oddMock = new Mock<Odd>();
            oddMock.Object.Id = Guid.Empty;
            oddMock.Object.OddValues = new Mock<OddValue>().Object;

            return oddMock.Object;
        }

        private OddDto GenerateOddDtoMock()
        {
            var oddMock = new Mock<OddDto>();
            oddMock.Object.Id = Guid.Empty;
            oddMock.Object.OddValues = new Mock<OddValueDto>().Object;

            return oddMock.Object;
        }
    }
}

