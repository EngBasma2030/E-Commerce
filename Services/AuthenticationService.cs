using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Shared.DataTransferObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager , IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            // check if email is exist
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UserNotFoundException(loginDto.Email);
            // check password
            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (IsPasswordValid)
            {
                // return UserDto
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                throw new UnauthorizedException();
            }
        }

       

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // Mapping RegisterDto to Application User
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };

            // createUser [ApplicationUser]
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                // Return UserDto
                return new UserDto()
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                // throw Exception BadRequest
                var errors = result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(errors);
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new (ClaimTypes.Email , user.Email!),
                new (ClaimTypes.Name , user.UserName!),
                new (ClaimTypes.NameIdentifier , user.Id!)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credintials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credintials);

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
