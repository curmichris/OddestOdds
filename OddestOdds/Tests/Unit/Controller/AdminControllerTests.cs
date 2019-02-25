using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using OddestOdds.Core.Entities;
using OddestOdds.Infrastructure.Entities;
using OddestOdds.Service.DTOs;
using OddestOdds.Service.Interfaces;
using OddestOdds.Web.Controllers;
using OddestOdds.Web.Helpers;
using OddestOdds.Web.Hubs;
using OddestOdds.Web.ViewModels.Admin;

namespace Tests.Unit.Controller
{
    class AdminControllerTests
    {
        private IOddsService _oddsService;
        private IMessageHub _messageHub;
        private AdminController sut;

        [SetUp]
        public void Setup()
        {
            _oddsService = Substitute.For<IOddsService>();
            _messageHub = Substitute.For<IMessageHub>();
            sut = new AdminController(_oddsService, _messageHub);
        }

        [Test]
        public async Task IndexViewReturnsValidList()
        {
            var odds = new List<OddDto>()
            {
                new OddDto()
                {
                    HomeTeamName = "Test1",
                    AwayTeamName = "Test2",
                    OddName = "TestName",
                    OddValues = new OddValueDto()
                    {
                        HomeOddValue = new decimal(1.35),
                        AwayOddValue = new decimal(3.10),
                        DrawOddValue = new decimal(1.01)
                    },
                }
            };

            this._oddsService.ListAsync(Arg.Any<string[]>()).Returns(odds);
            
            var result = await sut.Index();
            
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public async Task Delete_RedirectsToIndex()
        {
            var id = Guid.NewGuid();
            this._oddsService.DeleteAsync(id).Returns(id);
            
            var result = await sut.Delete(id);
            var routeResult = result as RedirectToActionResult;
            
            Assert.NotNull(result, "Not a redirect result");
            Assert.AreEqual(routeResult.ActionName, "Index");
        }

        [Test]
        public async Task Create_Post_RedirectsToIndex()
        {
            var odds = new OddDto()
            {
                HomeTeamName = "Test1",
                AwayTeamName = "Test2",
                OddName = "TestName",
                OddValues = new OddValueDto()
                {
                    HomeOddValue = new decimal(1.35),
                    AwayOddValue = new decimal(3.10),
                    DrawOddValue = new decimal(1.01)
                },
            };

            this._oddsService.AddAsync(odds).Returns(odds);
            
            var result = await sut.Create(OddsMapper.InverseMap(odds));
            var routeResult = result as RedirectToActionResult;
            
            Assert.NotNull(result, "Not a redirect result");
            Assert.AreEqual(routeResult.ActionName, "Index");
        }

        [Test]
        public async Task Update_Post_RedirectsToIndex()
        {
            var odds = new OddDto()
            {
                HomeTeamName = "Test1",
                AwayTeamName = "Test2",
                OddName = "TestName",
                OddValues = new OddValueDto()
                {
                    HomeOddValue = new decimal(1.35),
                    AwayOddValue = new decimal(3.10),
                    DrawOddValue = new decimal(1.01)
                },
            };

            this._oddsService.UpdateAsync(odds).Returns(odds);

            var result = await sut.Update(OddsMapper.InverseMap(odds));
            var routeResult = result as RedirectToActionResult;

            Assert.NotNull(result, "Not a redirect result");
            Assert.AreEqual(routeResult.ActionName, "Index");
        }
    }
}
