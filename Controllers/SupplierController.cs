using project_airlines.Data;
using project_airlines.Models.Supplier;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace project_airlines.Controllers
{
	public class SupplierController : Controller
	{

		//this line declares a private field "_db" of type "ApplicationDbContext", which represents a database context.This field is used to interact with the database throughout the controller's actions.
		private readonly ApplicationDbContext _db;

		//constructor
		public SupplierController(ApplicationDbContext db)
		{
			_db = db;

		}

	

		public async Task<IActionResult> Index(int? page, string? query)
		{


			int pageSize = 3; // Define the number of items per page 
			int pageNumber = (page ?? 1); // If no page is specified, default to page 1

			IQueryable<Supplier> supplierSearch = _db.Suppliers; //start fetching all the suppliers

			if (!string.IsNullOrEmpty(query))
			{
				long phoneQuery;
				bool isPhoneQuery = long.TryParse(query, out phoneQuery);
				query = query.ToLower(); // Convert the query to lowercase for case-insensitive search

				// Perform a search based on the query
				supplierSearch = supplierSearch.Where(s =>
					s.Name.ToLower().Contains(query) ||
					s.email.ToLower().Contains(query) ||
					(isPhoneQuery && s.mobile_phone == phoneQuery)
				);

				// Optional: Handle the case where the query is not a valid phone number
				if (!isPhoneQuery)
				{
					
					ViewBag.ErrorMessage = "Invalid phone number format.";
				}
			}
			
			// Retrieve a paginated list of suppliers from the database
			IPagedList<Supplier> suppliers = await supplierSearch.ToPagedListAsync(pageNumber, pageSize);
			ViewData["SearchQuery"] = query;
			return View(suppliers);
		}

		//This is an action method that handles HTTP GET requests to the "Create" endpoint. It returns a view that is typically used to create a new supplier
		public IActionResult _Create()
		{
			return View();
		}


		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> _Create(Supplier obj, IFormFile? SupplierImage)
		{
			// Check if a supplier with the same properties already exists in the database
			var existingSupplier = _db.Suppliers.FirstOrDefault(s =>
				s.email == obj.email ||
				s.mobile_phone == obj.mobile_phone);


			if (existingSupplier != null)
			{
				ViewData["ErrorMessage"] = "A supplier with the same properties already exists in the database.";

				return View(obj);
			}
			if (existingSupplier == null)
            {
                ViewData["SuccessMessage"] = "Created Successfully";

                return View(obj);
            }



			if (SupplierImage != null && SupplierImage.Length > 0)
			{
				using (var memoryStream = new MemoryStream())
				{
					await SupplierImage.CopyToAsync(memoryStream);
					obj.SupplierImage = memoryStream.ToArray();
				}
			}

			// Explicitly check for other validation rules
			if (string.IsNullOrEmpty(obj.Name) || string.IsNullOrEmpty(obj.Address) || string.IsNullOrEmpty(obj.email) || obj.mobile_phone == 0)
			{
				ViewData["ErrorMessage"] = "Please provide all required fields.";
				return View(obj);
			}


			_db.Suppliers.Add(obj);
			_db.SaveChanges();
			TempData["success"] = "Supplier created successfully";
			return RedirectToAction("Index");


		}

		public IActionResult _Edit(int? id)
		{
			// Retrieve the supplier by ID from the database based on the provided id ( given in the parameter). This is done using Entity Framework Core's " FirstOrDefault" method.
			Supplier supplierFromDb = _db.Suppliers.FirstOrDefault(s => s.Id == id);

			//if the supplierFromDb is null, it means that no supplier exists in the database that corresponds to the id provided, so then it will responds by NotFound
			if (supplierFromDb == null)
			{
				return NotFound(); // Return a 404 error if the supplier is not found
			}

			
			//the action returns a partial view named "_Edit" with the "supplierFromDb" object as the model.
			return PartialView("_Edit", supplierFromDb);
		}

		//the method receives a "Supplier" object as a parameter, validates it, and updates the supplier's information in the database.
		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult _Edit(int id, Supplier obj)
		{
			if (id != obj.Id)
			{
				return BadRequest(); // Return a bad request if the IDs don't match
			}

			// Find the existing supplier in the database
			var existingSupplier = _db.Suppliers.Find(id);

			if (existingSupplier == null)
			{
				return NotFound(); // Return a 404 error if the supplier is not found
			}

			// Update the properties of the existing supplier with the values from obj
			existingSupplier.Name = obj.Name;
			existingSupplier.Address = obj.Address;
			existingSupplier.mobile_phone = obj.mobile_phone;
			existingSupplier.email = obj.email;

			try
			{
				_db.Suppliers.Update(existingSupplier);
				_db.SaveChanges();
				TempData["success"] = "Supplier updated successfully";
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				// Handle exceptions or errors during the update process
				// You may want to log the error or provide a user-friendly error message
				return View(obj); // Return to the edit view with the original obj data
			}
		}
		//this action method handles HTTP GET requests to the "Delete" endpoint. It looks up a supplier by its "id" in the database and returns a view for confirming the deletion
		//the method receives a "Supplier" object as a parameter , validates it, and updates the supplier's information in the database
		public IActionResult _Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var supplierFromDb = _db.Suppliers.Find(id);

			//retrieve the first element from a sequence where the "ID" property corresponds to the "Id" variable.
			//var supplierFromDbFirst = _db.Suppliers.FirstOrDefault(u => u.Id == id);

			if (supplierFromDb == null)
			{
				return NotFound();
			}

			return View(supplierFromDb);
		}


		//this attribute specifies that this action method should only respond to HTTP POST requests and renames it to "DeletePost" to avoid method name conflicts.
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		//this action method handles HTTP GET requests to the "Delete" endpoint. It looks up a supplier by its "id" in the database and returns a view for confirming the deletion
		public IActionResult _DeletePost(int? id)
		{


			var obj = _db.Suppliers.Find(id);

			if (obj == null)
			{
				return NotFound();
			}


			_db.Suppliers.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Supplier deleted successfully";
			return RedirectToAction("Index");


		}


	}
}
