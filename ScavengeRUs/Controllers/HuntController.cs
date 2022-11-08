using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScavengeRUs.Models.Entities;
using ScavengeRUs.Services;

namespace ScavengeRUs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HuntController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IHuntRepository _huntRepo;


        public HuntController(IUserRepository userRepo, IHuntRepository HuntRepo)
        {
            _userRepo = userRepo;
            _huntRepo = HuntRepo;
        }

        public async Task<IActionResult> Index()
        {
            var Hunts = await _huntRepo.ReadAllAsync();
            if(Hunts == null)
            {
                return NotFound();
            }
            return View(Hunts);
        }
        public async Task<IActionResult> ViewPlayers([Bind(Prefix = "Id")] int huntId)
        {
            var hunt = await _huntRepo.ReadWithRelatedData(huntId);
            ViewData["Hunt"] = hunt.First();
            if(hunt == null)
            {
                return NotFound();
            }
            Hunt hunt1 = hunt.First();
            return View(hunt1.Players);
        }
        public IActionResult AddPlayerToHunt([Bind(Prefix ="Id")]int huntId)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayerToHunt([Bind(Prefix = "Id")] int huntId ,ApplicationUser user)
        {
            var hunt = await _huntRepo.ReadAsync(huntId);

            
            var newUser = new ApplicationUser()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessCode = user.AccessCode,
                UserName = user.Email
            };
            if (newUser.AccessCode.Code == null)
            {
                newUser.AccessCode = new AccessCode()
                {
                    Hunt = hunt,
                    Code = $"{newUser.PhoneNumber}/{hunt.HuntName}",
                };
                newUser.AccessCode.Users.Add(newUser);
            }
            else
            {
                newUser.AccessCode = new AccessCode()
                {
                    Hunt = hunt,
                    Code = newUser.AccessCode.Code,
                };
                newUser.AccessCode.Users.Add(newUser);
            }

            await _huntRepo.AddUserToHunt(huntId, newUser);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Hunt hunt)
        {
            if (ModelState.IsValid)
            {
                await _huntRepo.CreateAsync(hunt);
                return RedirectToAction("ViewPlayers", new { id = hunt.Id });
            }
            return View(hunt);
           
        }
        public async Task<IActionResult> Details([Bind(Prefix ="Id")]int huntId)
        {
            var hunt = await _huntRepo.ReadAsync(huntId);
            if (hunt == null)
            {
                return RedirectToAction("Index");
            }
            return View(hunt);
        }
        public async Task<IActionResult> Delete([Bind(Prefix = "Id")]int huntId)
        {
            var hunt = await _huntRepo.ReadAsync(huntId);
            if (hunt == null)
            {
                return RedirectToAction("Index");
            }
            return View(hunt);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([Bind(Prefix = "id")] int huntId)
        {
            await _huntRepo.DeleteAsync(huntId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveUser([Bind(Prefix ="Id")]string username, [Bind(Prefix ="huntId")]int huntid)
        {
            ViewData["Hunt"] = huntid;
            var user = await _userRepo.ReadAsync(username);
            return View(user);

        }
        [HttpPost]
        public async Task<IActionResult> RemoveUserConfirmed(string username, int huntid)
        {
            await _huntRepo.RemoveUserFromHunt(username, huntid);
            return RedirectToAction("Index");

        }
        //[HttpPost]
        //public async Task<IActionResult> AddPlayerToHunt(string huntId, ApplicationUser user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.UserName = user.Email;
        //        await _userRepo.CreateAsync(user, defaultPassword);
        //        return RedirectToAction("Details", new { id = user.UserName });
        //    }
        //    return View(user);

        //}
    }
}
