using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;
using XinerjiSocketApp.Infrastructure.Utilities;
using XinerjiSocketApp.Model;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.DataAccess.Dapper.Repositories
{
    public class CovidRepository : DapperGenericRepository<Covid>, ICovidRepository
    {
        public CovidRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<CovidChart>> GetCovidChart()
        {
            List<CovidChart> covidCharts = new List<CovidChart>();

            string query = @$"select tarih,[1],[2],[3],[4],[5] FROM
                            (select City, CovidCount, Cast(CovidDate as date) as tarih FROM Covid) as covidT
                            PIVOT
                            (SUM(CovidCount) For City IN([1],[2],[3],[4],[5]) ) AS ptable
                            order by tarih asc";

            var result = (await ConnectionFactory.Connection.QueryImplAsync<dynamic>(query)).ToList();

            foreach (dynamic res in result)
            {
                CovidChart covidChart = new CovidChart();

                covidChart.CovidDate = ((DateTime)res.tarih).ToShortDateString();

                Dictionary<string, object> dictionary = new Dictionary<string, object>();

                CommonUtility.AddDictionaryToDynamicType(dictionary, res);

                Enumerable.Range(1,5).ToList().ForEach(x =>
                {

                    int value = 0;

                    bool exists = dictionary.TryGetValue(x.ToString(),out object val);

                    if (DBNull.Value.Equals(val))
                    {
                        value = 0;
                    }
                    else
                    {
                        if (exists)
                            value = (int)val;
                    }

                    covidChart.Counts.Add(value);
                });
                
                covidCharts.Add(covidChart);
            }


            return covidCharts;
        }

        public async Task CreateCovid(Covid covid)
        {

            DynamicParameters paramDistribution = new DynamicParameters();
            paramDistribution.Add("@CovidCount", covid.CovidCount, DbType.Int32);
            paramDistribution.Add("@City", (int)covid.City, DbType.Int32);
            paramDistribution.Add("@CovidDate", covid.CovidDate, DbType.DateTime);

            await ConnectionFactory.Connection.QueryImplAsync<int>(
                "INSERT Covid (CovidCount, CovidDate,City) VALUES(@CovidCount,@CovidDate,@City)", paramDistribution);
        }



        public dynamic GetTest()
        {
            return ConnectionFactory.Connection.QueryAsync("select * from test");
        }


        public async Task DeleteAllDatas()
        {
            await ConnectionFactory.Connection.QueryImplAsync<int>("DELETE FROM Covid");
        }
    }
}
