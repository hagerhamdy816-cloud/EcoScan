using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.DTOs
{
    public class ScanResultDto
    {
        public int ScanId { get; set; }
        public string WasteType { get; set; }
        public decimal Weight { get; set; }
        public decimal Confidence { get; set; }
        public string Status { get; set; }
        public DateTime ScannedAt { get; set; }

        public int UserId { get; set; }

    }
}
