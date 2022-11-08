using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavengeRUs.Data;
using ScavengeRUs.Models.Entities;

namespace ScavengeRUs.Services
{
    public class HuntRepository : IHuntRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserRepository _userRepo;

        public HuntRepository(ApplicationDbContext db, IUserRepository userRepo)
        {
            _db = db;
            _userRepo = userRepo;
        }

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
        public async Task<Hunt> CreateAsync(Hunt hunt)
        {
            await _db.Hunts.AddAsync(hunt);
            await _db.SaveChangesAsync();
            return hunt;
        }
        public async Task DeleteAsync(int huntId)
        {
            Hunt? hunt = await ReadAsync(huntId);
            if (hunt != null)            
            {
                _db.Hunts.Remove(hunt);
                await _db.SaveChangesAsync();
            }
        }
        public async Task RemoveUserFromHunt(string username, int huntId)
        {
            var user = await _userRepo.ReadAsync(username);
            var hunt = await ReadAsync(huntId);
            if (user != null && hunt != null)
            {
                hunt.Players.Remove(user);
                await _db.SaveChangesAsync();
            }
        }
        public async Task<ICollection<Hunt>> ReadAllAsync()
        {
            var hunts = await _db.Hunts
                .Include(h => h.Players)
                .ToListAsync();
            return hunts;
        }
        public async Task<ICollection<Hunt>> ReadWithRelatedData(int huntId)
        {
            return await _db.Hunts
                .Include(p => p.Players)
                .ThenInclude(p => p.AccessCode)
                .ToListAsync();
        }
    }
}
