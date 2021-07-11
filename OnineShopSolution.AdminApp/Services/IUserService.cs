using OnlineShopSolution.Dtos.Common;
using OnlineShopSolution.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopSolution.AdminApp.Services
{
    public interface IUserService
    {
        Task<string> Authenticate(PostLoginDto req);
        Task<PagedResult<GetUserDto>> GetUserPaging(PostUserDto req);
        Task<bool> RegisterUser(PostRegisterDto req);
    }
}
