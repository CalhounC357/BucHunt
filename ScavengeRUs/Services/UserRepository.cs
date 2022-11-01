using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ScavengeRUs.Data;
using ScavengeRUs.Models.Entities;
using System.Security.Claims;

namespace ScavengeRUs.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(ApplicationDbContext db, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser?> ReadAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }
            return user;
        }
        public async Task<ApplicationUser> CreateAsync(
            ApplicationUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
            await AssignUserToRoleAsync(user.UserName, user.Roles.First());
            user.Roles.Add(user.Roles.First());
            return user;
        }
        public async Task AssignUserToRoleAsync(string userName, string roleName)
        {
            //var roleCheck = await _roleManager.RoleExistsAsync(roleName);
            //if (!roleCheck)
            //{
            //    await _roleManager.CreateAsync(new IdentityRole(roleName));
            //}
            var user = await ReadAsync(userName);            
            var role = await _roleManager.FindByNameAsync(roleName);           
            if (user != null)
            {
                if (user.Roles.Count != 0)
                {
                    foreach (var item in user.Roles)
                    {
                        await RemoveUserFromRoleAsync(userName, item);
                    }
                    
                }
                user.Roles.Add(role.Name);
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                await _db.SaveChangesAsync();
            }
            
        }
        public async Task RemoveUserFromRoleAsync(string userName, string roleName)
        {
            var user = await ReadByUsernameAsync(userName);
            if (user != null)
            {
                if (user.Roles.Contains(roleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
        }
        public async Task<ApplicationUser?> ReadByUsernameAsync(string username)
        {
            var user = await _db.Users.FirstOrDefaultAsync(
                u => u.UserName == username);
            return user;
        }

        public async Task<ICollection<ApplicationUser>> ReadAllAsync()
        {
            var users = await _db.Users
                .ToListAsync();
            foreach (var user in users)
            {
                if (user != null)
                {
                    user.Roles = await _userManager.GetRolesAsync(user);
                }
            }
            return users;
        }

        public async Task UpdateAsync(string username, ApplicationUser user)
        {
            var userToUpdate = await ReadAsync(username);
            if(userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                // await RemoveUserFromRoleAsync(username, userToUpdate.Role);
                userToUpdate.Roles.Add(user.Roles.First());
                await AssignUserToRoleAsync(username, user.Roles.First());
                await _db.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(string username)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == username);
            if( user != null )
            {
                _db.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

    }
}
