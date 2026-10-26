using EcoScan1.DTOs;
using EcoScan1.Interfaces;
using EcoScan1.Model;
using EcoScan1.Repositories.scan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Services
{
    public class ScanService : IScanService
    {
        private readonly IScanRepository _scanrepository;
       
        public ScanService(IScanRepository scanrepository)
        {
            _scanrepository = scanrepository;
        }

        // =========================
        // CREATE
        // =========================

        public async Task<ScanResultDto> CreateScan(CreateScannedItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.WasteType))
                throw new Exception("Waste type is required");

            if (dto.Weight <= 0)
                throw new Exception("Weight must be greater than 0");
            var scan = new ScannedItems
            {
                UserId = dto.UserId,
                WasteType = dto.WasteType?.ToLower() ?? "unknown",
                Weight = dto.Weight,
                Confidence = dto.Confidence,
                ImagePath = dto.ImagePath,
                Status = "Pending",
                ScannedAt = DateTime.UtcNow,
            };
            var created = await _scanrepository.AddScan(scan);
            return MapToDto(created);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<IEnumerable<ScanResultDto>> GetAllScans()
        {
            var scans = await _scanrepository.GetAllScans();
            return scans.Select(MapToDto);
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<ScanResultDto> GetScanById(int id)
        {
            var scan = await _scanrepository.GetScanById(id);
            if (scan == null)
                throw new Exception("Scan not found");
            return MapToDto(scan);
        }

        // =========================
        // GET BY USER
        // =========================

        public async Task<IEnumerable<ScanResultDto>> GetScanByUserId(int userId)
        {
            var scans = await _scanrepository.GetScansByUserId(userId);
            return scans.Select(MapToDto);
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<ScanResultDto> UpdateScan(int id, UpdateScanDto dto)
        {
            var scan = await _scanrepository.GetScanById(id);
            if (scan == null)
                throw new Exception("Scan not found");
            if (dto.Weight.HasValue && dto.Weight <= 0)
                throw new Exception("Weight must be greater than 0");

            scan.WasteType = dto.WasteType != null ? dto.WasteType.ToLower() : scan.WasteType;

            if(dto.Weight.HasValue)
                scan.Weight = dto.Weight.Value;

            if(dto.Confidence.HasValue)
                scan.Confidence = dto.Confidence.Value;

            scan.Status = dto.Status ?? scan.Status;

            await _scanrepository.UpdateScan(scan);
            return MapToDto(scan);
        }
        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteScan(int id)
        {
            var exists = await _scanrepository.Exists(id);
            if(!exists)
                throw new Exception("Scan not found");

            await _scanrepository.DeleteScan(id);
            return true;
        }

        // =========================
        // MAPPING
        // =========================

        private ScanResultDto MapToDto(ScannedItems scan)
        {
            return new ScanResultDto
            {
                ScanId = scan.ScanId,
                WasteType = scan.WasteType,
                Weight = scan.Weight,
                Confidence = scan.Confidence,
                Status = scan.Status,
                ScannedAt = scan.ScannedAt
            };
        }

    }
}
