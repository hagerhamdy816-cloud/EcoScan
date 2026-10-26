using EcoScan1.Data;
using EcoScan1.DTOs;
using EcoScan1.Model;
using Microsoft.EntityFrameworkCore;

namespace EcoScan1.Repositories.admin
{
    public class AdminRepository : IAdminRepository
    {
        private readonly EcoScanDbContext _context;

        public AdminRepository(EcoScanDbContext context)
        {
            _context = context;
        }
        // ===========================
        // Create Admin
        // ===========================
        public async Task<Admin> AddAdmin(Admin admin)
        {
            if (await EmailExists(admin.Email.ToLower()))
                throw new Exception("An Admin with this email already exists.");
            
            //var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            //var egyptTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone);

            //var admins = new Admin
            //{
            //    Email = admin.Email.ToLower(),
            //    AdminName = admin.Name,
            //    PasswordHash = BCrypt.Net.BCrypt.HashPassword(admin.Password),
            //    Phone = admin.Phone,
            //    // CreatedAt = egyptTime,
            //};

            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        // ===========================
        // Get Admin By ID
        // ===========================
        public async Task<Admin?> FindAdmin(int id)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.AdminId == id);
        }

        // ===========================
        // Get Admin By Email (Login)
        // ===========================
        public async Task<Admin?> FindAdmin(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        // ===========================
        // Get All Admins
        // ===========================
        public async Task<IEnumerable<Admin>> GetAllAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // ===========================
        // Update Admin
        // ===========================
        public async Task UpdateAdmin(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }

        // ===========================
        // Delete Admin
        // ===========================
        public async Task DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.AdminId == id);
            if (admin == null)
                throw new Exception($"Admin with ID {id} not found");
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
        }

        // ===========================
        // Check Email Exists
        // ===========================
        public async Task<bool> EmailExists(string email)
        {
            return await _context.Admins.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}

