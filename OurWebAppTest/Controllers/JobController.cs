using Microsoft.AspNetCore.Mvc;

namespace OurWebAppTest.Controllers
{
    public class JobController : Controller
    {
        public IActionResult JobPage()
        {
            return View("Job");
        }
    }
}
