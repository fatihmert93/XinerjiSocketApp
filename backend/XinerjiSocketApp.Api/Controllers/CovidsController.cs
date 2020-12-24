using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XinerjiSocketApp.Infrastructure.Abstract;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidsController : ControllerBase
    {

        private readonly ICovidService _covidService;

        public CovidsController(ICovidService covidService)
        {
            _covidService = covidService;
        }


        [HttpPost("SaveCovid")]
        public async Task<IActionResult> SaveCovid(Covid covid)
        {
            await _covidService.SaveCovid(covid);

            return Ok(_covidService.GetCovidChart());
        }

        [HttpGet("GetCovidList")]
        public async Task<IActionResult> GetCovidList()
        {
            var covidList = await _covidService.GetList();

            return Ok(covidList);
        }

        [HttpGet("GetCovidChart")]
        public async Task<IActionResult> GetCovidChart()
        {
            var covidCharts = await _covidService.GetCovidChart();

            return Ok(covidCharts);
        }

        [HttpGet("InitializeCovid")]
        public async Task<IActionResult> InitializeCovid()
        {
            Random rnd = new Random();
            
            Enumerable.Range(1, 10).ToList().ForEach(async v =>
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newCovid = new Covid()
                        {City = item, CovidCount = rnd.Next(100, 1000), CovidDate = DateTime.Now.AddDays(v)};
                    await _covidService.SaveCovid(newCovid);
                    System.Threading.Thread.Sleep(1000);
                }
            });

            return Ok("Covid 19 datas saved");
        }

    }
}
