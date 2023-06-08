using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurWebAppTest.Models;
using OurWebAppTest.Views.Shared;
using System;
using System.Data;

public class AppUserController : Controller
{

    public IActionResult CreateAppUser()
    {
        return View("AppUserCreate");
    }

    [HttpPost]
    public IActionResult CreateAppUser(AppUser user)
    {
        if (ModelState.IsValid)
        {

        }
        else
        {
            TempData["msg"] = "Invalid information entered!";
        }


        return RedirectToAction("Main");

    }

    public IActionResult LoginAppUser()
    {
        return View("AppUserLogin");
    }

    [HttpPost]
    public IActionResult AppUserLogin(AppUser user)
    {
        if (ModelState.IsValid)
        {

        }
        else
        {
            TempData["msg"] = "Invalid information entered!";
        }


        return RedirectToAction("Main");

    }
}
