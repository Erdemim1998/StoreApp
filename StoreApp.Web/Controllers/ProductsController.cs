using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using StoreApp.Web.Models;
using System.Text;
using System.Text.Json;

namespace StoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _webHost;

        public ProductsController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public async Task<IActionResult> Index(int? productId)
        {
            List<Product>? products = await DataControl.GetProducts();

            if (productId != null)
            {
                ViewBag.ProductImages = await DataControl.GetProductImages(productId ?? 0);
            }

            ViewBag.ProductId = productId;
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(products);
        }

        public async Task<IActionResult> Detail(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return NotFound();
            }

            Product? product = await DataControl.GetProductByUrl(url);
            List<Comment>? comments = await DataControl.GetComments(product!.Id);
            ViewBag.CommentCount = comments!.Count;
            ViewBag.Comments = comments;
            ViewBag.Users = new SelectList(await DataControl.GetUsers(), "Id", "FullName");
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            return View(product);
        }

        [HttpPost]
        public async Task<JsonResult?> AddComment(Comment comment)
        {
            using(var httpClient = new HttpClient())
            {
                var serializedModel = JsonSerializer.Serialize(comment);
                StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
                await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateComment/", content);
            }

            AppUserViewModel? appUserViewModel = await DataControl.GetUser(comment.UserId);
            string CommentDate = comment.CommentDate.ToString("dd.MM.yyyy HH:mm");

            if (!string.IsNullOrEmpty(comment.CommentText))
            {
                return Json(new
                {
                    appUserViewModel!.Image,
                    appUserViewModel!.FullName,
                    CommentDate,
                    comment.CommentText
                });
            }

            else
            {
                return Json(null);
            }
        }

        [HttpPost]
        public async Task<JsonResult?> AddBasket(BasketItemViewModel basket, int count)
        {
            for(int i = 0; i < count; i++)
            {
                using (var httpClient = new HttpClient())
                {
                    var serializedModel = JsonSerializer.Serialize(basket);
                    StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
                    await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateBasket/", content);
                }
            }

            return Json(NoContent());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.SubCategories = await DataControl.GetSubCategories();
            ViewBag.Brands = await DataControl.GetBrands();
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await DataControl.IsRecordExists(model))
                {
                    using (var httpClient = new HttpClient())
                    {
                        if (!string.IsNullOrEmpty(model.Ml2Name) && string.IsNullOrEmpty(model.Ml1Name))
                        {
                            model.Ml1Name = model.Ml2Name;
                        }

                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateProduct/", content).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index", new { lang = HttpContext.Request.Query["lang"].ToString() });
                            }
                        }
                    }
                }

                else
                {
                    ViewBag.Message = "Böyle bir kayıt zaten var.";
                }
            }

            ViewBag.SubCategories = await DataControl.GetSubCategories();
            ViewBag.Brands = await DataControl.GetBrands();
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.SubCategories = await DataControl.GetSubCategories();
            ViewBag.Brands = await DataControl.GetBrands();
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetProductViewModel(id ?? 0));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, ProductViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (!await DataControl.IsRecordExists(model))
                {
                    using (var httpClient = new HttpClient())
                    {
                        if(!string.IsNullOrEmpty(model.Ml2Name) && string.IsNullOrEmpty(model.Ml1Name))
                        {
                            model.Ml1Name = model.Ml2Name;
                        }

                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditProduct/", content).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index", new { lang = HttpContext.Request.Query["lang"].ToString() });
                            }
                        }
                    }
                }

                else
                {
                    ViewBag.Message = "Böyle bir kayıt zaten var.";
                }
            }

            ViewBag.SubCategories = await DataControl.GetSubCategories();
            ViewBag.Brands = await DataControl.GetBrands();
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetProductViewModel(id ?? 0));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, ProductViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteProduct/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", new { lang = HttpContext.Request.Query["lang"].ToString() });
                    }
                }
            }

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(model);
        }

        public IActionResult CreateProductImage(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            ProductImageViewModel model = new ProductImageViewModel();
            model.ProductId = productId ?? 0;
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            ViewBag.ProductId = model.ProductId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(ProductImageViewModel model, List<IFormFile> files)
        {
            if (files.Count > 0)
            {
                string rootPath = Path.Combine(_webHost.WebRootPath, "img");

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                foreach (IFormFile file in files)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(rootPath, fileName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    model.Url = $"/img/{fileName}";

                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
                        await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateProductImage", content);
                    }
                }

                return RedirectToAction("Index", new { lang = HttpContext.Request.Query["lang"].ToString() });
            }

            ModelState.AddModelError("", "Resim bilgisi zorunlu.");
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            ViewBag.ProductId = model.ProductId;
            return View();
        }

        public async Task<IActionResult> EditProductImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductImageViewModel? model = new ProductImageViewModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductImage/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        model = JsonSerializer.Deserialize<ProductImageViewModel>(jsonData);
                    }
                }
            }

            ViewBag.Products = new SelectList(await DataControl.GetProducts(), "Id", "Name");
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProductImage(int id, ProductImageViewModel model, IFormFile file)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if(ModelState["file"]!.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState["file"]!.ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                ModelState["file"]!.Errors.Clear();
            }

            if (file != null)
            {
                string rootPath = Path.Combine(_webHost.WebRootPath, "img");

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

                model.Url = $"/img/{fileName}";
            }

            using (var httpClient = new HttpClient())
            {
                var serializedModel = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditProductImage", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", new { lang = HttpContext.Request.Query["lang"].ToString() });
                    }
                }
            }

            ViewBag.Products = new SelectList(await DataControl.GetProducts(), "Id", "Name");
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(model);
        }

        public async Task<IActionResult> DeleteProductImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductImage? productImage = new ProductImage();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5292/api/StoreApp/GetProductImage/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        productImage = JsonSerializer.Deserialize<ProductImage>(jsonResponse);
                    }
                }
            }

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(productImage);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int id, ProductImage model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteProductImage/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", new { lang = HttpContext.Request.Query["lang"].ToString() });
                    }
                }
            }

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(model);
        }
    }
}
