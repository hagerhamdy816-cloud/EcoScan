using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Model
{
    public class PointsTransaction
    {
        public int PointsId { get; set; }
        public string RewardType { get; set; }
        public int PointsAmount { get; set; }
        public bool IsRedemption { get; set; }
    }
}
