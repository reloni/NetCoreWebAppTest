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
			return Json(repo.Users().Select(o => new { Id = o.Id, FirstName = o.FirstName, LastName = o.LastName, EMail = o.Email }));
		}

		public JsonResult Todos()
		{
			return Json(repo.context.Todos.ToList());
		}

		public JsonResult CreateTodo(string descr)
		{
			var newId = repo.TodoCount() + 1;
			var user = repo.Users().Last();
			var todo = new Todo() { Description = descr, Id = newId, User = user };
			repo.context.Todos.Add(todo);
			repo.context.SaveChanges();
			return Json(todo);
		}
    }
}