using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSIT141_Ajax_site.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141_Ajax_site.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;

        public ApiController(DemoContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _host = hostEnvironment;
        }

        //public IActionResult Index(string name ,int age =0)
        public IActionResult Index(User user)
        {
            //return Content("Ajax,你不好?", "text/plain", System.Text.Encoding.UTF8);
            //System.Threading.Thread.Sleep(5000);

            if (String.IsNullOrEmpty(user.name))
            {
                user.name = "Ajax";
            }
            return Content($"{user.name}你好，你的年紀是{user.age},信箱是{user.email}!!", "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult VerifyName(User user)
        {
            IEnumerable<Member> datas = null;
            if (user.name == null)
            {
                return Content("請輸入姓名");
            }
            else
            {
                datas = _context.Members.Where(u => u.Name.Equals(user.name)).ToList();
                if (datas.Count() == 0)
                {
                    return Content("此姓名可以使用");
                }
                else
                {
                    return Content("此姓名已經有人使用");
                }
            }
        }

        public IActionResult CheckName(string name)
        {
            var exist = _context.Members.Any(m => m.Name == name);
            return Content(exist.ToString(), "text/plan");
        }

        public IActionResult Register(Member member, IFormFile file)
        {
            //string info = $"{file.FileName}-{file.ContentType}-{file.Length}";
            //return Content(info, "text/plain", System.Text.Encoding.UTF8);

            string path = Path.Combine(_host.WebRootPath, "uploads", file.FileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            byte[] imgByte = null;
            using(var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileName = file.FileName;
            member.FileData = imgByte;

            _context.Members.Add(member);
            _context.SaveChanges();

            string info = $"{file.FileName}-{file.ContentType}-{file.Length}";
            return Content(info, "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult City()
        {
            var cites = _context.Addresses.Select(a => a.City).Distinct();
            return Json(cites);
        }

        public IActionResult Districts(string city)
        {
            var districts = _context.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        public IActionResult Road(string district)
        {
            var roads = _context.Addresses.Where(a => a.SiteId == district).Select(a => a.Road);
            return Json(roads);
        }

        public IActionResult Memebers()
        {
            return Json(_context.Members);
        }

        public IActionResult GetImageByBytes(int id = 1)
        {
            Member member = _context.Members.Find(id);
            byte[] img = member.FileData;
            return File(img, "image/jpeg");
        }
    }
}
