using EcoScan1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Interfaces
{
    public interface IJwtService
    {
        string GenerateUserToken(User user);
        string GenerateAdminToken(Admin admin);
    }
}
