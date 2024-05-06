﻿using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using System.Text;
using System.Text.Json;

namespace StoreApp.Web.Controllers
{
    public class RolesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<AppRole> roles = await DataControl.GetRoles();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppRole model)
        {
            if (ModelState.IsValid)
            {
                if (!await DataControl.IsRoleRecordExists(model))
                {
                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateRole/", content))
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

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            return View(await DataControl.GetRole(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, AppRole model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (!await DataControl.IsRoleRecordExists(model))
                {
                    using (var httpClient = new HttpClient())
                    {
                        var serializedModel = JsonSerializer.Serialize(model);
                        StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditRole/", content))
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

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            return View(await DataControl.GetRole(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, AppRole model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteRole/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Roles");
                    }
                }
            }

            return View(model);
        }
    }
}
