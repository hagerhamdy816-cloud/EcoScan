using EcoScan1.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUser(RegisterDto dto);
        Task<AuthResponseDto> LoginUser(LoginDto dto);
        Task<AuthResponseDto> RegisterAdmin(RegisterDto dto);
        Task<AuthResponseDto> LoginAdmin(LoginDto dto);
    }
}
