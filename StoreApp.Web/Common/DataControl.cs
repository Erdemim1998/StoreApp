using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace StoreApp.Web.Common
{
    public class DataControl
    {
        public static async Task<bool> IsRecordExists(ProductViewModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var serializedModel = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                using (var responseProducts = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProducts"))
                {
                    string apiResponse = await responseProducts.Content.ReadAsStringAsync();
                    var products = JsonSerializer.Deserialize<List<ProductViewModel>>(apiResponse);
                    var existsProduct = products!.FirstOrDefault(p => p.Name == model.Name && p.Ml1Name == model.Ml1Name && p.Ml2Name == model.Ml2Name && p.Price == model.Price && p.Description == model.Description &&
                                          p.Url == model.Url && p.SubCategoryId == model.SubCategoryId && p.BrandId == model.BrandId);

                    if (existsProduct != null)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public static async Task<bool> IsCategoryRecordExists(Category model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var serializedModel = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                using (var responseCategories = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetCategories"))
                {
                    string apiResponse = await responseCategories.Content.ReadAsStringAsync();
                    var categories = JsonSerializer.Deserialize<List<Category>>(apiResponse);
                    var existsProduct = categories!.FirstOrDefault(c => c.Name == model.Name && c.Ml1Name == model.Ml1Name && c.Ml2Name == model.Ml2Name);

                    if (existsProduct != null)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public static async Task<bool> IsSubCategoryRecordExists(SubCategory model)
        {
            var subCategories = await GetSubCategories();
            var existsSubCategory = subCategories!.FirstOrDefault(sc => sc.Name == model.Name && sc.Ml1Name == model.Ml1Name && sc.Ml2Name == model.Ml2Name && sc.Url == model.Url && sc.CategoryId == model.CategoryId);

            if (existsSubCategory != null)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> IsUserRecordExists(AppUserViewModel model)
        {
            var users = await GetUsers();
            var existsUser = users!.FirstOrDefault(sc => sc.UserName == model.UserName || sc.Email == model.Email);

            if (existsUser != null)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> IsRoleRecordExists(AppRole model)
        {
            var roles = await GetRoles();
            var existsRole = roles!.FirstOrDefault(role => role.Name!.ToLower() == model.Name!.ToLower());

            if (existsRole != null)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> IsBrandRecordExists(Brand model)
        {
            var brands = await GetBrands();
            var existsRole = brands!.FirstOrDefault(brand => brand.Name!.ToLower() == model.Name!.ToLower());

            if (existsRole != null)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> IsCityRecordExists(City model)
        {
            var cities = await GetCities();
            var existsRole = cities!.FirstOrDefault(city => city.Name!.ToLower() == model.Name!.ToLower());

            if (existsRole != null)
            {
                return true;
            }

            return false;
        }

        public static async Task<List<Product>> GetProducts()
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProducts"))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndex(int PageIndex)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndex/" + PageIndex))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexMaxPrice(int PageIndex, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexMaxPrice/" + PageIndex + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexMaxPrice(int PageIndex, float maxPrice, string url)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexMaxPrice/" + PageIndex + "/" + maxPrice + "/" + url))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexMinPrice(int PageIndex, float minPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexMinPrice/" + PageIndex + "/" + minPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexMinPrice(int PageIndex, float minPrice, string url)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexMinPrice/" + PageIndex + "/" + minPrice + "/" + url))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexMinMaxPrice(int PageIndex, float minPrice, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexMinMaxPrice/" + PageIndex + "/" + minPrice + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexMinMaxPrice(int PageIndex, float minPrice, float maxPrice, string url)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexMinMaxPrice/" + PageIndex + "/" + minPrice + "/" + maxPrice + "/" + url))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexBrands(int PageIndex, string Brands)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexBrands/" + PageIndex + "/" + Brands))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexBrands(int PageIndex, string Brands, string url)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexBrands/" + PageIndex + "/" + Brands + "/" + url))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexBrandsMaxPrice(int PageIndex, string Brands, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexBrandsMaxPrice/" + PageIndex + "/" + Brands + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexBrandsMaxPrice(int PageIndex, string Brands, float maxPrice, string url)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexBrandsMaxPrice/" + PageIndex + "/" + Brands + "/" + maxPrice + "/" + url))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexBrandsMinPrice(int PageIndex, string Brands, float minPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexBrandsMinPrice/" + PageIndex + "/" + Brands + "/" + minPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexBrandsMinPrice(int PageIndex, string Brands, float minPrice, string url)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexBrandsMinPrice/" + PageIndex + "/" + Brands + "/" + minPrice + "/" + url))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexBrandsMinMaxPrice(int PageIndex, string Brands, float minPrice, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexBrandsMinMaxPrice/" + PageIndex + "/" + Brands + "/" + minPrice + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByPageIndexBrandsMinMaxPrice(int PageIndex, string Brands, float minPrice, float maxPrice, string url)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByPageIndexBrandsMinMaxPrice/" + PageIndex + "/" + Brands + "/" + minPrice + "/" + maxPrice + "/" + url))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProducts(string? url)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProducts/" + url))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsMinPrice(float minPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsMinPrice/" + minPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsMaxPrice(float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsMaxPrice/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsMinMaxPrice(float minPrice, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsMinMaxPrice/" + minPrice + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsBrands(string brands)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsBrands/" + brands))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsBrandsMinPrice(string brands, float minPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsBrandsMinPrice/" + brands + "/" + minPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsBrandsMaxPrice(string brands, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsBrandsMaxPrice/" + brands + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsBrandsMinMaxPrice(string brands, float minPrice, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsBrandsMinMaxPrice/" + brands + "/" + minPrice + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsUrlMinPrice(string? url, float minPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsUrlMinPrice/" + url + "/" + minPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsUrlMaxPrice(string? url, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsUrlMaxPrice/" + url + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsUrlMinMaxPrice(string? url, float minPrice, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsUrlMinMaxPrice/" + url + "/" + minPrice + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsUrlBrands(string? url, string brands)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsUrlBrands/" + url + "/" + brands))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsUrlBrandsMinPrice(string? url, string brands, float minPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsUrlBrandsMinPrice/" + url + "/" + brands + "/" + minPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsUrlBrandsMaxPrice(string? url, string brands, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsUrlBrandsMaxPrice/" + url + "/" + brands + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsUrlBrandsMinMaxPrice(string? url, string brands, float minPrice, float maxPrice)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsUrlBrandsMinMaxPrice/" + url + "/" + brands + "/" + minPrice + "/" + maxPrice))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProducts(string? url, int PageIndex)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProducts/" + url + "/" + PageIndex))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsBySubCategoryId(int id)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsBySubCategoryId/" + id))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsByBrandId(int id)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsByBrandId/" + id))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<List<Product>> GetProductsBySearchText(string? q)
        {
            List<Product>? products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductsBySearchText/" + q))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(jsonResponse);
                }
            }

            return products!;
        }

        public static async Task<Product?> GetProductByUrl(string url)
        {
            Product? product = new Product();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductByUrl/" + url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        product = JsonSerializer.Deserialize<Product>(jsonData);
                    }
                }
            }

            return product;
        }

        public static async Task<List<ProductImage>> GetProductImages(int id)
        {
            List<ProductImage>? productImages = new List<ProductImage>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductImages/" + id))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    productImages = JsonSerializer.Deserialize<List<ProductImage>>(jsonResponse);
                }
            }

            return productImages!;
        }

        public static async Task<List<ProductImage>> GetCardProductImages(int id)
        {
            List<ProductImage>? productImages = new List<ProductImage>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetCardProductImages/" + id))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    productImages = JsonSerializer.Deserialize<List<ProductImage>>(jsonResponse);
                }
            }

            return productImages!;
        }

        public static async Task<List<Category>> GetCategories()
        {
            List<Category>? categories = new List<Category>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetCategories"))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<List<Category>>(jsonResponse);
                }
            }

            return categories!;
        }

        public static async Task<List<OrderItem>> GetOrders()
        {
            List<OrderItem>? orders = new List<OrderItem>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetOrders"))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    orders = JsonSerializer.Deserialize<List<OrderItem>>(jsonResponse);
                }
            }

            return orders!;
        }

        public static async Task<List<SubCategory>> GetSubCategories()
        {
            List<SubCategory>? subCategories = new List<SubCategory>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetSubCategories"))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    subCategories = JsonSerializer.Deserialize<List<SubCategory>>(jsonResponse);
                }
            }

            return subCategories!;
        }

        public static async Task<List<SubCategory>> GetSubCategories(int id)
        {
            List<SubCategory>? subCategories = new List<SubCategory>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetSubCategories/" + id))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    subCategories = JsonSerializer.Deserialize<List<SubCategory>>(jsonResponse);
                }
            }

            return subCategories!;
        }

        public static async Task<List<SubCategory>> GetSubCategoriesBySearchText(string? q)
        {
            List<SubCategory>? subCategories = new List<SubCategory>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetSubCategoriesBySearchText/" + q))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    subCategories = JsonSerializer.Deserialize<List<SubCategory>>(jsonResponse);
                }
            }

            return subCategories!;
        }

        public static async Task<List<AppUser>> GetUsers()
        {
            List<AppUser>? users = new List<AppUser>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetUsers"))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    users = JsonSerializer.Deserialize<List<AppUser>>(jsonResponse);
                }
            }

            return users!;
        }

        public static async Task<List<AppRole>> GetRoles()
        {
            List<AppRole>? roles = new List<AppRole>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetRoles"))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    roles = JsonSerializer.Deserialize<List<AppRole>>(jsonResponse);
                }
            }

            return roles!;
        }

        public static async Task<List<BasketItem>> GetBaskets()
        {
            List<BasketItem>? baskets = new List<BasketItem>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetBaskets"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        baskets = JsonSerializer.Deserialize<List<BasketItem>>(jsonData);
                    }
                }
            }

            return baskets!;
        }

        public static async Task<List<Brand>> GetBrands()
        {
            List<Brand>? brands = new List<Brand>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetBrands"))
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    brands = JsonSerializer.Deserialize<List<Brand>>(jsonData);
                }
            }

            return brands!;
        }

        public static async Task<List<Brand>> GetBrandsBySearchText(string? q)
        {
            List<Brand>? brands = new List<Brand>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetBrandsBySearchText/" + q))
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    brands = JsonSerializer.Deserialize<List<Brand>>(jsonResponse);
                }
            }

            return brands!;
        }

        public static async Task<List<Comment>> GetComments(int id)
        {
            List<Comment>? comments = new List<Comment>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetComments/" + id))
                {
                    if(response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        comments = JsonSerializer.Deserialize<List<Comment>>(jsonData);
                    }
                }
            }

            return comments!;
        }

        public static async Task<List<City>> GetCities()
        {
            List<City>? cities = new List<City>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetCities"))
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    cities = JsonSerializer.Deserialize<List<City>>(jsonData);
                }
            }

            return cities!;
        }

        public static async Task<City?> GetCity(int id)
        {
            City? city = new City();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetCity/" + id))
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    city = JsonSerializer.Deserialize<City>(jsonData);
                }
            }

            return city;
        }

        public static async Task<List<BrandSubCategory>> GetBrandSubCategories(int id)
        {
            List<BrandSubCategory>? brandSubCategories = new List<BrandSubCategory>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetBrandSubCategories/" + id))
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    brandSubCategories = JsonSerializer.Deserialize<List<BrandSubCategory>>(jsonData);
                }
            }

            return brandSubCategories!;
        }

        public static async Task<List<BrandSubCategory>> GetBrandSubCategoriesByCategoryId(int id)
        {
            List<BrandSubCategory>? brandSubCategories = new List<BrandSubCategory>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetBrandSubCategoriesByCategoryId/" + id))
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    brandSubCategories = JsonSerializer.Deserialize<List<BrandSubCategory>>(jsonData);
                }
            }

            return brandSubCategories!;
        }

        public static async Task<ProductViewModel?> GetProductViewModel(int id)
        {
            ProductViewModel? product = new ProductViewModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProduct/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        product = JsonSerializer.Deserialize<ProductViewModel>(jsonResponse);
                    }
                }
            }

            return product!;
        }

        public static async Task<Product?> GetProduct(int id)
        {
            Product? product = new Product();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProduct/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        product = JsonSerializer.Deserialize<Product>(jsonResponse);
                    }
                }
            }

            return product!;
        }

        public static async Task<Product?> GetProduct(string name)
        {
            Product? product = new Product();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductByProductName/" + name))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        product = JsonSerializer.Deserialize<Product>(jsonResponse);
                    }
                }
            }

            return product!;
        }

        public static async Task<Category?> GetCategory(int id)
        {
            Category? category = new Category();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetCategory/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        category = JsonSerializer.Deserialize<Category>(apiResponse);
                    }
                }
            }

            return category!;
        }

        public static async Task<SubCategoryViewModel?> GetSubCategory(int id)
        {
            SubCategoryViewModel? subCategory = new SubCategoryViewModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetSubCategory/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        subCategory = JsonSerializer.Deserialize<SubCategoryViewModel>(apiResponse);
                    }
                }
            }

            return subCategory!;
        }

        public static async Task<SubCategoryViewModel?> GetSubCategoryByUrl(string url)
        {
            SubCategoryViewModel? subCategory = new SubCategoryViewModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetSubCategoryByUrl/" + url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        subCategory = JsonSerializer.Deserialize<SubCategoryViewModel>(apiResponse);
                    }
                }
            }

            return subCategory!;
        }

        public static async Task<AppUserViewModel?> GetUser(string id)
        {
            AppUserViewModel? appUser = new AppUserViewModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetUser/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        appUser = JsonSerializer.Deserialize<AppUserViewModel>(apiResponse);
                    }
                }
            }

            return appUser!;
        }

        public static async Task<AppUser?> GetUserByEmail(string email)
        {
            AppUser? appUser = new AppUser();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetUserByEmail/" + email))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(apiResponse))
                        {
                            appUser = JsonSerializer.Deserialize<AppUser>(apiResponse);
                        }

                        else
                        {
                            appUser = null;
                        }
                    }
                }
            }

            return appUser!;
        }

        public static async Task<string?> GetUserIdByUserName(string userName)
        {
            string? userId = string.Empty;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetUserIdByUserName/" + userName))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        userId = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return userId!;
        }

        public static async Task<AppRole?> GetRole(string id)
        {
            AppRole? role = new AppRole();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetRole/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        role = JsonSerializer.Deserialize<AppRole>(jsonData);
                    }
                }
            }

            return role!;
        }

        public static async Task<AppRole?> GetRoleByRoleName(string name)
        {
            AppRole? role = new AppRole();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetRoleByRoleName/" + name))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        role = JsonSerializer.Deserialize<AppRole>(jsonData);
                    }
                }
            }

            return role!;
        }

        public static async Task<Brand?> GetBrand(int id)
        {
            Brand? brand = new Brand();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetBrand/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        brand = JsonSerializer.Deserialize<Brand>(jsonData);
                    }
                }
            }

            return brand!;
        }

        public static async Task<List<AppUserRolesViewModel>> GetUserRoles(string id)
        {
            List<AppUserRolesViewModel>? userRoles = new List<AppUserRolesViewModel>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetUserRoles/" + id))
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    userRoles = JsonSerializer.Deserialize<List<AppUserRolesViewModel>>(jsonData);
                }
            }

            return userRoles!;
        }

        public static string? GetHashedPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                    password: password,
                                    salt: salt,
                                    prf: KeyDerivationPrf.HMACSHA256,
                                    iterationCount: 100000,
                                    numBytesRequested: 256 / 8));

            return hashedPassword;
        }
    }
}
