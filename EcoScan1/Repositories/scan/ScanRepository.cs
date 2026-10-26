using EcoScan1.Data;
using EcoScan1.DTOs;
using EcoScan1.Model;
using EcoScan1.Repositories.scan;
using Microsoft.EntityFrameworkCore;

namespace EcoScan1.Repositories.scan
{
    public class ScanRepository : IScanRepository
    {
        private readonly EcoScanDbContext _context;
        public ScanRepository(EcoScanDbContext context)
        {
            _context = context;
        }

        // =========================
        // CREATE
        // =========================
        public async Task<ScannedItems> AddScan(ScannedItems scan)
        {
            await _context.ScannedItems.AddAsync(scan);
            await _context.SaveChangesAsync();
            return scan;
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<ScannedItems?> GetScanById(int id)
        {
            return await _context.ScannedItems.Include(s => s.User).FirstOrDefaultAsync(s => s.ScanId == id);
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<IEnumerable<ScannedItems>> GetAllScans()
        {
            return await _context.ScannedItems.Include(s => s.User).OrderByDescending(s => s.ScannedAt).ToListAsync();
        }

        // =========================
        // GET BY USER
        // =========================
        public async Task<IEnumerable<ScannedItems>> GetScansByUserId(int userId)
        {
            return await _context.ScannedItems.Where(s => s.UserId == userId).Include(s => s.User).OrderByDescending(s => s.ScannedAt).ToListAsync();
        }

        // =========================
        // UPDATE
        // =========================
        public async Task UpdateScan(ScannedItems scan)
        {
            var existingScan = await _context.ScannedItems
                .FirstOrDefaultAsync(s => s.ScanId == scan.ScanId);

            if (existingScan == null)
                throw new Exception($"Scan with ID {scan.ScanId} not found.");

            existingScan.WasteType = scan.WasteType;
            existingScan.Weight = scan.Weight;
            existingScan.Confidence = scan.Confidence;
            existingScan.Status = scan.Status;
            existingScan.ImagePath = scan.ImagePath;

            await _context.SaveChangesAsync();
        }

        // =========================
        // DELETE
        // =========================
        public async Task DeleteScan(int id)
        {
            var scan = await _context.ScannedItems
                .FirstOrDefaultAsync(s => s.ScanId == id);

            if (scan == null)
                throw new Exception($"Scan with ID {id} not found.");

            _context.ScannedItems.Remove(scan);
            await _context.SaveChangesAsync();
        }

        // =========================
        // EXISTS
        // =========================
        public async Task<bool> Exists(int id)
        {
            return await _context.ScannedItems
                .AnyAsync(s => s.ScanId == id);
        }
    }
}
