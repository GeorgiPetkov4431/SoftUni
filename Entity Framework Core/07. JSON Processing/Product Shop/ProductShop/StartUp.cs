using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            using ProductShopContext dbContext = new ProductShopContext();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            //string jsonFileName = "categories-products.json";
            //string jsonFilePath = GetJsonFilePath(jsonFileName);

            //string inputJson = File.ReadAllText(jsonFilePath, Encoding.UTF8);

            //string importResult = ImportCategoryProducts(dbContext, inputJson);

            //Console.WriteLine(importResult);

            string jsonFileName = "products-in-range.json";
            string jsonFilePath = GetJsonResultFilePath(jsonFileName);

            string jsonResult = GetProductsInRange(dbContext);

            File.WriteAllText(jsonFilePath, jsonResult, Encoding.UTF8);

            Console.WriteLine(jsonResult);

        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            IEnumerable<ImportUserDto>? importUserDtos =
                JsonConvert.DeserializeObject<ImportUserDto[]>(inputJson);

            if (importUserDtos == null)
            {
                importUserDtos = Array.Empty<ImportUserDto>();
            }

            ICollection<User> usersToPersist = new List<User>();

            foreach (ImportUserDto userDto in importUserDtos)
            {
                // Skip invalid DTO records
                if (!IsValid(userDto)) continue;

                // Manual mapping -> Can be omitted by using AutoMapper/Mapperly
                User newUser = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Age = userDto.Age
                };

                usersToPersist.Add(newUser);
            }

            context.Users.AddRange(usersToPersist);
            context.SaveChanges();

            return $"Successfully imported {usersToPersist.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            IEnumerable<ImportProductDto>? importProductDtos =
                JsonConvert.DeserializeObject<ImportProductDto[]>(inputJson);

            if (importProductDtos == null)
            {
                importProductDtos = Array.Empty<ImportProductDto>();
            }

            // One-time fetch of all User Ids
            // Pros: Avoids multiple database queries for each product DTO (Network Congestion)
            // Cons: If the user table is very large, it may consume more memory
            IEnumerable<int> validUserIds = context.Users
                .AsNoTracking()
                .Select(u => u.Id)
                .ToArray();

            ICollection<Product> productsToPersist = new List<Product>();
            foreach (ImportProductDto productDto in importProductDtos)
            {
                if (!IsValid(productDto)) continue;

                // Fetch whether User Id exists for each Product DTO
                // Pros: Avoids loading all User Ids into memory, which is beneficial if the Users table is very large
                // Cons: May result in multiple DB queries, which can lead to network congestion

                //bool isSellerIdValid = validUserIds.Contains(productDto.SellerId);
                //bool isBuyerIdValid = (!productDto.BuyerId.HasValue) || validUserIds.Contains(productDto.BuyerId.Value);

                //// Skip records with relation to non-existing Users
                //if (!isSellerIdValid || !isBuyerIdValid) continue;

                Product product = new Product()
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    SellerId = productDto.SellerId,
                    BuyerId = productDto.BuyerId
                };

                productsToPersist.Add(product);
            }

            context.Products.AddRange(productsToPersist);
            context.SaveChanges();

            return $"Successfully imported {productsToPersist.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            IEnumerable<ImportCategoryDto>? importCategoryDtos =
                JsonConvert.DeserializeObject<ImportCategoryDto[]>(inputJson);

            if (importCategoryDtos == null)
            {
                importCategoryDtos = Array.Empty<ImportCategoryDto>();
            }

            ICollection<Category> categoriesToPersist = new List<Category>();
            foreach (ImportCategoryDto categoryDto in importCategoryDtos)
            {
                if (!IsValid(categoryDto)) continue;

                Category category = new Category()
                {
                    Name = categoryDto.Name
                };

                categoriesToPersist.Add(category);
            }

            context.Categories.AddRange(categoriesToPersist);
            context.SaveChanges();

            return $"Successfully imported {categoriesToPersist.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            IEnumerable<ImportCategoryProductDto>? importCategoryProductDtos =
                JsonConvert.DeserializeObject<ImportCategoryProductDto[]>(inputJson);

            if (importCategoryProductDtos == null)
            {
                importCategoryProductDtos = Array.Empty<ImportCategoryProductDto>();
            }

            ICollection<CategoryProduct> categoryProductsToPersist = new List<CategoryProduct>();
            foreach (ImportCategoryProductDto categoryProductDto in importCategoryProductDtos)
            {
                // No validation attributes are presen -> skip validation check
                // Adding validation check will enable future Validation Attributes use
                if (!IsValid(categoryProductDto)) continue;

                CategoryProduct categoryProduct = new CategoryProduct()
                {
                    CategoryId = categoryProductDto.CategoryId,
                    ProductId = categoryProductDto.ProductId
                };

                categoryProductsToPersist.Add(categoryProduct);
            }

            context.CategoriesProducts.AddRange(categoryProductsToPersist);
            context.SaveChanges();

            return $"Successfully imported {categoryProductsToPersist.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .AsNoTracking()
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .OrderBy(p => p.Price)
                .ToArray();

            DefaultContractResolver camelCaseContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            string jsonResult = JsonConvert
                .SerializeObject(products, Formatting.Indented, new JsonSerializerSettings()
                {
                    ContractResolver = camelCaseContractResolver
                });

            return jsonResult;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var usersProducts = context.Users
            .Where(u => u.ProductsSold.Any(ps => ps.BuyerId != null))
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .AsNoTracking()
            .Select(u => new
            {
                firstName = u.FirstName,
                lastName = u.LastName,
                soldProducts = u.ProductsSold
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName

                    }).ToArray()

            }).ToArray();

            return JsonConvert.SerializeObject(usersProducts, Formatting.Indented);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            ExportCategoryByProductsDto[] categoriesByProducts = context.Categories
                .AsNoTracking()
                .Select(c => new ExportCategoryByProductsDto()
                {
                    Name = c.Name,
                    ProductsCount = c.CategoriesProducts.Count,
                    AveragePrice = c.CategoriesProducts
                        .Average(cp => cp.Product.Price)
                        .ToString("F2"),
                    TotalRevenue = c.CategoriesProducts
                        .Sum(cp => cp.Product.Price)
                        .ToString("F2")
                })
                .OrderByDescending(c => c.ProductsCount)
                .ToArray();

            /* .SerializeObject() is capable of working with anonymous types as well */
            string jsonResult = JsonConvert
                .SerializeObject(categoriesByProducts, Formatting.Indented);

            return jsonResult;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            // When working with anonymous types -> keep in mind that property names should match property names in JSON
            var usersWithProducts = context.Users
                .AsNoTracking()
                .Where(u => u.ProductsSold.Any(ps => ps.BuyerId.HasValue))
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProducts = new
                    {
                        Count = u.ProductsSold.Count(ps => ps.BuyerId.HasValue),
                        Products = u.ProductsSold
                            .Where(ps => ps.BuyerId.HasValue)
                            .Select(ps => new
                            {
                                ps.Name,
                                ps.Price
                            })
                            .ToArray()
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToArray();

            var usersWithSoldProducts = new
            {
                UsersCount = usersWithProducts.Length,
                Users = usersWithProducts
            };

            DefaultContractResolver camelCaseContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            string jsonResult = JsonConvert
                .SerializeObject(usersWithSoldProducts, Formatting.Indented, new JsonSerializerSettings()
                {
                    ContractResolver = camelCaseContractResolver,
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonResult;
        }

        private static string GetJsonFilePath(string jsonFileName)
        {
            string jsonFolderRelPath = "../../../Datasets/";
            string jsonFilePath = Path.Combine(jsonFolderRelPath, jsonFileName);

            return Path.GetFullPath(jsonFilePath);
        }

        private static string GetJsonResultFilePath(string jsonResultFileName)
        {
            string jsonResultFolderRelPath = "../../../Results/";
            string jsonResultFilePath = Path.Combine(jsonResultFolderRelPath, jsonResultFileName);

            return Path.GetFullPath(jsonResultFilePath);
        }

        private static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator
                .TryValidateObject(obj, validationContext, validationResults);

            return isValid;
        }
    }
}