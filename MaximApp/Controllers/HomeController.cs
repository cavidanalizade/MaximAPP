using MaximApp.DAL;
using MaximApp.Models;
using MaximApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MaximApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ReadServicesVM readServicesVM = new ReadServicesVM()
            {
                services =await _context.services.ToListAsync()
            };
            return View(readServicesVM);
        }


    }
    
}
