using Microsoft.AspNetCore.Mvc;
using MSIT141_Ajax_site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141_Ajax_site.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;

        public ApiController(DemoContext context)
        {
            _context = context;
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
                if(datas.Count()==0)
                {
                    return Content("此姓名可以使用");
                }
                else
                {
                    return Content("此姓名已經有人使用");
                }
            }
        }
    }
}
