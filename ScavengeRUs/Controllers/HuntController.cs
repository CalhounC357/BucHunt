using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScavengeRUs.Models.Entities;
using ScavengeRUs.Services;

namespace ScavengeRUs.Controllers
{
    /// <summary>
    /// This class is the controller for any page realted to hunts
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class HuntController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IHuntRepository _huntRepo;

        /// <summary>
        /// Injecting the user repository and hunt repository (Db classes)
        /// </summary>
        /// <param name="userRepo"></param>
        /// <param name="HuntRepo"></param>
        public HuntController(IUserRepository userRepo, IHuntRepository HuntRepo)
        {
            _userRepo = userRepo;
            _huntRepo = HuntRepo;
        }
        /// <summary>
        /// www.localhost.com/hunt/index Returns a list of all hunts
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var hunts = await _huntRepo.ReadAllAsync();
            return View(hunts);
        }
        /// <summary>
        /// www.localhost.com/hunt/create This is the get method for creating a hunt
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// www.localhost.com/hunt/create This is the post method for creating a hunt
        /// </summary>
        /// <param name="hunt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Hunt hunt)
        {
            if (ModelState.IsValid)
            {
                await _huntRepo.CreateAsync(hunt);
                return RedirectToAction("Index");
            }
            return View(hunt);
           
        }
        /// <summary>
        /// www.localhost.com/hunt/details/{huntId} This is the details view of a hunt
        /// </summary>
        /// <param name="huntId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details([Bind(Prefix ="Id")]int huntId)
        {
            if (huntId == 0)
            {
                return RedirectToAction("Index");
            }
            var hunt = await _huntRepo.ReadAsync(huntId);
            if (hunt == null)
            {
                return RedirectToAction("Index");
            }
            return View(hunt);
        }
        /// <summary>
        /// www.localhost.com/hunt/delete/{huntId} This is the get method for deleting a hunt
        /// </summary>
        /// <param name="huntId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([Bind(Prefix = "Id")]int huntId)
        {
            if (huntId == 0)
            {
                return RedirectToAction("Index");
            }
            var hunt = await _huntRepo.ReadAsync(huntId);
            if (hunt == null)
            {
                return RedirectToAction("Index");
            }
            return View(hunt);
        }
        /// <summary>
        /// www.localhost.com/hunt/delete/{huntId} This is the post method for deleteing a hunt.
        /// </summary>
        /// <param name="huntId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([Bind(Prefix = "id")] int huntId)
        {
            await _huntRepo.DeleteAsync(huntId);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// www.localhost.com/hunt/viewplayers/{huntId} Returns a list of all players in a specified hunt
        /// </summary>
        /// <param name="huntId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ViewPlayers([Bind(Prefix = "Id")] int huntId)
        {
            var hunt = await _huntRepo.ReadHuntWithRelatedData(huntId);
            ViewData["Hunt"] = hunt;
            if(hunt == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(hunt.Players);
        }
        /// <summary>
        /// www.localhost.com/hunt/addplayertohunt{huntid} Get method for adding a player to a hunt. 
        /// </summary>
        /// <param name="huntId"></param>
        /// <returns></returns>
        public IActionResult AddPlayerToHunt([Bind(Prefix ="Id")]int huntId)
        {

            return View();
            
        }
        /// <summary>
        /// www.localhost.com/hunt/addplayertohunt{huntid} Post method for the form submission. This creates a user and assigns the access code for the hunt. 
        /// </summary>
        /// <param name="huntId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPlayerToHunt([Bind(Prefix = "Id")] int huntId ,ApplicationUser user)
        {

            if (huntId == 0)
            {
                RedirectToAction("Index");
            }
            var hunt = await _huntRepo.ReadAsync(huntId);
            var existingUser = await _userRepo.ReadAsync(user.Email);
            var newUser = new ApplicationUser();
            if (existingUser == null)
            {
                newUser.Email = user.Email;
                newUser.PhoneNumber = user.PhoneNumber;
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.AccessCode = user.AccessCode;
                newUser.UserName = user.Email;
            }
            else
            {
                newUser = existingUser;
                newUser.AccessCode = user.AccessCode;
            }
            if (newUser.AccessCode!.Code == null)       //If the admin didn't specify an access code (If we need to, I have the field readonly currently)
            {
                newUser.AccessCode = new AccessCode()
                {
                    Hunt = hunt,                        //Setting foriegn key
                    Code = $"{newUser.PhoneNumber}/{hunt.HuntName.Replace(" ", string.Empty)}",            //This is the access code generation
                };
                newUser.AccessCode.Users.Add(newUser);  //Setting foriegn key
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
            await _huntRepo.AddUserToHunt(huntId, newUser); //This methods adds the user to the database and adds the database relationship to a hunt.
            return RedirectToAction("Index");
        }
        /// <summary>
        /// www.localhost.com/hunt/removeuser/{username}/{huntId} This is the get method for removing a user from a hunt.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="huntid"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveUser([Bind(Prefix ="Id")]string username, [Bind(Prefix ="huntId")]int huntid)
        {
            ViewData["Hunt"] = huntid;
            var user = await _userRepo.ReadAsync(username);
            return View(user);

        }
        /// <summary>
        /// www.localhost.com/hunt/removeuser/{username}/{huntId} This is the post method for removing a user from a hunt.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="huntid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveUserConfirmed(string username, int huntid)
        {
            await _huntRepo.RemoveUserFromHunt(username, huntid);
            return RedirectToAction("Index");

        }
    }
}
