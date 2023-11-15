using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project_airlines.Data;
using project_airlines.Models.User;
using Microsoft.Extensions.Logging;

namespace project_airlines.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User userModel)
        {
            _logger.LogInformation("Attempting to register a new user.");

            if (userModel.Password != userModel.Repwd)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View();
            }
            var existingUser = await _userManager.FindByNameAsync(userModel.UserName);
            if(existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Email already exists, Please use a different email. ");
                return View();
            }
            var user = new User
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                Gender = userModel.Gender,
                Password = userModel.Password,
                Repwd = userModel.Repwd
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                _logger.LogError($"Error during user registration: {error.Description}");
                ModelState.AddModelError(string.Empty, error.Description);
            }
            _logger.LogInformation("Returning the view with errors.");
            return View(userModel);
        }
    }
}
