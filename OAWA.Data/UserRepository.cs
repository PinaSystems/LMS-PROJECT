

using OAWA.Data.Helpers;
using OAWA.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using OAWA.Data.Dtos;

namespace OAWA.Data
{
    public class UserRepository : DataRepository, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
            
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                                .Include(p => p.UserRoles)
                                .ThenInclude(p => p.Role)
                                .FirstOrDefaultAsync(item => item.Id.Equals(id));
            if(user==null) return null;
            return user;
        }

        public async Task<long> GetUserIdByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Email.Equals(email));
            if(user==null) return 0;
            return user.Id;
        }

        public async Task<ICollection<Role>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<ICollection<UserRole>> GetRolesByUserId(int userId)
        {
            var roles = await _context.UserRoles.Where(item => item.UserId.Equals(userId)).ToListAsync();
            return roles;
        }

        public async Task<ICollection<UserRole>> GetRolesByUserIdAsync(int userId)
        {
            var roles = await _context.UserRoles
                                        .Include(p => p.Role)
                                        .Where(item => item.UserId.Equals(userId)).ToListAsync();
            return roles;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users
            .OrderByDescending(u => u.LastActive).AsQueryable();
            users = users.Where(u => u.Id != userParams.UserId);
            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            await users.ForEachAsync(item => 
            { 
            });

            return await PagedList<User>.CreateAsync(users, userParams.Page, userParams.Per_Page);
        }

        public async Task<bool> DeleteUser(long userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(id => id.Id.Equals(userId));
            if(user==null) return false;
            user.IsDeleted=true;
            user.Email= user.NormalizedEmail= user.UserName= user.NormalizedUserName= Guid.NewGuid().ToString();
            _context.Users.Update(user);
            //_context.Users.Remove(user);
            var result= await _context.SaveChangesAsync()>0?true:false;
            return result;
        }

        public async Task<List<string>> GetRefreshToken(string username)
        {
            return await _context.UserSessions.Where(u => u.User.UserName == username)
            .Select(session => session.RefreshToken).ToListAsync();
        }

        public async Task<bool> DeleteRefreshToken(TokenRefreshDto refreshToken)
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(    
                u => u.RefreshToken == refreshToken.RefreshToken
            );
            if(session!=null)   _context.UserSessions.Remove(session);
            return await _context.SaveChangesAsync()>0?true:false;
        }

        public async Task<bool> SaveRefreshToken(long userId, string newRefreshToken)
        {
            await _context.UserSessions.AddAsync(new UserSession()
            {
                RefreshToken = newRefreshToken,
                UserId = userId
            });
            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> IsInEndUserRoleOnly(long userId)
        {
            var roles= await _context.UserRoles
                                        .Include(item => item.Role)
                                        .Where(item => item.UserId.Equals(userId))
                                        .Select(item => item.Role.NormalizedName)
                                        .ToListAsync();
            if(roles.Any(item => item.Equals("ENDUSER")) && roles.Count==1)    return true;
            return false;
        }
    }
}