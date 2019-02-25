using NUnit.Framework;
using OddestOdds.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using OddestOdds.Core.Entities;
using OddestOdds.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using OddestOdds.Infrastructure.Entities;
using OddestOdds.Service.DTOs;
using OddestOdds.Service.Interfaces;

namespace Tests.Unit.Controller
{
    public class OddsControllerTests
    {
        private IOddsService oddsService;
        private OddsController sut;

        [SetUp]
        public void Setup()
        {
            oddsService = Substitute.For<IOddsService>();
            sut = new OddsController(oddsService);
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

            this.oddsService.ListAsync(Arg.Any<string[]>()).Returns(odds);

            var result = await sut.Index();

            Assert.IsInstanceOf(typeof(ViewResult), result);
        }
    }
}
