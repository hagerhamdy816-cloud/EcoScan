using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.DTOs
{
    public class TransactionDto
    {
        public int BinId { get; set; }
        public double WeightKg { get; set; }
        public string Image { get; set; }
    }
}
