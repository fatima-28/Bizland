using Bizland.DAL;
using Bizland.Models;
using Bizland.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bizland.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get;  }
      
        public HomeController(AppDbContext context)
        {
            _context = context;
          
        }
        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel
            {
                Cards=_context.Cards.Where(c=>!c.IsDeleted).ToList()
            };
            return View(home);
        }
    }
}
