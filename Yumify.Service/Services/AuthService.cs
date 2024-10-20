﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities.IdentityEntities;
using Yumify.Core.IServices;

namespace Yumify.Service.Services
{
    public class AuthService : IAuthSerivce
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> CreateToken(ApplicationUser applicationUser)
        {
            var PrivateClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,applicationUser.DisplayName),
                new Claim(ClaimTypes.Email,applicationUser.Email!),
                new Claim(ClaimTypes.StreetAddress,applicationUser.Address?.Street??"No Address Provided"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(applicationUser);
            foreach (var role in userRoles)
            {
                PrivateClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var secretKey = _configuration["Jwt:SecreteKey"];
            var key = Encoding.UTF8.GetBytes(secretKey!);
            var authKey = new SymmetricSecurityKey(key);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Iss"],
                audience: _configuration["Jwt:Aud"],
                expires:DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256),
                claims: PrivateClaims
                );

           return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
