﻿using MyBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity.DTOs.Users
{
    public class UserAddDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public  string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public List<AppRole> Roles { get; set; }

        public string? LinkedInLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? ContactMail { get; set; }
        public string? GithubLink { get; set; }

    }
}

