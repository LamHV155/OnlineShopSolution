using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Dtos.Users
{
    public class PostLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool  RememberMe { get; set; }
    }
}
