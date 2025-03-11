using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Users;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Walls;

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
        private readonly ApplicationDbContext _dbContext;
        private readonly IWallService _wallService;

        /// <summary>
        /// Конструктор для инициализации сервисов UserManager и SignInManager.
        /// </summary>
        /// <param name="userManager">Менеджер пользователей для работы с пользователями.</param>
        /// <param name="signInManager">Менеджер входа для работы с аутентификацией.</param>
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, ApplicationDbContext applicationDbContext, IWallService wallService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _dbContext = applicationDbContext;
            _wallService = wallService;
        }

        /// <summary>
        /// Регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="registerUserDto">Объект с данными нового пользователя.</param>
        /// <returns>Строка с результатом регистрации пользователя.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если регистрация не удалась.</exception>
        public async Task<string> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(registerUserDto);

                // Проверка существования организации
                var organizationExists = await _dbContext.Organizations.AnyAsync(o => o.Id == registerUserDto.OrganizationId);
                if (!organizationExists)
                {
                    return "Organization not found";
                }

                var result = await _userManager.CreateAsync(user, registerUserDto.Password);
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Registration failed: {string.Join("; ", result.Errors.Select(e => e.Description))}");
                }
                await _wallService.CreateWallAsync(user.Id);

                return "User registered successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                throw new ApplicationException("An unexpected error occurred during registration.", ex);
            }
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
                .Include(u => u.SubscribersAsUser)
                .Include(u => u.SubscribersAsSubscriber)
                .Include(u => u.ClubUsers)
                .Include(u => u.UserAvatars)
                .ThenInclude(ua => ua.Media)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.Id == userId);

            Console.WriteLine($"\n\n{user}\n\n");
            if (user == null)
            {
                return null;
            }

            var userProfile = _mapper.Map<UserProfileDTO>(user);
            userProfile.RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "No Role";

            return userProfile;
        }

        /// <summary>
        /// Получение всех профилей пользователей
        /// </summary>
        /// <returns>Объект профилей пользователей.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если пользователи не найдены или обновление не удалось.</exception>
        public async Task<List<UserProfileDTO>> GetAllUserProfilesAsync()
        {
            var users = await _userManager.Users
                .Include(u => u.Organization)
                .Include(u => u.SubscribersAsSubscriber)
                .Include(u => u.SubscribersAsUser)
                .Include(u => u.ClubUsers)
                .Include(u => u.UserAvatars)
                .ThenInclude(ua => ua.Media)
                .ToListAsync();
            if (users == null || !users.Any())
            {
                return new List<UserProfileDTO>();
            }

            var usersProdiles = _mapper.Map<List<UserProfileDTO>>(users);
            return usersProdiles;
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

        /// <summary>
        /// Восстановление пароля пользователя.
        /// </summary>
        /// <param name="recoveryPassword">Сброс пароля</param>
        /// <returns>Строка с результатом обновления.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если пользователь не найден или обновление не удалось.</exception>
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

            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);

            await _wallService.DeleteWallAsync(userId);

            return "User deleted successfully";
        }
    }
}
