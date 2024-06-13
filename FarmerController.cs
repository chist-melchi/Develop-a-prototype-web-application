using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgricultureEnergyConnect.Data;
using AgricultureEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

namespace AgricultureEnergyConnect.Controllers
{
    [Authorize(Roles = "Employee")]
    public class FarmerController : Controller
    {
        private readonly DataContext _context;

        public FarmerController(DataContext context)
        {
            _context = context;
        }

        // GET: Farmer
        public async Task<IActionResult> Index()
        {
            return View(await _context.FarmerProducts.ToListAsync());
        }

        // GET: Farmer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farmer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmerProductId,FarmerName,Location")] FarmerProduct farmerProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmerProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farmerProduct);
        }

        // GET: Farmer/Products
        public async Task<IActionResult> Products(int? id, string category, DateTime? startDate, DateTime? endDate)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsQuery = _context.Product.Where(p => p.FarmerProductId == id);

            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Category == category);
            }

            if (startDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate <= endDate.Value);
            }

            var product = await productsQuery.Include(p => p.FarmerProduct).ToListAsync();

            return View(product);
        }
    }
}
