﻿using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models.User.Dto
{
    public class UpdateUserDto
    {
        [MaxLength(40)]
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
