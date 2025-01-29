using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Models;

namespace T2JuniorAPI.Services
{
    /// <summary>
    /// Сервис для работы с аккаунтами пользователей, включая регистрацию, вход в систему, обновление и удаление профилей.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Конструктор для инициализации сервисов UserManager и SignInManager.
        /// </summary>
        /// <param name="userManager">Менеджер пользователей для работы с пользователями.</param>
        /// <param name="signInManager">Менеджер входа для работы с аутентификацией.</param>
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="registerUserDto">Объект с данными нового пользователя.</param>
        /// <returns>Строка с результатом регистрации пользователя.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если регистрация не удалась.</exception>
        public async Task<string> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerUserDto.Email,
                Email = registerUserDto.Email,
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                MiddleName = registerUserDto.MiddleName,
                PhoneNumber = registerUserDto.PhoneNumber,
                DateOfBirth = registerUserDto.DateOfBirth,
                Gender = registerUserDto.Gender,
                OrganizationId = registerUserDto.OrganizationId
            };

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
        public async Task<UserProfileDTO> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            return new UserProfileDTO
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "No Role",
                SubscibersCount = user.Subscribers?.Count ?? 0,
                SubscriptionsCount = user.Subscribers?.Count ?? 0,
                ClubsCount = user.ClubUsers?.Count ?? 0,
                PostAndOrganization = user.Organization?.Name ?? "N/A"
            };
        }

        /// <summary>
        /// Обновляет профиль пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="updateUserDto">Объект с новыми данными для обновления.</param>
        /// <returns>Строка с результатом обновления.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если пользователь не найден или обновление не удалось.</exception>
        public async Task<string> UpdateUserProfileAsync(string userId, UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.MiddleName = updateUserDto.MiddleName;
            user.PhoneNumber = updateUserDto.PhoneNumber;
            user.DateOfBirth = updateUserDto.DateOfBirth;
            user.Gender = updateUserDto.Gender;
            user.OrganizationId = updateUserDto.OrganizationId;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Update failed: {string.Join("; ", result.Errors.Select(e => e.Description))}");
            }

            return "User profile updated successfully";
        }

        /// <summary>
        /// Удаляет пользователя из системы.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, которого нужно удалить.</param>
        /// <returns>Строка с результатом удаления пользователя.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если пользователь не найден или удаление не удалось.</exception>
        public async Task<string> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Delete failed: {string.Join("; ", result.Errors.Select(e => e.Description))}");
            }

            return "User deleted successfully";
        }
    }
}
