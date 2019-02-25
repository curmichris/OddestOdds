using System.Collections.Generic;
using System.Threading.Tasks;
using OddestOdds.Web.ViewModels.Punter;

namespace OddestOdds.Web.Hubs
{
    public interface IMessageHub
    {
        Task UpdateOdds(List<OddsViewModel> viewModel);
    }
}