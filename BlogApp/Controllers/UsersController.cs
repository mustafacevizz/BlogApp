using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Posts");   // User is already authenticated redirect to the Posts index
            }
            return View();
        }

        public IActionResult Register() 
        { 
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userRepository.Users.FirstOrDefaultAsync(x=>x.UserName==model.UserName||x.Email==model.Email);
                if (user == null)
                {
                    _userRepository.CreateUser(new User
                    {
                        UserName=model.UserName,
                        Name=model.Name,
                        Email=model.Email,
                        Password=model.Password,
                        Image="avatar.jpg"
                    });
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("","That Username or Password already exists");
                }
            }
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Try to find the user with the provided email and password
                var isUser =_userRepository.Users.FirstOrDefault(x=>x.Email == model.Email && x.Password==model.Password);

                if (isUser != null)
                {
                    // Create a list of user claims
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));

                    if (isUser.Email == "info@mcvz.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    // Create a claims identity with the user claims and the cookie authentication scheme
                    var claimsIdentity =new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    //User stays logged in even after closing the browser
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };
                    
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Sign out any existing user

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);    // Sign in the new user
                    return RedirectToAction("Index", "Posts");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                }


            }
            
            return View(model);
        }
        public IActionResult Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var user =_userRepository
                .Users
                .Include(x=>x.Posts)
                .Include(x=>x.Comments)
                .ThenInclude(x=>x.Post)
                .FirstOrDefault(x=>x.UserName == username);
            if (user == null)
            {
                return NotFound();
            }
            return View(user); 
        }
    }
}
