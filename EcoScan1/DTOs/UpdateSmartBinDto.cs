namespace EcoScan1.DTOs
{
    public class UpdateSmartBinDto
    {
        public string? Location { get; set; }
        public float? CapacityKg { get; set; }
        public float? CurrentFillLevelKg { get; set; }
        public bool? HasCamera { get; set; }
        public string? Status { get; set; }
    }
}
