namespace EcoScan1.DTOs
{
    public class UpdateScanDto
    {
        public string? WasteType { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Confidence { get; set; }
        public string? Status { get; set; } // e.g., "Completed", "InProgress", "Failed"
    }
}
