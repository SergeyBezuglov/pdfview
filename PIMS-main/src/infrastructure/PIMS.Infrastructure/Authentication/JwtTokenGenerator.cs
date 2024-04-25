using PIMS.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;
using PIMS.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.Common.Authentication.Configuration;
using PIMS.Domain.UserDataAggregate;

namespace PIMS.Infrastructure.Authentication
{
    /// <summary>
    ///Генератор токенов jwt.
    /// </summary>
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        /// <summary>
        /// Поставщик даты и времени.
        /// </summary>
        private readonly IDateTimeProvider _dateTimeProvider;
        /// <summary>
        /// Настройки jwt.
        /// </summary>
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="JwtTokenGenerator"/> .
        /// </summary>
        /// <param name="dateTimeProvider">Поставщик даты и времени.</param>
        /// <param name="jwtSettings">Настройки jwt.</param>
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider,IOptions<JwtAuthenticationModuleOption> jwtSettings)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtSettings.Value.Settings;
             
        }
        /// <summary>
        /// Генерирует токен.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="userData">Пользовательские данные</param>
        /// <returns>Строка.</returns>
        public string GenerateToken(User user,UserData userData)
        {
            return BaseGenerateToken(user, userData, _jwtSettings, _dateTimeProvider);
        }
        /// <summary>
        /// Основывает токен генерации.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="userData">Пользовательские данные.</param>
        /// <param name="settings">Настройки.</param>
        /// <param name="_dateTimeProvider">Поставщик даты и времени.</param>
        /// <returns>Строка.</returns>
        public static string BaseGenerateToken(User user, UserData userData, JwtSettings settings, IDateTimeProvider _dateTimeProvider)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret)),
                SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName,userData.FirstName ?? ""),
                new Claim(JwtRegisteredClaimNames.FamilyName,userData.LastName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti,userData.Id.Value.ToString()),
                
            };
            var securityToken = new JwtSecurityToken(
                claims: claims,
                issuer: settings.Issuer,
                audience: settings.Audience,
                expires: _dateTimeProvider.UtcNow.AddDays(settings.ExpiryDays),
                signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }
    }
}
