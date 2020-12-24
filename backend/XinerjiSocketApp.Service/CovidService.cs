using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using XinerjiSocketApp.Infrastructure.Abstract;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;
using XinerjiSocketApp.Infrastructure.Hubs;
using XinerjiSocketApp.Model;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Service
{
    public class CovidService : ICovidService
    {
        private readonly IHubContext<CovidHub> _covidHubContext;
        private readonly ICovidRepository _covidRepository;

        public CovidService(IHubContext<CovidHub> covidHubContext, ICovidRepository covidRepository)
        {
            _covidHubContext = covidHubContext;
            _covidRepository = covidRepository;
        }


        public async Task<IEnumerable<Covid>> GetList()
        {
            return (await _covidRepository.QueryAsync(v => true)).ToList();
        }


        public async Task<IEnumerable<CovidChart>> GetCovidChart()
        {
            return (await _covidRepository.GetCovidChart()).ToList();
        }

        public async Task SaveCovid(Covid covid)
        {
            await _covidRepository.CreateCovid(covid);

            var covidCharts = await _covidRepository.GetCovidChart();
            
            await _covidHubContext.Clients.All.SendAsync("ReceiveCovidList",covidCharts);
        }

        public async Task DeleteAllData()
        {
            await _covidRepository.DeleteAllDatas();
        }
    }
}
