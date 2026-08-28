using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.User
{
    public class UserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
