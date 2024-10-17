using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Repository.Data.DataSeeding
{
    public static class DataSeed
    {
        public static async Task SeedAsync(YumifyDbContext dbContext)
        {
            var DbBrands=dbContext.Brands;
            var DbCategories=dbContext.Categories;
            var DbProducts=dbContext.Products;
            var DbDelivery = dbContext.deliveryMethods;
            //Brands
            if (DbBrands.Count() == 0)
            {
                var BrandData = File.ReadAllText("../Yumify.Repository/Data/DataSeeding/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands?.Count > 0)
                {
                    Brands = Brands.Select(P => new ProductBrand
                    {
                        Name = P.Name,
                    }).ToList();

                    foreach (var brand in Brands)
                    {
                        await DbBrands.AddAsync(brand);
                    }
                    await dbContext.SaveChangesAsync();
                }


            }

            //Categories
            if (DbCategories.Count() == 0)
            {
                var CategoryData = File.ReadAllText("../Yumify.Repository/Data/DataSeeding/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);
                if (Categories?.Count > 0)
                {
                    Categories = Categories.Select(P => new ProductCategory
                    {
                        Name = P.Name,
                    }).ToList();

                    foreach (var category in Categories)
                    {
                        await DbCategories.AddAsync(category);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }


            //Products
            if (DbProducts.Count() == 0)
            {
                var productData = File.ReadAllText("../Yumify.Repository/Data/DataSeeding/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (Products?.Count > 0)
                {
                    Products = Products.Select(P => new Product
                    {
                        Name = P.Name,
                        Brand = P.Brand,
                        BrandId = P.BrandId,
                        Category = P.Category,
                        CategoryId = P.CategoryId,
                        Description = P.Description,
                        PictureUrl = P.PictureUrl,
                        Price = P.Price
                    }).ToList();

                    foreach (var Product in Products)
                    {
                        await DbProducts.AddAsync(Product);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if(!DbDelivery.Any())
            {
                var deliveryData = File.ReadAllText("../Yumify.Repository/Data/DataSeeding/delivery.json");
                var deliverySerialzied= JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (deliverySerialzied?.Count >0)
                {
                    deliverySerialzied= deliverySerialzied.Select(D => new DeliveryMethod
                    {
                        Name= D.Name,
                        DeliveryTime = D.DeliveryTime,
                        Description= D.Description,
                        Cost = D.Cost,
                    }).ToList();

                    foreach (var deliverymethod in deliverySerialzied)
                    {
                        await DbDelivery.AddAsync(deliverymethod);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
