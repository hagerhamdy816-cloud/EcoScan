using EcoScan1.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Interfaces
{
    public interface IScanService
    {
        Task<ScanResultDto> CreateScan(CreateScannedItemDto dto);
        Task<ScanResultDto?> GetScanById(int id);
        Task<IEnumerable<ScanResultDto>> GetAllScans();
        Task<IEnumerable<ScanResultDto>> GetScanByUserId(int userId);
        Task<ScanResultDto> UpdateScan(int id, UpdateScanDto dto);
        Task<bool> DeleteScan(int id);
    }
}
