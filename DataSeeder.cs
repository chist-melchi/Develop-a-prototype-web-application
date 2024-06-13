using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using AgricultureEnergyConnect.Models;

namespace AgricultureEnergyConnect.Data
{
    public static class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                // Look for any data already in the database
                if (context.Farmers.Any() || context.Product.Any())
                {
                    return; // Data was already seeded
                }

                // Seed Farmers
                var farmers = new Farmer[]
                {
                    new Farmer { FarmerName = "John Doe", Email = "john@example.com", PhoneNumber = "1234567890", Address = "123 Farm Lane" },
                    new Farmer { FarmerName = "Jane Smith", Email = "jane@example.com", PhoneNumber = "0987654321", Address = "456 Agriculture Ave" }
                };

                context.Farmers.AddRange(farmers);
                context.SaveChanges();

                // Seed Products
                var products = new Product[]
                {
                    new Product { FarmerName = "Solar-Powered Irrigation System", Description = "Efficient solar irrigation", Price = 1500.00M },
                    new Product { FarmerName = "Wind Turbine", Description = "High-efficiency wind turbine", Price = 3000.00M },
                    new Product { FarmerName = "Biogas Plant", Description = "Eco-friendly biogas solution", Price = 2500.00M }
                };

                context.Product.AddRange(products);
                context.SaveChanges();

                // Seed FarmerProducts (associating farmers with products)
                var farmerProducts = new FarmerProduct[]
                {
                    new FarmerProduct { FarmerProductId = farmers[0].FarmerProductId, ProductId = products[0].ProductId },
                    new FarmerProduct { FarmerProductId = farmers[0].FarmerProductId, ProductId = products[1].ProductId },
                    new FarmerProduct { FarmerProductId = farmers[1].FarmerProductId, ProductId = products[2].ProductId }
                };

                context.FarmerProducts.AddRange(farmerProducts);
                context.SaveChanges();
            }
        }
    }
}
