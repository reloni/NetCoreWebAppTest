using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebAppTest;

namespace ConsoleApplication
{
    public class HomeController : Controller
    {
		Repository repo { get; set; }

		public HomeController(DatabaseContext databaseContext)
		{
			repo = new Repository(databaseContext);
		}

        public JsonResult Time()
        {
            return Json(new { Type = "Time", Value = DateTime.Now.ToString("hh:mm:ss tt") });
        }

        public JsonResult Year() 
        {
            return Json(new { Type = "Year", Value = DateTime.Now.Year });
        }

		public JsonResult Users()
		{
			var count = repo.Users().Count();
			return Json(repo.Users().Select(o => new { Id = o.Id, FirstName = o.FirstName, LastName = o.LastName, EMail = o.Email }));
		}
    }
}