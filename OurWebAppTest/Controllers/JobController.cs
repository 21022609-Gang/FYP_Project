using Microsoft.AspNetCore.Mvc;
using OurWebAppTest.Models;
using RP.SOI.DotNet.Utils;
using System.Data;

namespace OurWebAppTest.Controllers
{
    public class JobController : Controller
    {
        public IActionResult JobPage()
        {
            string sql = @"SELECT * FROM `Job Listing`";
            DataTable ds = DBUtl.GetTable(sql);
            int check = ds.Rows.Count;

            List<JobListing> jobListings = ds.AsEnumerable()
            .Select(row => new JobListing
            {
                Job_ID = row.Field<int>("Job_ID"),
                Title = row.Field<string>("Title"),
                Location = row.Field<string>("Location"),
                Salary = row.Field<int>("Salary"),
                
            }).ToList();

            List<string?> locations = ds.AsEnumerable()
                .Select(x => x.Field<string>("Location")).ToList();

            List<SelectListItem> locationList = locations
                .Select(x => new SelectListItem
                {
                    Text = x,
                    Value = x
                }).ToList();

            ViewData["LocationList"] = locationList;
            ViewData["JobListings"] = jobListings;
            return View("Job");
        }

        [HttpPost] // maybe use?
        public IActionResult JobPage(JobListing jobL)
        {
            return View("Job");
        }
    }
}
