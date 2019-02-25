using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OddestOdds.Core.Entities;
using OddestOdds.Service.DTOs;
using OddestOdds.Web.ViewModels.Punter;

namespace OddestOdds.Web.Hubs
{
    public class MessageHub : Hub, IMessageHub
    {

        protected IHubContext<MessageHub> _context;

        public MessageHub(IHubContext<MessageHub> context)
        {
            _context = context;
        }

        public Task UpdateOdds(List<OddsViewModel> viewModel)
        {
            return _context.Clients.All.SendAsync("UpdatedOdds", viewModel);
        }
    }
}
