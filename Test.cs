using Xunit;
using AgricultureEnergyConnect.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;


namespace AgricultureEnergyConnect.Models
{
    public class Test
    {
        public void Product_Name_Is_Required()
        {
            var product = new Product
            {
                Category = "Vegetables",
                ProductionDate = DateTime.Now,
                FarmerProductId = 1

            };

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, ValueTask => ValueTask.MemberNames.Contains("FarmerName") && ValueTask.ErrorMessage.Contains("required"));
        }
    }
}
