using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSIT141_Ajax_site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141_Ajax_site.Controllers
{
    public class HomeController : Controller
    {
        private readonly DemoContext _context;
        private readonly ILogger<HomeController> _logger;

        public IActionResult Partial()
        {
            ViewBag.data = "Hello Partial!!";
            return PartialView(_context.Members);
        }

        public IActionResult jQuery()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Fetch()
        {
            return View();
        }

        public IActionResult Promise()
        {
            return View();
        }

        public IActionResult FirstAjax()
        {
            return View();
        }

        public IActionResult AjaxPost()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }
        
        public HomeController(ILogger<HomeController> logger, DemoContext conetxt)
        {

            _logger = logger;
            _context = conetxt;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
