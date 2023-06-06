using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurWebAppTest.Models;
using OurWebAppTest.Views.Shared;
using System;
using System.Data;

public class UserController : Controller
{

    public IActionResult CreateUser()
    {
        return View("UserCreate");
    }

    [HttpPost]
    public IActionResult CreateUser(User user)
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
