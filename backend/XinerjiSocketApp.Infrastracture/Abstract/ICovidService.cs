using System.Collections.Generic;
using System.Threading.Tasks;
using XinerjiSocketApp.Model;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.Abstract
{
    public interface ICovidService
    {
        Task<IEnumerable<Covid>> GetList();
        Task<IEnumerable<CovidChart>> GetCovidChart();
        Task SaveCovid(Covid covid);
        Task DeleteAllData();
    }
}
