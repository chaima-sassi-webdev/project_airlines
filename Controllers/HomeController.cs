using Microsoft.AspNetCore.Mvc;
using project_airlines.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using project_airlines.Models.Market;
using project_airlines.Models.Market_Details;
using project_airlines.Models.TopCountries;
using project_airlines.Data;
using Humanizer;
using Microsoft.AspNetCore.Http;
using MyTested.AspNetCore.Mvc;

namespace project_airlines.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _db;


		public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
		{
			_db = db;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{


			var zones = _db.TopCountries;
			var query = from markets in _db.Market
						join traffic in _db.MarketDetails
						on markets.escale_D equals traffic.ESCALESTAT

						group new { traffic, markets } by new { markets.pays } into grouped
						
						select new
						{

							Pays = grouped.Key.pays,
							Filling_Ratio = grouped.Sum(x => x.traffic.PAX_BOOKED_C + x.traffic.PAX_BOOKED_Y) ,
							Traffic_Passenger = grouped.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y)
						};
			var totalBooked = _db.MarketDetails.Sum(x => x.PAX_BOOKED_C + x.PAX_BOOKED_Y);
			
			
			// Sort the result by Traffic_Passenger in descending order
			var sortedResult = await query.OrderByDescending(x => x.Traffic_Passenger).ToListAsync();
			// Get the top eleven records
			var result = sortedResult.Take(12).ToList();
			var topCountriesList = new List<TopCountries>(); // Create a list to store the TopCountries objects

			foreach (var item in result)
			{
				if (item != null)
				{
					var topCountry = new TopCountries
					{
						Filling_Ratio = (totalBooked > 0) ? (item.Traffic_Passenger / (float)totalBooked) * 100 : 0,
						Traffic_Passenger = item.Traffic_Passenger,
						Zonegeo = item.Pays,
					};
					topCountriesList.Add(topCountry); // Add the topCountry object to the list
				}
			}
			// Remove all existing records from the TopCountries table
			_db.Database.ExecuteSqlRaw("DELETE FROM TopCountries");

			// Save the list of topCountries to the database in one batch
			_db.TopCountries.AddRange(topCountriesList);
			await _db.SaveChangesAsync(); // Save changes to the database

			return View(topCountriesList); // Return the list to the view

		}










		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}