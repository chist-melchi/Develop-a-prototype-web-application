using Microsoft.AspNetCore.Mvc;
using AgricultureEnergyConnect.Data;
using System.Linq;

namespace AgricultureEnergyConnect.Controllers
{
    public class SustainableFarmingController : Controller
    {
        private readonly DataContext _context;

        public SustainableFarmingController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var articles = _context.Articles.ToList();
            return View(articles);
        }

       
        public IActionResult Details(int id)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }


    }
}
