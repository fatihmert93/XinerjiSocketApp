using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.Hubs
{
    public class CovidHub : Hub<ICovidHub>
    {

        public static List<string> Names { get; set; } = new List<string>();
        
        public async Task SendCovid(Covid covid)
        {
            await Clients.All.ReceiveCovid(covid);
        }

        
        
    }
}
