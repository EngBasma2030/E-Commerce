using Domain.Contracts;
using Domain.Models;
using Domain.Models.IdentityModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext , 
        UserManager<ApplicationUser> _userManager , 
        RoleManager<IdentityRole> _roleManager , StoreIdentityDbContext _identityDbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Persistence\Data\DataSeed\types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"..\Persistence\Data\DataSeed\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products != null && Products.Any())
                    {
                        _dbContext.Products.AddRange(Products);
                    }
                }

                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodData = File.ReadAllText(@"..\Persistence\Data\DataSeed\delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
                    if (DeliveryMethods != null && DeliveryMethods.Any())
                    {
                        _dbContext.Set<DeliveryMethod>().AddRange(DeliveryMethods);
                    }
                }

                _dbContext.SaveChanges();


            }
            catch(Exception ex)
            {
                //TODO
            }
        }

        public async void IdentityDataSeed()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Ahmed",
                        PhoneNumber = "1234567890",
                        UserName = "MohamedAhmed"
                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Ahmed",
                        PhoneNumber = "1234567890",
                        UserName = "SalmaAhmed"
                    };

                    await _userManager.CreateAsync(user01, "P@$$w0rd");
                    await _userManager.CreateAsync(user02, "P@$$w0rd");

                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");

                    await _identityDbContext.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
