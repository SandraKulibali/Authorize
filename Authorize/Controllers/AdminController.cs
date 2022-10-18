using Authorize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authorize.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IPasswordHasher<AppUser> _passwordHasher;
        public AdminController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;   
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if(ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach(IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }             
            }
            return View(user);
        }

         public async Task<IActionResult> Update(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id,string email, string password)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if(user!=null)
            {
                if (!string.IsNullOrWhiteSpace(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email is required");
                if (!string.IsNullOrWhiteSpace(password))
                    user.PasswordHash = _passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password is required");
                if(!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                    {
                        foreach (IdentityError item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
              
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                }
            }
            else
                ModelState.AddModelError("", "Not Found");
            return View("Index", _userManager.Users);
        }
        public ActionResult Create()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
    }
}
