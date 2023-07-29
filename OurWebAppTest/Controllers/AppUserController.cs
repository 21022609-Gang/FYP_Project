using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurWebAppTest.Models;
using OurWebAppTest.Views.Shared;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

public class AppUserController : Controller
{
    private readonly IConfiguration _configuration;

    public AppUserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private const string ConnectionStringKey = "MyDbConnection";

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

    public IActionResult CreateAppUserEmployer()
    {
        return View("AppUserCreateEmployer");
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
        string? connectionString = _configuration.GetConnectionString(ConnectionStringKey);
        MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            string sql = "INSERT INTO Test VALUES (10)";
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
