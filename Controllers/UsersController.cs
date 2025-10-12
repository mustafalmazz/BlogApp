using BlogApp.Data.Abstract;
using BlogApp.Models;
using BlogApp.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName || u.Email == model.Email);
                if (user == null)
                {
                    _userRepository.CreateUser(
                    new User
                    {
                        UserName = model.UserName,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Image = "smile.png"
                    }    
                    );
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya Eposta Kayıtlı!");
                }

               
            }
            return View(model);
        }
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                RedirectToAction("Index", "Posts");
            }
            return View();
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
                var IsUser = _userRepository.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (IsUser != null)
                {
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, IsUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, IsUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, IsUser.Name ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.UserData, IsUser.Image ?? ""));

                    if (IsUser.Email == "admin@gmail.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true

                    };
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Posts");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı!");
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
            var user = _userRepository.Users.Include(u => u.Posts).Include(u => u.Comments).ThenInclude(u=>u.Post).FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
