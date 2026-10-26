using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Model
{
    public class SmartBin
    {
        public int SmartBinId { get; set; }
        public int AdminId { get; set; }
        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }
        public string Location { get; set; }
        public decimal CapacityKg { get; set; }
        public decimal CurrentFillKg { get; set; } = 0;
        public string Status { get; set; } = "Active"; // Active, Full, Maintenance
        public bool HasCamera { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
