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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using project_airlines.Models.MiddleWare;
using System.Security.Cryptography.Xml;
using NuGet.Packaging;
using System.Text.RegularExpressions;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;

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

        public async Task<IActionResult> Calculate()
        {
            var firstPart = new List<CalculationVariables>();
            var secondPart = new List<CalculationVariables>();
            var dictionary = new Dictionary<string, List<CalculationVariables>>();


            var query1 = from md in _db.MarketDetails
                         join mw in _db.Market on md.ESCALESTAT equals mw.escale_D
                         select new CalculationVariables
                         {

                             Market = new Market
                             {
                                 escale_D = mw.escale_D,
                                 zonegeo = mw.zonegeo,
                                 zonegeoarabe = mw.zonegeoarabe,
                                 pays = mw.pays,
                                 paysArabe = mw.paysArabe,
                                 paysArRapport = mw.paysArRapport
                             },
                             MarketDetails = new MarketDetails
                             {
                                 AC_REGISTRATION = md.AC_REGISTRATION,
                                 ID = md.ID,
                                 PAX_FLOWN_C = md.PAX_FLOWN_C,
                                 PAX_FLOWN_Y = md.PAX_FLOWN_Y,
                                 PAX_BOOKED_C = md.PAX_BOOKED_C,
                                 PAX_BOOKED_Y = md.PAX_BOOKED_Y,
                                 DAY_OF_ORIGIN = md.DAY_OF_ORIGIN,
                                 FN_CARRIER = md.FN_CARRIER,
                                 FN_NUMBER = md.FN_NUMBER,
                                 ARR_AP_ACTUAL = md.ARR_AP_ACTUAL,
                                 ESCALESTAT = md.ESCALESTAT,
                                 AVG_FARE = md.AVG_FARE
                             },
                         };
            var totalBooked = _db.MarketDetails.Sum(x => x.PAX_BOOKED_C + x.PAX_BOOKED_Y);
            var totalFlown = _db.MarketDetails.Sum(x => x.PAX_FLOWN_C + x.PAX_FLOWN_Y);
            var totalFlownC = _db.MarketDetails.Sum(x => x.PAX_FLOWN_C);
            var query2 = from markets in _db.Market
                         join traffic in _db.MarketDetails
                         on markets.escale_D equals traffic.ESCALESTAT
                         group new { traffic, markets } by new { markets.pays } into grouped
                         select new CalculationVariables
                         {
                             Pays = grouped.Key.pays,
                             Traffic_Passenger = grouped.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y),
                             Offered_seats = grouped.Sum(x => x.traffic.PAX_BOOKED_C + x.traffic.PAX_BOOKED_Y),
                             Filling_Ratio = (totalBooked > 0) ? (grouped.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y) / (float)grouped.Sum(x => x.traffic.PAX_BOOKED_C + x.traffic.PAX_BOOKED_Y)) * 100 : 0
                         };
            var sortedResult = await query2.OrderByDescending(x => x.Traffic_Passenger).ToListAsync();
            var result = sortedResult.Take(12).ToList();


            _db.Database.SetCommandTimeout(timeout: 520); // Set the command timeout to a higher value in seconds
            var groupedData = (from markets in _db.Market
                               join traffic in _db.MarketDetails on markets.escale_D equals traffic.ESCALESTAT
                               group new { traffic, markets } by markets.zonegeo into grouped
                               select new
                               {
                                   ZoneGeo = grouped.Key,
                                   GroupedData = grouped.ToList()
                               })
                   .ToList();
            var query3 = groupedData.Select(groupedItem =>
            {
                var countries = groupedItem.GroupedData
                    .GroupBy(x => x.markets.pays)
                    .Select(g => new CountryTraffic
                    {
                        Country = g.Key,
                        TrafficPassenger = g.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y),
                        OfferedSeats = g.Sum(x => x.traffic.PAX_BOOKED_C + x.traffic.PAX_BOOKED_Y),
                        FillingRatio = (g.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y) / (float)g.Sum(x => x.traffic.PAX_BOOKED_C + x.traffic.PAX_BOOKED_Y)),
                        MarketPassengerShare = Math.Round(g.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y) * 100.0 / totalFlown, 2),
                        TotalFirstClassPassenger = (g.Sum(x => x.traffic.PAX_FLOWN_C)) > 0 ? (totalFlownC / (g.Sum(x => x.traffic.PAX_FLOWN_C))) : 0,
                        AverageTicketValue_TND = (int)g.Average(x => x.traffic.AVG_FARE),
                        TransactionNumber_Million_TND = Math.Round(g.Average(x => x.traffic.AVG_FARE) * g.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y), 2),
                        MarketTransactionShare = (Math.Round(groupedItem.GroupedData.Average(x => x.traffic.AVG_FARE) * groupedItem.GroupedData.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y), 2)) > 0 ? Math.Round(g.Average(x => x.traffic.AVG_FARE) * g.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y), 2) / Math.Round(groupedItem.GroupedData.Average(x => x.traffic.AVG_FARE) * groupedItem.GroupedData.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y), 2) : 0,
                    })
                    .ToList();

                var totalFirstClassPassengerSum = countries.Sum(country => country.TotalFirstClassPassenger);
                var totalTrafficPassenger = countries.Sum(country => country.TrafficPassenger);
                var totalOfferedSeats = countries.Sum(country => country.OfferedSeats);
                var totalFirstClassPassenger = countries.Sum(country => country.TotalFirstClassPassenger);
                var totalAverageTicketValue_TND = countries.Sum(country => country.AverageTicketValue_TND);
                var totalTransactionNumber_Million_TND = countries.Sum(country => country.TransactionNumber_Million_TND);
                var totalMarketTransactionShare = countries.Sum(country => country.MarketTransactionShare);
                return new CalculationVariables
                {
                    ZoneGeo = groupedItem.ZoneGeo,
                    Countries = countries,
                    Traffic_Passenger = totalTrafficPassenger,
                    Offered_seats = totalOfferedSeats,
                    Filling_Ratio = (totalBooked > 0) ? (groupedItem.GroupedData.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y) / (float)groupedItem.GroupedData.Sum(x => x.traffic.PAX_BOOKED_C + x.traffic.PAX_BOOKED_Y)) * 100 : 0,
                    MarketPassengerShare_T = Math.Round(groupedItem.GroupedData.Sum(x => x.traffic.PAX_FLOWN_C + x.traffic.PAX_FLOWN_Y) * 100.0 / totalFlown, 2),
                    TotalFirstClassPassenger_T = totalFirstClassPassengerSum,
                    AverageTicketValue_TND_T = totalAverageTicketValue_TND,
                    MarketTransactionShareT = totalMarketTransactionShare

                };
            }).ToList();


            firstPart.AddRange(result);
            secondPart.AddRange(query3);
            dictionary.Add("firstPart", firstPart);
            dictionary.Add("secondPart", secondPart);
            return View(dictionary);
        }
        public async Task<IActionResult> Index()
        {
            var result = await Calculate();
            var dictionary = (result as ViewResult)?.Model as Dictionary<string, List<CalculationVariables>>;
            return View(dictionary["firstPart"]);

        }

        [HttpGet]
        public async Task<IActionResult> _GroupedCountries()
        {
            var result = await Calculate();
            var dictionary = (result as ViewResult)?.Model as Dictionary<string, List<CalculationVariables>>;
            return PartialView("_GroupedCountries", dictionary["secondPart"]);
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