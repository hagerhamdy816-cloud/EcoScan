using EcoScan1.Data;
using EcoScan1.DTOs;
using EcoScan1.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Repositories.user
{
    public class UserRepository : IUserRepository
    {
        private readonly EcoScanDbContext _context;

        public UserRepository(EcoScanDbContext context)
        {
            _context = context;
        }
        // ===========================
        // Create User
        // ===========================
        public async Task <User> AddUser(User user)
        {
            if (await EmailExists(user.Email))
                throw new ArgumentException("A user with this email already exists.");
            //var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            //if (existingUser != null)
            //{
            //    throw new Exception("A user with this email already exists.");
            //}
            //var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            //var egyptTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone);

            //var users = new User
            //{
            //    Email = user.Email.ToLower(),
            //    UserName = user.Name,
            //    PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
            //    Phone = user.Phone,
            //    CreatedAt = egyptTime,
            //};

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // ===========================
        // Get User By ID
        // ===========================
        public async Task<User?> FindUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

        // ===========================
        // Get User By Email (Login)
        // ===========================
        public async Task<User?> FindUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        // ===========================
        // Get All Users
        // ===========================
        public async Task <IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // ===========================
        // Update User
        // ===========================
        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // ===========================
        // Delete User
        // ===========================
        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        // ===========================
        // Check Email Exists
        // ===========================
        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
