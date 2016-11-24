using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace ConsoleApplication
{
	public class HeaderCheckMiddleware
	{
		private readonly RequestDelegate next;

		public HeaderCheckMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Headers["customauthheader"] != "auth me!")
			{
				context.Response.StatusCode = 401; //Unauthorized
				var jsonString = "{ \"Error\" : \"Where my header, dude?\" }";
				context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
				await context.Response.WriteAsync(jsonString, Encoding.UTF8);
				return;
			}

			context.Items.Add("authorized", "yes!");

			await next.Invoke(context);
		}
	}
}
