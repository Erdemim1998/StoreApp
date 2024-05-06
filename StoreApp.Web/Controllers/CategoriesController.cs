using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using StoreApp.Web.Models;
using System.Text;
using System.Text.Json;

namespace StoreApp.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public async Task<IActionResult> Index(int? categoryId)
        {
            List<Category>? categories = await DataControl.GetCategories();

            if (categoryId != null)
            {
                List<SubCategory>? subCategories = await DataControl.GetSubCategories(categoryId ?? 0);
                ViewBag.SubCategories = subCategories;
            }

            ViewBag.CategoryId = categoryId;
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                if (!await DataControl.IsCategoryRecordExists(model))
                {
                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateCategory/", content).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }

                else
                {
                    ViewBag.Message = "The record has already exists.";
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await DataControl.GetCategory(id ?? 0));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!await DataControl.IsCategoryRecordExists(model))
                {
                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditCategory/", content).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }

                else
                {
                    ViewBag.Message = "The record has already exists.";
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await DataControl.GetCategory(id ?? 0));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Category model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteCategory/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }

        public IActionResult CreateSubCategory(int categoryId)
        {
            SubCategoryViewModel subCategory = new SubCategoryViewModel();
            subCategory.CategoryId = categoryId;
            return View(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(SubCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await DataControl.IsSubCategoryRecordExists(new SubCategory { Id = model.Id, Name = model.Name, Url = model.Url, CategoryId = model.CategoryId }))
                {
                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateSubCategory/", content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }

                else
                {
                    ViewBag.Message = "The record has already exists.";
                }
            }

            return View(model);
        }

        public async Task<IActionResult> EditSubCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await DataControl.GetCategories();
            return View(await DataControl.GetSubCategory(id ?? 0));
        }

        [HttpPost]
        public async Task<IActionResult> EditSubCategory(int id, SubCategoryViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (!await DataControl.IsSubCategoryRecordExists(new SubCategory { Id = model.Id, Name = model.Name, Url = model.Url, CategoryId = model.CategoryId }))
                {
                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditSubCategory/", content).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }

                else
                {
                    ViewBag.Message = "The record has already exists.";
                }
            }

            ViewBag.Categories = await DataControl.GetCategories();
            return View(model);
        }

        public async Task<IActionResult> DeleteSubCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await DataControl.GetSubCategory(id ?? 0));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubCategory(int? id, SubCategoryViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteSubCategory/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }
    }
}
