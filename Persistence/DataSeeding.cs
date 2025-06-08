using Domain.Contracts;
using Domain.Models;
using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
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

                _dbContext.SaveChanges();


            }
            catch(Exception ex)
            {
                //TODO
            }
        }
    }
}
