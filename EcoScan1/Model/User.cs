using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public int TotalPoints { get; set; } = 0;
        public DateTime CreatedAt { get; set; }

        // Navigation property for related activities
        // public ICollection<ScannedItems> ScannedItems { get; set; }
        //public ICollection<RecyclingTransaction> Transactions { get; set; }
    }
}
