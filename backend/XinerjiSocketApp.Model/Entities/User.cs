using System;
using System.Collections.Generic;
using System.Text;

namespace XinerjiSocketApp.Model.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionId { get; set; }
        
    }
}
