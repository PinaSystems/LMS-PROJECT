using System.Threading.Tasks;
using OAWA.Data.Helpers;
using OAWA.Data.Models;
using System.Collections.Generic;
using OAWA.Data.Dtos;

namespace OAWA.Data
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetUser(int id);
        Task<long> GetUserIdByEmail(string email);
        Task<ICollection<Role>> GetRoles();
        Task<ICollection<UserRole>> GetRolesByUserId(int userId);
        Task<ICollection<UserRole>> GetRolesByUserIdAsync(int userId);
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<bool> DeleteUser(long userId);
        Task<List<string>> GetRefreshToken(string username);
        Task<bool> DeleteRefreshToken(TokenRefreshDto refreshToken);
        Task<bool> SaveRefreshToken(long userId,string newFrefreshToken);
    }
}