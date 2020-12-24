using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XinerjiSocketApp.Model;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository
{
    public interface ICovidRepository : IRepository<Covid>
    {
        Task CreateCovid(Covid covid);
        Task<IEnumerable<CovidChart>> GetCovidChart();
        Task DeleteAllDatas();

    }
}
