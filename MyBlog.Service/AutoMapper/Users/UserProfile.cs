using AutoMapper;
using MyBlog.Entity.DTOs.Categories;
using MyBlog.Entity.DTOs.Users;
using MyBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.AutoMapper.Users
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, AppUser>().ReverseMap();
            CreateMap<UserAddDto, AppUser>().ReverseMap();
            CreateMap<UserAddDto, UserDto>().ReverseMap();
            CreateMap<UserUpdateDto, AppUser>().ReverseMap();
            CreateMap<UserProfileDto, AppUser>().ReverseMap();
           
        }
    }
}
