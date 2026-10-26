using EcoScan1.Model;

namespace EcoScan1.Repositories.scan
{
    public interface IScanRepository
    {
        Task<ScannedItems> AddScan(ScannedItems scan);
        Task<ScannedItems?> GetScanById(int id);
        Task<IEnumerable<ScannedItems>> GetAllScans();
        Task<IEnumerable<ScannedItems>> GetScansByUserId(int userId);
        Task UpdateScan(ScannedItems scan);
        Task DeleteScan(int id);
        Task<bool> Exists(int id);
    }
}
