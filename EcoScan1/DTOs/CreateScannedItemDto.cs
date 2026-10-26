namespace EcoScan1.DTOs
{
    public class CreateScannedItemDto
    {
        public int UserId { get; set; }
        public string WasteType { get; set; } 
        public decimal Weight { get; set; }
        public decimal Confidence { get; set; }
        public string? ImagePath { get; set; }
    }
}
