using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Model
{
    public class ScannedItems
    {
        [Key]
        public int ScanId { get; set; }
        [Required]
        public string WasteType { get; set; }
        [Column(TypeName = "decimal(10,2)")] // وزن المخلفات
        public decimal Weight { get; set; }
        [Column(TypeName = "decimal(5,2)")] // نسبة دقة AI (مثلا 92.5)
        public decimal Confidence { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public string? ImagePath { get; set; } // maybe null
        public DateTime ScannedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
