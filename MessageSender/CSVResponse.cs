using System;
using System.Collections.Generic;
using System.Text;

namespace MessageSender
{
    public class CSVResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public List<string> Lines{ get; set; }

    }
}
