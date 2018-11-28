using System;
using System.Collections.Generic;
using System.Text;

namespace ZFood.Core.API
{
    public class CreateUserRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
    }
}
