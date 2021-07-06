using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Utilities.Exceptions
{
    public class OnlineShopException : Exception
    {
        public OnlineShopException() { }

        public OnlineShopException(string msg) : base(msg) { }

        public OnlineShopException(string msg, Exception inner): base(msg, inner) { }
    }
}
