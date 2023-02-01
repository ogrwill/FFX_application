using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace MonitoringService.Controllers
{
    public class QueueController : Controller
    {
        public IActionResult Index()
        {
           // ViewBag.Status = CheckServices();
            return View();
        }

        public bool CheckService(string serviceName)
        {

            return Process.GetProcessesByName(serviceName).Any();
      
        }
    }
}