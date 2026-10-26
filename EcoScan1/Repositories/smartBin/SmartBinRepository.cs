using EcoScan1.Data;
using EcoScan1.DTOs;
using EcoScan1.Model;
using Microsoft.EntityFrameworkCore;

namespace EcoScan1.Repositories.smartBin
{
    public class SmartBinRepository : ISmartBinRepository
    {
        private readonly EcoScanDbContext _context;
        
        public SmartBinRepository(EcoScanDbContext context)
        {
            _context = context;
        }


        // ===========================
        // Create SmartBin
        // ===========================
        public async Task<SmartBin> AddSmartBin(SmartBinDto smartBin)
        {
            // check admin exists
            if (!await _context.Admins.AnyAsync(a => a.AdminId == smartBin.AdminId))
                throw new Exception($"Admin with ID {smartBin.AdminId} not found.");
            //var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == smartBin.AdminId);
            //if (!adminExists)
            //    throw new Exception($"Admin with ID {smartBin.AdminId} not found.");

            var bin = new SmartBin
            {
                AdminId       = smartBin.AdminId,
                Location      = smartBin.Location,
                CapacityKg    = (decimal)smartBin.CapacityKg,
                CurrentFillKg = 0,
                HasCamera     = smartBin.HasCamera,
                Status        = "Active"
            };

            await _context.SmartBins.AddAsync(bin);
            await _context.SaveChangesAsync();
            return bin;
        }

        // ===========================
        // Get SmartBin By ID
        // ===========================
        public async Task<SmartBin?> FindBin(int id)
        {
            return await _context.SmartBins.Include(b => b.Admin).FirstOrDefaultAsync(x => x.SmartBinId == id);
        }

        // ===========================
        // Get All SmartBins
        // ===========================
        public async Task<IEnumerable<SmartBin>> GetAllBins()
        {
            return await _context.SmartBins.Include(b => b.Admin).ToListAsync();
                                 
        }

        // ===========================
        // Get SmartBins By Admin ID
        // ===========================
        public async Task<IEnumerable<SmartBin>> GetBinsByAdminId(int adminId)
        {
            return await _context.SmartBins.Where(x => x.AdminId == adminId).ToListAsync();
                                 
        }

        // ===========================
        // Update SmartBin
        // ===========================
        public async Task UpdateSmartBin(SmartBin bin)
        {
            var existingBin = await _context.SmartBins.FirstOrDefaultAsync(x => x.SmartBinId == bin.SmartBinId);
            if (existingBin == null)
                throw new Exception($"SmartBin with ID {bin.SmartBinId} not found.");

            // Update properties
            existingBin.Location      = bin.Location;
            existingBin.CapacityKg    = bin.CapacityKg;
            existingBin.CurrentFillKg = bin.CurrentFillKg;
            existingBin.HasCamera     = bin.HasCamera;
            existingBin.Status        = bin.Status;
            existingBin.AdminId       = bin.AdminId;

            // _context.SmartBins.Update(existingBin);
            await _context.SaveChangesAsync();
        }

        // ===========================
        // Delete SmartBin
        // ===========================
        public async Task DeleteSmartBin(int id)
        {
            var bin = await _context.SmartBins.FirstOrDefaultAsync(x => x.SmartBinId == id);

            if (bin == null)
                throw new Exception($"SmartBin with ID {id} not found.");

            _context.SmartBins.Remove(bin);
            await _context.SaveChangesAsync();
        }

        // ===========================
        // Exists
        // ===========================
        public async Task<bool> Exists(int id)
        {
            return await _context.SmartBins.AnyAsync(x => x.SmartBinId == id);
        }
    }
}
