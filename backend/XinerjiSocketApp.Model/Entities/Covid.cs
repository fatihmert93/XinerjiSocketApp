using System;
using System.Collections.Generic;
using System.Text;

namespace XinerjiSocketApp.Model.Entities
{

    public enum ECity
    {
        Istanbul = 1,
        Ankara = 2,
        Izmir = 3,
        Konya = 4,
        Antalya = 5
    }
    
    public class Covid : EntityBase
    {
        public ECity City { get; set; }
        public int CovidCount { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
