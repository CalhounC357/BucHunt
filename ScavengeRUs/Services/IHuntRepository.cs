using ScavengeRUs.Models.Entities;

namespace ScavengeRUs.Services
{
    public interface IHuntRepository
    {
        Task<ICollection<Hunt>> ReadAllAsync();
        Task<Hunt>? ReadAsync(int huntId);
        Task<ICollection<Hunt>> ReadHuntWithRelatedData(int huntId);
        Task AddUserToHunt(int huntId, ApplicationUser user);
        Task<Hunt> CreateAsync(Hunt hunt);
        Task DeleteAsync(int huntId);
        Task RemoveUserFromHunt(string username, int huntId);   
    }
}
