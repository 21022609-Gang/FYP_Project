﻿using Microsoft.AspNetCore.Mvc;
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
using System.Text;
using OurWebAppTest.Services;
using System.Text.RegularExpressions;

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

    public IActionResult Welcome()
    {
        return View("Home/Welcome");
    }

    public IActionResult CreateAppUser()
    {
        return View("CreateAppUser");
    }

    [HttpPost]
    public IActionResult CreateAppUser(AppUser user)
    {
        var helper = new Helper(ModelState);

        if (helper.ValidatePart(nameof(AppUser.FirstName)) &&
            helper.ValidatePart(nameof(AppUser.LastName)) &&
            helper.ValidatePart(nameof(AppUser.Email)) &&
            helper.ValidatePart(nameof(AppUser.Password)))
        {
            string emailTest = user.Email;

            string sql = @"SELECT * FROM AppUser WHERE Email ='" + emailTest + "'";
            DataTable check = DBUtl.GetTable(sql);

            if (check.Rows.Count == 1)
            {
                ModelState.AddModelError("Email", "Email has already been registered");
                TempData["Msg"] = DBUtl.DB_Message;
                return View("CreateAppUser");
            }

            TempData["FName"] = user.FirstName;
            TempData["LName"] = user.LastName;
            TempData["Email"] = user.Email;
            TempData["Password"] = user.Password;
            return RedirectToAction("CreateAppUser2");
        }
        else
        {
            /* Use to check for errors
            // Create a StringBuilder to accumulate error messages
            var errorMessageBuilder = new StringBuilder();

            foreach (var modelStateEntry in ModelState.Values)
            {
                foreach (var error in modelStateEntry.Errors)
                {
                    // Append the error message to the StringBuilder
                    errorMessageBuilder.AppendLine(error.ErrorMessage);
                }
            }

            // Store the concatenated error messages in TempData
            TempData["msg"] = errorMessageBuilder.ToString();
            */

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
        var helper = new Helper(ModelState);
        string regPat = @"\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])"; //use to check date stuff cus asp does not work??

        if (TempData["Fname2"] != null)
        {
            TempData["Fname"] = TempData["Fname2"];
            TempData["LName2"] = TempData["LName"] as string;
            TempData["Email2"] = TempData["Email"] as string;
            TempData["Password2"] = TempData["Password"] as string;
        }

        string? FName = TempData["FName"] as string;
        string? LName = TempData["LName"] as string;
        string? Email = TempData["Email"] as string;
        string? Password = TempData["Password"] as string;
        string formatDOB = user.Dob.ToString("yyyy-MM-dd");
        bool datecheck = false;

        if (Regex.IsMatch(formatDOB, regPat))
        {
            if(user.Dob.Year < 2023)
            {
                datecheck = true;
            }
            else
            {
                ModelState.AddModelError("Dob", "Invalid Year");
            }
        }
        else
        {
            ModelState.AddModelError("Dob", "Invalid Format");
        }

        if (helper.ValidatePart(nameof(AppUser.Dob)) &&
            helper.ValidatePart(nameof(AppUser.HighestEdu)) &&
            helper.ValidatePart(nameof(AppUser.ContactInfo)) &&
            datecheck
            )
        {
            int age = helper.CalcAge(user.Dob);

            string sql = @"INSERT INTO AppUser (FirstName, LastName, Email, Password
                            , DOB, Age, HighestEdu, ContactInfo, Consent)
                            VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}', {8});";

            string sqlFormat = string.Format(sql, FName, LName, Email, Password,
                formatDOB, age, user.HighestEdu, user.ContactInfo, 1);

            int check = DBUtl.ExecSQL(sqlFormat);
            
            if(check != 1)
            {
                TempData["Msg"] = DBUtl.DB_Message;
                return View("CreateAppUser2");
            }
            else
            {
                return View("Home/Welcome");
            }

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

            ViewData["HighestEducation"] = new SelectList(HighestEducation, "Value", "Text");
            TempData["Fname2"] = TempData["FName"] as string;
            TempData["LName2"] = TempData["LName"] as string;
            TempData["Email2"] = TempData["Email"] as string;
            TempData["Password2"] = TempData["Password"] as string;
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
            string sql = @"SELECT * FROM `AppUser` 
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
