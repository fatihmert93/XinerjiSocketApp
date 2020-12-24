using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XinerjiSocketApp.Model.Entities;

namespace XinerjiSocketApp.Infrastructure.Hubs
{
    public interface ICovidHub
    {
        Task ReceiveCovid(Covid covid);
    }
}
