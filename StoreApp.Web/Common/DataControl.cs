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
                    var existsProduct = products!.FirstOrDefault(p => p.Name == model.Name && p.Price == model.Price && p.Description == model.Description &&
                                          p.SubCategoryId == model.SubCategoryId && p.BrandId == model.BrandId);

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
                    var existsProduct = categories!.FirstOrDefault(c => c.Name == model.Name);

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
            var existsSubCategory = subCategories!.FirstOrDefault(sc => sc.Name == model.Name && sc.Url == model.Url && sc.CategoryId == model.CategoryId);

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
