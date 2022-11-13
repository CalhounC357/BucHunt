using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavengeRUs.Data;
using ScavengeRUs.Models.Entities;

namespace ScavengeRUs.Services
{
    /// <summary>
    /// This class is the middleware controlling all db queries for hunts. Such as Adding users, Creating hunts, Reading, Deleting, etc
    /// </summary>
    public class HuntRepository : IHuntRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserRepository _userRepo;

        public HuntRepository(ApplicationDbContext db, IUserRepository userRepo)
        {
            _db = db;           //Database injection
            _userRepo = userRepo;   //User repo injection
        }
        /// <summary>
        /// This method adds a hunt to the db passing a Hunt object
        /// </summary>
        /// <param name="hunt"></param>
        /// <returns></returns>
        public async Task<Hunt> CreateAsync(Hunt hunt)
        {
            await _db.Hunts.AddAsync(hunt);
            await _db.SaveChangesAsync();
            return hunt;
        }
        /// <summary>
        /// This method reads all hunts from the db and returns a list of hunts
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Hunt>> ReadAllAsync()
        {
            var hunts = await _db.Hunts
                .Include(h => h.Players)
                .ToListAsync();
            return hunts;
        }
        /// <summary>
        /// This methods returns a hunt passing a huntId
        /// </summary>
        /// <param name="huntId"></param>
        /// <returns></returns>
        public async Task<Hunt> ReadAsync(int huntId)
        {
            var hunt = await _db.Hunts.FindAsync(huntId);

            if (hunt != null)
            {
                _db.Entry(hunt)
                    .Collection(p => p.Players)
                    .Load();
                return hunt;
            }
            return new Hunt();


        }
        /// <summary>
        /// This method delete a hunt from the db passing the huntId
        /// </summary>
        /// <param name="huntId"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int huntId)
        {
            Hunt? hunt = await ReadAsync(huntId);
            if (hunt != null)            
            {
                _db.Hunts.Remove(hunt);
                var list = _db.AccessCodes.Where(a => a.HuntId == huntId);
                foreach (var item in list)
                {
                _db.AccessCodes.Remove(item);

                }
                await _db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// This method is similar to the ReadAsync, but it includes the players and access codes associated with a hunt. This is nessesary becasue of the way the database is set up with the foreign keys
        /// </summary>
        /// <param name="huntId"></param>
        /// <returns></returns>
        public async Task<Hunt> ReadHuntWithRelatedData(int huntId)
        {
            
                var hunts = await _db.Hunts
                
                .Include(p => p.Players)
                .ThenInclude(p => p.AccessCode)
                .ToListAsync();
                return hunts.FirstOrDefault(a => a.Id == huntId);
        }
        /// <summary>
        /// This methods adds a user to a hunt passing the huntId and a user
        /// </summary>
        /// <param name="huntId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddUserToHunt(int huntId, ApplicationUser user)
        {
            var hunt = await ReadAsync(huntId);
            if (hunt != null)
            {
                user.Hunt = hunt;
                user.Roles.Add("Player");
                await _userRepo.CreateAsync(user, "Etsupass12!");
                var player = await _userRepo.ReadAsync(user.Id);
                if (player != null)
                {
                    hunt.Players.Add(player);
                    await _db.SaveChangesAsync();
                    await _userRepo.AddUserToHunt(user.Id, hunt);

                }
            }
        }
        /// <summary>
        /// This method removes the relationship between a hunt, but doesnt delete the user passing a username and huntId.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="huntId"></param>
        /// <returns></returns>
        public async Task RemoveUserFromHunt(string username, int huntId)
        {
            var user = await _userRepo.ReadAsync(username);
            var hunt = await ReadAsync(huntId);
            if (user != null && hunt != null)
            {
                user.Hunt = null;
                
                hunt.Players.Remove(user);
                await _db.SaveChangesAsync();
            }
        }
    }
}
