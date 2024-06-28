using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using System.Text;
using System.Text.Json;

namespace StoreApp.Web.Controllers
{
    public class CitiesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetCities());
        }

        public IActionResult Create()
        {
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(City model)
        {
            if(ModelState.IsValid)
            {
                if(!await DataControl.IsCityRecordExists(model))
                {
                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateCity", content))
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

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetCity(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, City model)
        {
            if(id != model.Id)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                if(!await DataControl.IsCityRecordExists(model))
                {
                    using(var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using(var response = await httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditCity", content))
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

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetCity(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, City model)
        {
            if(id != model.Id)
            {
                return BadRequest();
            }

            using(var httpClient = new HttpClient())
            {
                using(var response = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteCity/" + id))
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
