using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurWebAppTest.Models;
using OurWebAppTest.Views.Shared;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using RP.SOI.DotNet.Services;
using RP.SOI.DotNet.Utils;

public class AppUserController : Controller
{
    private readonly IConfiguration _configuration;

    public AppUserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult CreateAppUser()
    {
        return View("CreateAppUser");
    }

    [HttpPost]
    public IActionResult CreateAppUser(AppUser user)
    {
        if (ModelState.IsValid)
        {

            return RedirectToAction("CreateAppUser");
        }
        else
        {
            TempData["msg"] = "Invalid information entered!";
            return View("CreateAppUser");
        }

    }

    public IActionResult CreateAppUserEmployer()
    {
        return View("CreateAppUserEmployer");
    }

    [HttpPost]
    public IActionResult CreateAppUserEmployer(AppUser user)
    {
        if (ModelState.IsValid)
        {

        }
        else
        {
            TempData["msg"] = "Invalid information entered!";
        }


        return RedirectToAction("CreateAppUser");

    }

    public IActionResult LoginAppUser()
    {
        return View("AppUserLogin");
    }

    [HttpPost]
    public IActionResult LoginAppUser(string email, string password)
    {
        if (ModelState.IsValid)
        {
            string sql = @"SELECT * FROM `User` 
                WHERE Email = '"+email+"' AND PASSWORD = '"+password+"'";
            DataTable check = DBUtl.GetTable(sql);
            if(check.Rows.Count == 1)
            {
                TempData["Msg"] = "Welcome";
                return View("CreateAppUser");
            }
            else
            {
                TempData["Msg"] = DBUtl.DB_Message;
                return RedirectToAction("LoginAppUser");
            }
        }
        else
        {
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage);
            foreach (var errorMessage in errorMessages)
            {
                TempData["Msg"] = errorMessage;
            }

            return RedirectToAction("LoginAppUser");
        }

    }

    public ActionResult MyAction()
    {
        
        string sql = @"INSERT INTO Test VALUES (300)";
        int rowsAffected = DBUtl.ExecSQL(sql);
        if(rowsAffected == 1)
        {
            TempData["Msg"] = "We did it";
        }
        else
        {
            TempData["Msg"] = DBUtl.DB_Message;
        }

        return View("CreateAppUser");
    }
}
