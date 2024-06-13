using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgricultureEnergyConnect.Data;
using AgricultureEnergyConnect.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AgricultureEnergyConnect.Controllers
{
    [Authorize(Roles = "Farmer, Employee")]
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductsController(DataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //var products = _context.Products.ToList();
            var product = await _context.Product.Include(p => p.FarmerProduct).ToListAsync();
            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["FarmerProductId"] = new SelectList(_context.FarmerProducts, "FarmerProductId", "FarmerName");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> Create([Bind("ProductId,FarmerName,Category,ProductionDate,Price,FarmerPrpductId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FarmerProductId"] = new SelectList(_context.FarmerProducts, "FarmerProductId", "FarmerName", product.FarmerProductId);
            return View(product);
        }


        [Authorize(Roles = "Farmer")]
        public IActionResult MyProducts()
        {
            var userId = User.Identity.Name;
            var product = _context.Product.Where(p => p.FarmerProducts.Any(fp => fp.Farmer.Email == userId));
            return View(product);
        }

        [Authorize(Roles = "Farmer")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Farmer")]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.Name;
                var farmer = _context.Farmers.FirstOrDefault(f => f.Email == userId);
                if (farmer != null)
                {
                    var farmerProduct = new FarmerProduct
                    {
                        Farmer = farmer,
                        //Product = product
                    };
                    _context.Product.Add(product);
                    _context.FarmerProducts.Add(farmerProduct);
                    _context.SaveChanges();
                    return RedirectToAction("MyProducts");
                }
            }
            return View(product);
        }

        

        [Authorize(Roles = "Employee")]
        public IActionResult AddFarmer()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Employee")]
        public IActionResult AddFarmer(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _context.Farmers.Add(farmer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(farmer);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
