using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NetCoreWebAppTest
{
	public class Repository
	{
		internal DatabaseContext context { get; set; }

		public Repository(DatabaseContext context)
		{
			this.context = context;
		}

		public IEnumerable<User> Users()
		{
			//return new DatabaseContext(new DbContextOptions<DatabaseContext>()).Users;
			return context.Users;
		}

		public int TodoCount()
		{
			return context.Todos.Count();
		}
	}

	public class DatabaseContext : DbContext
	{
		
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
		{
			
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Todo> Todos { get; set; }
	}

	[Table("user")]
	public class User
	{
		[Column("id")]
		public int Id { get; set; }
		[Column("firstname")]
		public string FirstName { get; set; }
		[Column("lastname")]
		public string LastName { get; set; }
		[Column("email")]
		public string Email { get; set; }
		[Column("password")]
		public string Password { get; set; }
	}
		

	[Table("todo")]
	public class Todo
	{
		[Column("id")]
		public int Id { get; set; }
		[Column("description")]
		public string Description { get; set; }
		//[Column("userid")]
		//public int UserId { get; set; }
		[ForeignKey("userid")]
		public User User { get; set; }
	}
}
