using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MessageSender
{
    public class CSVHelper
    {
        public static CSVResponse UploadCSV(string filePath)
        {
            CSVResponse response = new CSVResponse();
            try
            {
                //skip the first line of headings              
                response.Lines = File.ReadAllLines(filePath).Skip(1).ToList(); ;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                throw;
            }
          
            return response;         
        }
     
    }
}
