using System;
using System.Collections.Generic;
using System.Text;

namespace XinerjiSocketApp.Model.Entities
{
    public class Message : EntityBase
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
