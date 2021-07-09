using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShopSolution.Data.Entities;
using OnlineShopSolution.Dtos.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSolution.Service.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        public UserService(UserManager<User> userManager, 
                           SignInManager<User> signInManager,
                           RoleManager<Role> roleManager,
                           IConfiguration config )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<string> Authenticate(PostLoginDto req)
        {
            var user = await _userManager.FindByNameAsync(req.UserName);
            if (user == null) return null;
            var result = await _signInManager.PasswordSignInAsync(user.UserName, req.Password, req.RememberMe, true);

            if (!result.Succeeded) return null;
            var roles = _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(",", roles)),
                new Claim(ClaimTypes.Name, req.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(PostRegisterDto req)
        {
            var user = new User
            {
                UserName = req.UserName,
                FirstName = req.FirstName,
                LastName = req.LastName,
                DoB = req.DoB,
                Email = req.Email,
                PhoneNumber = req.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, req.Password);
            if (result.Succeeded) return true;
            return false;
        }
    }
}
