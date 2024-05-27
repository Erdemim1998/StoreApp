using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Concrete;
using System.Text;
using System.Text.Json;
using StoreApp.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApp.Web.Common;
using System.Data;

namespace StoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHost;

        public HomeController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public async Task<IActionResult> Index(string brands, float minPrice, float maxPrice, string? url, string? q, int PageIndex = 1)
        {
            List<Product> products = await DataControl.GetProducts();
            List<int> Brands = new List<int>();

            if (!string.IsNullOrEmpty(brands))
            {
                foreach (string brandId in brands.Split(","))
                {
                    Brands.Add(int.Parse(brandId));
                }
            }
            
            if (string.IsNullOrEmpty(url))
            {
                ViewBag.Brands = await DataControl.GetBrands();

                if (!string.IsNullOrEmpty(q) && q.Length >= 2)
                {
                    ViewBag.qParam = q;
                    products = await DataControl.GetProductsBySearchText(q);
                    List<SubCategory> subCategories = await DataControl.GetSubCategoriesBySearchText(q);
                    List<Brand> brandsSearch = await DataControl.GetBrandsBySearchText(q);

                    if(products.Count == 0)
                    {
                        if (subCategories.Count > 0)
                        {
                            foreach (SubCategory subCategory in subCategories)
                            {
                                foreach (Product product in await DataControl.GetProductsBySubCategoryId(subCategory.Id))
                                {
                                    if (products.FirstOrDefault(p => p.Id == product.Id) is null)
                                    {
                                        products.Add(product);
                                    }
                                }
                            }
                        }

                        if (brandsSearch.Count > 0)
                        {
                            foreach (Brand brand in brandsSearch)
                            {
                                foreach (Product product in await DataControl.GetProductsByBrandId(brand.Id))
                                {
                                    if (products.FirstOrDefault(p => p.Id == product.Id) is null)
                                    {
                                        products.Add(product);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            else
            {
                ViewBag.BrandSubCategories = await DataControl.GetBrandSubCategoriesByCategoryId((await DataControl.GetSubCategoryByUrl(url))!.Id);
                ViewBag.Url = url;
                ViewBag.SubCategoryName = (await DataControl.GetSubCategoryByUrl(url))!.Name;
            }

            ViewBag.SelectedBrands = Brands;
            ViewBag.SelBrandsStr = brands;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            if (Brands.Count == 0 && minPrice == 0 && maxPrice == 0)
            {
                if (string.IsNullOrEmpty(q))
                {
                    return View(string.IsNullOrEmpty(url) ? await DataControl.GetProductsByPageIndex(PageIndex) : await DataControl.GetProducts(url, PageIndex));
                }

                else
                {
                    ViewBag.ProductCount = products.Count;
                    return View(products.Skip((PageIndex - 1) * 8).Take(8).ToList());
                }
            }

            else if (Brands.Count == 0 && minPrice == 0 && maxPrice > 0)
            {
                if (string.IsNullOrEmpty(q))
                {
                    return View(string.IsNullOrEmpty(url) ? await DataControl.GetProductsByPageIndexMaxPrice(PageIndex, maxPrice) : await DataControl.GetProductsByPageIndexMaxPrice(PageIndex, maxPrice, url));
                }

                else
                {
                    ViewBag.ProductCount = products.Where(p => p.Price <= maxPrice).ToList().Count;
                    return View(products.Where(p => p.Price <= maxPrice).Skip((PageIndex - 1) * 8).Take(8).ToList());
                }
            }

            else if (Brands.Count == 0 && minPrice > 0 && maxPrice == 0)
            {
                if (string.IsNullOrEmpty(q))
                {
                    return View(string.IsNullOrEmpty(url) ? await DataControl.GetProductsByPageIndexMinPrice(PageIndex, minPrice) : await DataControl.GetProductsByPageIndexMinPrice(PageIndex, minPrice, url));
                }

                else
                {
                    ViewBag.ProductCount = products.Where(p => p.Price >= minPrice).ToList().Count;
                    return View(products.Where(p => p.Price >= minPrice).Skip((PageIndex - 1) * 8).Take(8).ToList());
                }
            }

            else if (Brands.Count == 0 && minPrice > 0 && maxPrice > 0)
            {
                if (string.IsNullOrEmpty(q))
                {
                    return View(string.IsNullOrEmpty(url) ? await DataControl.GetProductsByPageIndexMinMaxPrice(PageIndex, minPrice, maxPrice) : await DataControl.GetProductsByPageIndexMinMaxPrice(PageIndex, minPrice, maxPrice, url));
                }

                else
                {
                    ViewBag.ProductCount = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList().Count;
                    return View(products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).Skip((PageIndex - 1) * 8).Take(8).ToList());
                }
            }

            else if (Brands.Count > 0 && minPrice == 0 && maxPrice == 0)
            {
                if (string.IsNullOrEmpty(q))
                {
                    return View(string.IsNullOrEmpty(url) ? await DataControl.GetProductsByPageIndexBrands(PageIndex, brands) : await DataControl.GetProductsByPageIndexBrands(PageIndex, brands, url));
                }

                else
                {
                    ViewBag.ProductCount = products.Where(p => Brands.Contains(p.BrandId)).ToList().Count;
                    return View(products.Where(p => Brands.Contains(p.BrandId)).Skip((PageIndex - 1) * 8).Take(8).ToList());
                }
            }

            else if (Brands.Count > 0 && minPrice == 0 && maxPrice > 0)
            {
                if (string.IsNullOrEmpty(q))
                {
                    return View(string.IsNullOrEmpty(url) ? await DataControl.GetProductsByPageIndexBrandsMaxPrice(PageIndex, brands, maxPrice) : await DataControl.GetProductsByPageIndexBrandsMaxPrice(PageIndex, brands, maxPrice, url));
                }

                else
                {
                    ViewBag.ProductCount = products.Where(p => Brands.Contains(p.BrandId) && p.Price <= maxPrice).ToList().Count;
                    return View(products.Where(p => Brands.Contains(p.BrandId) && p.Price <= maxPrice).Skip((PageIndex - 1) * 8).Take(8).ToList());
                }
            }

            else if (Brands.Count > 0 && minPrice > 0 && maxPrice == 0)
            {
                if (string.IsNullOrEmpty(q))
                {
                    return View(string.IsNullOrEmpty(url) ? await DataControl.GetProductsByPageIndexBrandsMinPrice(PageIndex, brands, minPrice) : await DataControl.GetProductsByPageIndexBrandsMinPrice(PageIndex, brands, minPrice, url));
                }

                else
                {
                    ViewBag.ProductCount = products.Where(p => Brands.Contains(p.BrandId) && p.Price >= minPrice).ToList().Count;
                    return View(products.Where(p => Brands.Contains(p.BrandId) && p.Price >= minPrice).Skip((PageIndex - 1) * 8).Take(8).ToList());
                }
            }

            else
            {
                if (string.IsNullOrEmpty(q))
                {
                    return View(string.IsNullOrEmpty(url) ? await DataControl.GetProductsByPageIndexBrandsMinMaxPrice(PageIndex, brands, minPrice, maxPrice) : await DataControl.GetProductsByPageIndexBrandsMinMaxPrice(PageIndex, brands, minPrice, maxPrice, url));
                }

                else
                {
                    ViewBag.ProductCount = products.Where(p => Brands.Contains(p.BrandId) && p.Price >= minPrice && p.Price <= maxPrice).ToList().Count;
                    return View(products.Where(p => Brands.Contains(p.BrandId) && p.Price >= minPrice && p.Price <= maxPrice).Skip((PageIndex - 1) * 8).Take(8).ToList());
                }
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUser model, IFormFile userImg)
        {
            if (userImg != null)
            {
                if (ModelState["Image"]!.ValidationState == ModelValidationState.Invalid)
                {
                    ModelState["Image"]!.ValidationState = ModelValidationState.Valid;
                    ModelState["Image"]!.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    if (!await DataControl.IsUserRecordExists(new AppUserViewModel { UserName = model.UserName!, Email = model.Email! }))
                    {
                        using (var httpClient = new HttpClient())
                        {
                            string rootPath = Path.Combine(_webHost.WebRootPath, "img\\avatars");

                            if (!Directory.Exists(rootPath))
                            {
                                Directory.CreateDirectory(rootPath);
                            }

                            string fileName = Path.GetFileName(userImg.FileName);
                            string path = Path.Combine(rootPath, fileName);

                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                await userImg.CopyToAsync(stream);
                            }

                            model.Image = $"/img/avatars/{fileName}";
                            var serializedModel = JsonSerializer.Serialize(model);
                            StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                            using (var reponse = await httpClient.PostAsync("http://localhost:5292/api/StoreApp/Register/", content))
                            {
                                if (reponse.IsSuccessStatusCode)
                                {
                                    string jsonData = await reponse.Content.ReadAsStringAsync();
                                    string userId = JsonSerializer.Deserialize<AppUser>(jsonData)!.Id;
                                    AppRole? role = await DataControl.GetRoleByRoleName("customer");

                                    if (role != null)
                                    {
                                        AppUserRolesViewModel userRolesViewModel = new AppUserRolesViewModel();
                                        userRolesViewModel.UserId = userId;
                                        userRolesViewModel.RoleId = role.Id!;
                                        var serializedModelUserRoles = JsonSerializer.Serialize(userRolesViewModel);
                                        StringContent contentUserRoles = new StringContent(serializedModelUserRoles, Encoding.UTF8, "application/json");
                                        await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateUserRoles/", contentUserRoles);
                                    }
                                   
                                    return RedirectToAction("Login");
                                }
                            }
                        }
                    }

                    else
                    {
                        ViewBag.Message = "Böyle bir kayýt zaten var.";
                    }
                }
            }

            else
            {
                ModelState["userImg"]!.ValidationState = ModelValidationState.Valid;
                ModelState["userImg"]!.Errors.Clear();
            }

            ViewBag.Roles = new SelectList(await DataControl.GetRoles(), "Id", "Name");
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                using(var httpClient = new HttpClient())
                {
                    var serializedModel = JsonSerializer.Serialize(model);
                    StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                    using(var response = await httpClient.PostAsync("http://localhost:5292/api/StoreApp/Login/", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonData = await response.Content.ReadAsStringAsync();
                            Authentication? auth = JsonSerializer.Deserialize<Authentication>(jsonData);

                            if (!string.IsNullOrEmpty(auth!.Token))
                            {
                                HttpContext.Response.Cookies.Delete("token");
                                HttpContext.Response.Cookies.Delete("isAdmin");
                                HttpContext.Response.Cookies.Delete("userId");
                                HttpContext.Response.Cookies.Append("token", "Bearer " + auth.Token);
                                HttpContext.Response.Cookies.Append("isAdmin", auth.IsAdmin.ToString());
                                HttpContext.Response.Cookies.Append("userId", auth.UserId!);
                            }

                            return RedirectToAction("Index");
                        }

                        else
                        {
                            ModelState.AddModelError("", "Kullanýcý adý veya parola hatalý.");
                        }
                    }
                }
            }

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            HttpContext.Response.Cookies.Delete("isAdmin");
            HttpContext.Response.Cookies.Delete("userId");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MyProfile()
        {
            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            return View(await DataControl.GetUser(ViewBag.UserId));
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(AppUserViewModel model, IFormFile file)
        {
            if(file == null)
            {
                ModelState["file"]!.ValidationState = ModelValidationState.Valid;
                ModelState["file"]!.Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    if(file != null)
                    {
                        string rootPath = Path.Combine(_webHost.WebRootPath, "img\\avatars");

                        if (!Directory.Exists(rootPath))
                        {
                            Directory.CreateDirectory(rootPath);
                        }

                        string fileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(rootPath, fileName);

                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        model.Image = $"/img/avatars/{fileName}";
                    }
                        
                    var serializedModel = JsonSerializer.Serialize(model);
                    StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
                    await httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditUser", content);
                }
            }

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            return View(model);
        }

        public async Task<IActionResult> Profile(string id)
        {
            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            if (ViewBag.UserId == id)
            {
                return RedirectToAction("MyProfile");
            }

            return View(await DataControl.GetUser(id));
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(await DataControl.GetUserByEmail(model.Email) != null)
                {
                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditUserPassword", content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Login");
                            }
                        }
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Girilen email bilgisine ait kullanýcý bulunamadý.");
                }
            }

            return View(model);
        }
    }
}
