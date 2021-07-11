using OnlineShopSolution.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Dtos.Users
{
    public class PostUserDto : PageRequestBase
    {
       public string Keyword { get; set; }
    }
}
