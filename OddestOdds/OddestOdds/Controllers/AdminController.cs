using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using OddestOdds.Core.Shared;
using OddestOdds.Service.Interfaces;
using OddestOdds.Web.Helpers;
using OddestOdds.Web.Hubs;
using OddestOdds.Web.ViewModels.Admin;

namespace OddestOdds.Web.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IOddsService _oddsService;
        private readonly IMessageHub _messageHub;

        public AdminController(IOddsService oddsService, IMessageHub messageHub)
        {
            _oddsService = oddsService;
            _messageHub = messageHub;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var odds = await this._oddsService.ListAsync(new[] { ProjectConstants.OddValuesEntityName });
            if (odds != null)
            {
                var result = odds.Select(OddsMapper.InverseMap).ToList();
                return View(result);
            }

            return RedirectToAction(ProjectConstants.ErrorPage, ProjectConstants.HomeController);
        }

        [Route("create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(AdminOddsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _oddsService.AddAsync(OddsMapper.Map(viewModel));
                return RedirectToAction(ProjectConstants.AdminIndexView, ProjectConstants.AdminController);
            }

            return View();
        }

        [Route("update")]
        public async Task<IActionResult> Update(Guid id)
        {
            var odd = await this._oddsService.GetByIdAsync(id, new[] { ProjectConstants.OddValuesEntityName });
            if (odd != null)
            {
                return View(OddsMapper.InverseMap(odd));
            }

            return RedirectToAction(ProjectConstants.ErrorPage, ProjectConstants.HomeController);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(AdminOddsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _oddsService.UpdateAsync(OddsMapper.Map(viewModel));
                return RedirectToAction(ProjectConstants.AdminIndexView, ProjectConstants.AdminController);
            }

            return View();
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != null)
            {
                await this._oddsService.DeleteAsync(id);
                return RedirectToAction(ProjectConstants.AdminIndexView);
            }

            return RedirectToAction(ProjectConstants.ErrorPage, ProjectConstants.HomeController);
        }

        [Route("populate")]
        public async Task<IActionResult> Populate()
        {
            var viewModel = (await _oddsService.ListAsync(new[] { ProjectConstants.OddValuesEntityName })).Select(OddsMapper.InverseOddsViewModelMap).ToList();
            await _messageHub.UpdateOdds(viewModel);

            return RedirectToAction(ProjectConstants.AdminIndexView);
        }
    }
}