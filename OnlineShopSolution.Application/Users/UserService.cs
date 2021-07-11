using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShopSolution.Data.Entities;
using OnlineShopSolution.Dtos.Common;
using OnlineShopSolution.Dtos.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public async Task<PagedResult<GetUserDto>> GetUserPaging(PostUserDto req)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(req.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(req.Keyword) || x.PhoneNumber.Contains(req.Keyword));

            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .Select(x => new GetUserDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email

                }).ToListAsync();

            var pagedResult = new PagedResult<GetUserDto>
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
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
