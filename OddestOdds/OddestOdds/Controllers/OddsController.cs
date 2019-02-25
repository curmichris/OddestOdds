using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets.Internal;
using OddestOdds.Core.Entities;
using OddestOdds.Core.Interfaces;
using OddestOdds.Core.Shared;
using OddestOdds.Service.Interfaces;
using OddestOdds.Web.ViewModels;
using OddestOdds.Web.ViewModels.Punter;

namespace OddestOdds.Web.Controllers
{
    [Route("[controller]")]
    public class OddsController : Controller
    {
        private readonly IOddsService _oddsService;

        public OddsController(IOddsService oddsService)
        {
            _oddsService = oddsService;
        }

        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var odds = await this._oddsService.ListAsync(new [] { ProjectConstants.OddValuesEntityName });
            if (odds != null)
            {
                var result = odds.Select(x => new OddsViewModel
                {
                    OddsName = $"{x.HomeTeamName} vs {x.AwayTeamName}",
                    HomeTeamName = x.HomeTeamName,
                    AwayTeamName = x.AwayTeamName,
                    OddValues = new OddValuesViewModel()
                    {
                        HomeOddValue = x.OddValues.HomeOddValue,
                        DrawOddValue = x.OddValues.DrawOddValue,
                        AwayOddValue = x.OddValues.AwayOddValue,
                    }
                });

                return View(result.ToList());
            }

            return RedirectToAction(ProjectConstants.ErrorPage, ProjectConstants.HomeController);
        }
    }
}