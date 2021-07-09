﻿using OnlineShopSolution.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSolution.Service.Users
{
    public interface IUserService
    {
        Task<string> Authenticate(PostLoginDto req);
        Task<bool> Register(PostRegisterDto req);
    }
}
