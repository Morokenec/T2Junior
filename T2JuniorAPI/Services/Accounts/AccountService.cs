using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Users;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Accounts
{
    /// <summary>
    /// Сервис для работы с аккаунтами пользователей, включая регистрацию, вход в систему, обновление и удаление профилей.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор для инициализации сервисов UserManager и SignInManager.
        /// </summary>
        /// <param name="userManager">Менеджер пользователей для работы с пользователями.</param>
        /// <param name="signInManager">Менеджер входа для работы с аутентификацией.</param>
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="registerUserDto">Объект с данными нового пользователя.</param>
        /// <returns>Строка с результатом регистрации пользователя.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если регистрация не удалась.</exception>
        public async Task<string> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerUserDto);

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Registration failed: {string.Join("; ", result.Errors.Select(e => e.Description))}");
            }

            return "User registered successfully";
        }

        /// <summary>
        /// Получает профиль пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Объект профиля пользователя.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если пользователь не найден.</exception>
        public async Task<UserProfileDTO> GetUserProfileAsync(Guid userId)
        {
            var user = await _userManager.Users
                .Include(u => u.Organization)
                .Include(u => u.Subscribers)
                .Include(u => u.ClubUsers)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }

            var userProfile = _mapper.Map<UserProfileDTO>(user);
            userProfile.RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "No Role";

            return userProfile;
        }

        /// <summary>
        /// Обновляет профиль пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="updateUserDto">Объект с новыми данными для обновления.</param>
        /// <returns>Строка с результатом обновления.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если пользователь не найден или обновление не удалось.</exception>
        public async Task<string> UpdateUserProfileAsync(Guid userId, UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new ApplicationException("User not found");

            _mapper.Map(updateUserDto, user);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Update failed: {string.Join("; ", result.Errors.Select(e => e.Description))}");
            }

            return "User profile updated successfully";
        }

        public async Task<string> UserPasswordRecovery(RecoveryPasswordDTO recoveryPassword)
        {
            var user = await _userManager.FindByEmailAsync(recoveryPassword.Email);
            if (user == null)
                return "User not found";

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, recoveryPassword.Password);
            if (!result.Succeeded)
                return "Password recovery failed";

            return "user password successfully recovered";
        }

        /// <summary>
        /// Удаляет пользователя из системы.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, которого нужно удалить.</param>
        /// <returns>Строка с результатом удаления пользователя.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если пользователь не найден или удаление не удалось.</exception>
        public async Task<string> DeleteUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new ApplicationException("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Delete failed: {string.Join("; ", result.Errors.Select(e => e.Description))}");
            }

            return "User deleted successfully";
        }
    }
}
