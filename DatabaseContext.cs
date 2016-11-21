using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NetCoreWebAppTest
{
	public class Repository
	{
		DatabaseContext context { get; set; }

		public Repository(DatabaseContext context)
		{
			this.context = context;
		}

		public IEnumerable<User> Users()
		{
			//return new DatabaseContext(new DbContextOptions<DatabaseContext>()).Users;
			return context.Users;
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
		[Column("Description")]
		public string Description { get; set; }
		[Column("UserId")]
		public User UserId { get; set; }
	}
}
