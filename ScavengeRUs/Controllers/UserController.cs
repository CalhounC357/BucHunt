using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScavengeRUs.Models.Entities;
using ScavengeRUs.Services;
using System;
using System.Security.Claims;

namespace ScavengeRUs.Controllers
{
    [Authorize(Roles = "Admin, Player")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        string defaultPassword = "Etsupass12!";

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<IActionResult> Manage()
        {
            var users = await _userRepo.ReadAllAsync();
            return View(users);
        }
        public async Task<IActionResult> Edit([Bind(Prefix = "id")]string username)
        {
            var user = await _userRepo.ReadAsync(username);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                await _userRepo.UpdateAsync(user.Id, user);
                return RedirectToAction("Manage");
            }
            return View(user);
        }
        public async Task<IActionResult> Delete([Bind(Prefix ="id")]string username)
        {
            var user = await _userRepo.ReadAsync(username);
            if (user == null)
            {
                return RedirectToAction("Manage");
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([Bind(Prefix = "id")]string username)
        {
            await _userRepo.DeleteAsync(username);
            return RedirectToAction("Manage");
        }
        public async Task<IActionResult> Details([Bind(Prefix = "id")]string username)
        {
            var user = await _userRepo.ReadAsync(username);

            return View(user);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                user.UserName = user.Email;
                await _userRepo.CreateAsync(user, defaultPassword);
                return RedirectToAction("Details", new { id = user.UserName });
            }
            return View(user);
            
        }
        public IActionResult readRoles()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            string result = "";
            foreach (var item in roles)
            {
                result = result + item;
            }
            return Content(result);

        }
    }
}
