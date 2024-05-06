using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using StoreApp.Web.Models;
using System.Text;
using System.Text.Json;

namespace StoreApp.Web.Controllers
{
    public class BrandsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await DataControl.GetBrands());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.SubCategories = new SelectList(await DataControl.GetSubCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand model, int[] SubCategories)
        {
            if (ModelState.IsValid)
            {
                if (!await DataControl.IsBrandRecordExists(model))
                {
                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateBrand", content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                if (SubCategories.Length > 0)
                                {
                                    foreach (int subCategoryId in SubCategories)
                                    {
                                        string jsonData = await response.Content.ReadAsStringAsync();
                                        Brand? brand = JsonSerializer.Deserialize<Brand>(jsonData);
                                        BrandSubCategoryViewModel modelBrandSubCategory = new BrandSubCategoryViewModel();
                                        modelBrandSubCategory.BrandId = brand!.Id;
                                        modelBrandSubCategory.SubCategoryId = subCategoryId;
                                        var serializedBrandSubCategoryModel = JsonSerializer.Serialize(modelBrandSubCategory);
                                        StringContent contentBrandSubCategory = new StringContent(serializedBrandSubCategoryModel, Encoding.UTF8, "application/json");
                                        await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateBrandSubCategory", contentBrandSubCategory);
                                    }

                                    return RedirectToAction("Index");
                                }

                                else
                                {
                                    return RedirectToAction("Index");
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

            ViewBag.SubCategories = new SelectList(await DataControl.GetSubCategories(), "Id", "Name", SubCategories);
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.SubCategories = new SelectList(await DataControl.GetSubCategories(), "Id", "Name");
            return View(await DataControl.GetBrand(id ?? 0));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Brand model, int[] SubCategories)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var serializedModel = JsonSerializer.Serialize(model);
                    StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditBrand", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            if (SubCategories.Length > 0)
                            {
                                using (var responseDeleteSubCategories = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteBrandSubCategories/" + id))
                                {
                                    if (responseDeleteSubCategories.IsSuccessStatusCode)
                                    {
                                        foreach (int subCategoryId in SubCategories)
                                        {
                                            BrandSubCategoryViewModel mdlBrandSubCategory = new BrandSubCategoryViewModel();
                                            mdlBrandSubCategory.BrandId = model.Id;
                                            mdlBrandSubCategory.SubCategoryId = subCategoryId;
                                            var serializedModelBrandSubCategory = JsonSerializer.Serialize(mdlBrandSubCategory);
                                            StringContent contentBrandSubCategory = new StringContent(serializedModelBrandSubCategory, Encoding.UTF8, "application/json");
                                            await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateBrandSubCategory", contentBrandSubCategory);
                                        }

                                        return RedirectToAction("Index");
                                    }
                                }
                            }

                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
            }

            ViewBag.SubCategories = new SelectList(await DataControl.GetSubCategories(), "Id", "Name", SubCategories);
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await DataControl.GetBrand(id ?? 0));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Brand model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteBrand/" + id))
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
