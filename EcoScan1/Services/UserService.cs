using EcoScan1.DTOs;
using EcoScan1.Interfaces;
using EcoScan1.Repositories.user;

namespace EcoScan1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<object>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            return users.Select(u => new
            {
                u.UserId,
                u.UserName,
                u.Email,
                u.Phone,
                u.TotalPoints
            });
        }

        public async Task<object> GetUserById(int id)
        {
            var user = await _userRepository.FindUser(id);

            if (user == null)
                throw new Exception("User not found");

            return new
            {
                user.UserId,
                user.UserName,
                user.Email,
                user.Phone,
                user.TotalPoints,
                user.CreatedAt
            };
        }

        public async Task UpdateUser(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.FindUser(id);

            if (user == null)
                throw new Exception("User not found");

            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
            {
                if (await _userRepository.EmailExists(dto.Email))
                    throw new Exception("Email already exists");

                user.Email = dto.Email.ToLower();
            }

            user.UserName = dto.UserName ?? user.UserName;
            user.Phone = dto.Phone ?? user.Phone;

            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }
    }
}
