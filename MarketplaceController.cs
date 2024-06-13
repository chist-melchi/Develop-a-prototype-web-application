using Microsoft.AspNetCore.Mvc;
using AgricultureEnergyConnect.Data;
using System.Linq;

namespace AgricultureEnergyConnect.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly DataContext _context;

        public MarketplaceController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Product.ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _context.Product.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }

}
