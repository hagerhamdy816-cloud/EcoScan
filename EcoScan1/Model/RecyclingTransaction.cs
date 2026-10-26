using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Model
{
    public class RecyclingTransaction
    {
        public int TransactionId { get; set; }
        public string Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public double WeightKg { get; set; }
        public string WasteType { get; set; }

        //public User User { get; set; }
        //public ScannedItems ScannedItems { get; set; }
    }
}
