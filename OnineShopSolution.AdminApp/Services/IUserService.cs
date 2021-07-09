using OnlineShopSolution.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnineShopSolution.AdminApp.Services
{
    public interface IUserService
    {
        Task<string> Authenticate(PostLoginDto req);
    }
}
