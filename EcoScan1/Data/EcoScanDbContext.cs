using EcoScan1.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Data
{
    public class EcoScanDbContext : DbContext
    {
        public EcoScanDbContext(DbContextOptions<EcoScanDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SmartBin> SmartBins { get; set; }
        public DbSet<ScannedItems> ScannedItems { get; set; }

    }
}
