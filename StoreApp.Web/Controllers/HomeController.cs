using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Concrete;
using System.Text;
using System.Text.Json;
using StoreApp.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApp.Web.Common;

namespace StoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHost;

        public HomeController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public async Task<IActionResult> Index(string? url)
        {
            if (string.IsNullOrEmpty(url))
            {
                ViewBag.Brands = await DataControl.GetBrands();
            }

            else
            {
                ViewBag.BrandSubCategories = await DataControl.GetBrandSubCategoriesByCategoryId((await DataControl.GetSubCategoryByUrl(url))!.Id);
            }
            
            ViewBag.MinPrice = 0;
            ViewBag.MaxPrice = 0;

            if (string.IsNullOrEmpty(url))
            {
                return View(await DataControl.GetProducts());
            }

            ViewBag.Url = url;
            ViewBag.SubCategoryName = (await DataControl.GetSubCategoryByUrl(url))!.Name;
            return View(await DataControl.GetProducts(url));
        }

        [HttpPost]
        public async Task<IActionResult> Index(int[] brands, float minPrice, float maxPrice, string? url)
        {
            if (string.IsNullOrEmpty(url))
            {
                ViewBag.Brands = await DataControl.GetBrands();
            }

            else
            {
                ViewBag.BrandSubCategories = await DataControl.GetBrandSubCategoriesByCategoryId((await DataControl.GetSubCategoryByUrl(url))!.Id);
            }

            ViewBag.SelectedBrands = brands;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Url = url;
            ViewBag.SubCategoryName = (await DataControl.GetSubCategoryByUrl(url ?? string.Empty))!.Name;

            if (brands.Length == 0 && minPrice == 0 && maxPrice == 0)
            {
                return View(string.IsNullOrEmpty(url) ? await DataControl.GetProducts() : (await DataControl.GetProducts()).Where(p => p.SubCategory.Url == url).ToList());
            }

            else if(brands.Length == 0 && minPrice == 0 && maxPrice > 0)
            {
                return View(string.IsNullOrEmpty(url) ? (await DataControl.GetProducts()).Where(p => p.Price <= maxPrice).ToList() : (await DataControl.GetProducts()).Where(p => p.Price <= maxPrice && p.SubCategory.Url == url).ToList());
            }

            else if (brands.Length == 0 && minPrice > 0 && maxPrice == 0)
            {
                return View(string.IsNullOrEmpty(url) ? (await DataControl.GetProducts()).Where(p => p.Price >= minPrice).ToList() : (await DataControl.GetProducts()).Where(p => p.Price >= minPrice && p.SubCategory.Url == url).ToList());
            }

            else if (brands.Length == 0 && minPrice > 0 && maxPrice > 0)
            {
                return View(string.IsNullOrEmpty(url) ? (await DataControl.GetProducts()).Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList() : (await DataControl.GetProducts()).Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.SubCategory.Url == url).ToList());
            }

            else if (brands.Length > 0 && minPrice == 0 && maxPrice == 0)
            {
                return View(string.IsNullOrEmpty(url) ? (await DataControl.GetProducts()).Where(p => brands.Contains(p.BrandId)).ToList() : (await DataControl.GetProducts()).Where(p => brands.Contains(p.BrandId) && p.SubCategory.Url == url).ToList());
            }

            else if (brands.Length > 0 && minPrice == 0 && maxPrice > 0)
            {
                return View(string.IsNullOrEmpty(url) ? (await DataControl.GetProducts()).Where(p => brands.Contains(p.BrandId) && p.Price <= maxPrice).ToList() : (await DataControl.GetProducts()).Where(p => brands.Contains(p.BrandId) && p.Price <= maxPrice && p.SubCategory.Url == url).ToList());
            }

            else if (brands.Length > 0 && minPrice > 0 && maxPrice == 0)
            {
                return View(string.IsNullOrEmpty(url) ? (await DataControl.GetProducts()).Where(p => brands.Contains(p.BrandId) && p.Price >= minPrice).ToList() : (await DataControl.GetProducts()).Where(p => brands.Contains(p.BrandId) && p.Price >= minPrice && p.SubCategory.Url == url).ToList());
            }

            return View(string.IsNullOrEmpty(url) ? (await DataControl.GetProducts()).Where(p => brands.Contains(p.BrandId) && p.Price >= minPrice && p.Price <= maxPrice).ToList() : (await DataControl.GetProducts()).Where(p => brands.Contains(p.BrandId) && p.Price >= minPrice && p.Price <= maxPrice && p.SubCategory.Url == url).ToList());
        }

        public async Task<IActionResult> Register()
        {
            ViewBag.Roles = new SelectList(await DataControl.GetRoles(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUser model, IFormFile userImg, string[] Roles)
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
                            model.NormalizedUserName = model.UserName;
                            model.NormalizedEmail = model.Email;
                            model.PasswordHash = DataControl.GetHashedPassword(model.Password);
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
                                    if(Roles.Length > 0)
                                    {
                                        string jsonData = await reponse.Content.ReadAsStringAsync();
                                        string userId = JsonSerializer.Deserialize<AppUser>(jsonData)!.Id;
                                        AppUserRolesViewModel userRolesViewModel = new AppUserRolesViewModel();
                                        userRolesViewModel.UserId = userId;

                                        foreach(string roleId in Roles)
                                        {
                                            userRolesViewModel.RoleId = roleId;
                                            var serializedModelUserRoles = JsonSerializer.Serialize(userRolesViewModel);
                                            StringContent contentUserRoles = new StringContent(serializedModelUserRoles, Encoding.UTF8, "application/json");
                                            await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateUserRoles/", contentUserRoles);
                                        }

                                        ViewBag.RegisterMessage = "Email hesabýnýza gönderilen onay mailine týklayýnýz.";
                                        return RedirectToAction("Login");
                                    }

                                    else
                                    {
                                        ViewBag.RegisterMessage = "Email hesabýnýza gönderilen onay mailine týklayýnýz.";
                                        return RedirectToAction("Login");
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        ViewBag.Message = "The record has already exists.";
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
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }
    }
}
