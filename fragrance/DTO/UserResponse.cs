﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fragrance.DTO
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}