using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.common.Models
{
    public class User
    {
        public int Id { get; set; }

        public Guid GuidId { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }
    }
}
