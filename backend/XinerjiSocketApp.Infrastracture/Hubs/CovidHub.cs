using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using XinerjiSocketApp.Infrastructure.Abstract;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.Hubs
{
    public class CovidHub : Hub
    {
        private readonly ICovidService _covidService;
        
        public CovidHub(ICovidService covidService)
        {
            _covidService = covidService;
        }
        
        public static List<string> Names { get; set; } = new List<string>();

        public async Task GetCovidList()
        {
            var covidCharts = await _covidService.GetCovidChart();

            await Clients.All.SendAsync("ReceiveCovidList", covidCharts);
        }

        
        
    }
}
