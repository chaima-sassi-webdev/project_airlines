using Microsoft.EntityFrameworkCore;
using project_airlines.Models;
using project_airlines.Models.GlobalTraffic;
using project_airlines.Models.Market_Details;
using project_airlines.Models.Supplier;
using project_airlines.Models.Market;
using project_airlines.Models.TopCountries;



namespace project_airlines.Data
{
	//DbContext: is a base class provided by Entity Framework Core for managing database interactions
	//ApplicationDbContext : a class defined by DbContext
	public class ApplicationDbContext : DbContext
	{

		//constructor for the ApplicationDbContext class.It accepts an instance of DbContextOptions<ApplicationDbContext> as a parameter, which typically contains database connection information and configuration.
		//DbContextOptions<ApplicationDbContext> contains database connection information and configuration.
		//base(options) initializes the base "DbContext" class with these options.
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		//create a table named suppliers
		//DbSet is a class provided by Entity Framework Core that represents a table in the database.It represents a table named "Suppliers".
		//public DbSet<Supplier> suppliers property allows you to perform database operations ( such as quering, inserting, updating, and deleting records) on the "Suppliers" table using Entity Framework
		public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MarketDetails> MarketDetails { get; set; }
		public DbSet<GlobalTraffic> GlobalTraffic { get; set; }
		public DbSet<Market> Market { get; set; }
		public DbSet<TopCountries> TopCountries { get; set; }


	}
}
