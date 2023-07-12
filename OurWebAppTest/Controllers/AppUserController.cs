using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurWebAppTest.Models;
using OurWebAppTest.Views.Shared;
using System;
using System.Data;
using MySql.Data.MySqlClient;


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

    public ActionResult MyAction()
    {
        string connectionString = "server=db4free.net;database=rp21022609;uid=rp21022609;pwd=rp21022609;";
        MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            string sql = "INSERT INTO Test VALUES (5)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
            TempData["Msg"] = "User created successfully!";
            connection.Close();
        }
        catch (Exception ex)
        {
            TempData["Msg"] = "Error creating user: " + ex.Message;
        }

        return View("AppUserCreate");
    }
}
