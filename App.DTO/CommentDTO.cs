using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class CommentDTO
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public int Score { get; set; }

        public string ClientName { get; set; }

        public string ClientPhoto { get; set; }
    }
}
