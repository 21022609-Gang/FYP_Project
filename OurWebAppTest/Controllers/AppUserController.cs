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
using System.ComponentModel;

public class AppUserController : Controller
{
    private readonly IConfiguration _configuration;

    public AppUserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult TermsAndConditions()
    {
        return View("TaC");
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

    public IActionResult CreateAppUser2()
    {

        List<SelectListItem> HighestEducation = new List<SelectListItem>
        {
            new SelectListItem { Value = "Primary", Text = "Primary" },
            new SelectListItem { Value = "N Levels", Text = "N Levels" },
            new SelectListItem { Value = "O Levels", Text = "O Levels" },
            new SelectListItem { Value = "A Levels", Text = "A Levels" },
            new SelectListItem { Value = "Vocation", Text = "Vocation" },
            new SelectListItem { Value = "Diploma", Text = "Diploma" },
            new SelectListItem { Value = "Bachelor", Text = "Bachelor" },
            new SelectListItem { Value = "Master", Text = "Master" },
            new SelectListItem { Value = "Doctoral", Text = "Doctoral" }
        };

        ViewData["HighestEducation"] = new SelectList(HighestEducation, "Value", "Text");

        return View("CreateAppUser2");
    }

    [HttpPost]
    public IActionResult CreateAppUser2(AppUser user)
    {
        if (ModelState.IsValid)
        {

            return RedirectToAction("CreateAppUser2");
        }
        else
        {
            List<SelectListItem> HighestEducation = new List<SelectListItem>
            {
                new SelectListItem { Value = "Primary", Text = "Primary" },
                new SelectListItem { Value = "N Levels", Text = "N Levels" },
                new SelectListItem { Value = "O Levels", Text = "O Levels" },
                new SelectListItem { Value = "A Levels", Text = "A Levels" },
                new SelectListItem { Value = "Vocation", Text = "Vocation" },
                new SelectListItem { Value = "Diploma", Text = "Diploma" },
                new SelectListItem { Value = "Bachelor", Text = "Bachelor" },
                new SelectListItem { Value = "Master", Text = "Master" },
                new SelectListItem { Value = "Doctoral", Text = "Doctoral" }
            };

            TempData["msg"] = "Invalid information entered!";

            ViewData["HighestEducation"] = new SelectList(HighestEducation, "Value", "Text");
            return View("CreateAppUser2");
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
