using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="ToDoItem"/> to <see cref="ToDoItemDto"/> and vice versa.</summary>
    public class ToDoItemProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="ToDoItemDto"/> and <see cref="ToDoItem"/>.</summary>
        public ToDoItemProfile()
        {
            CreateMap<ToDoItem, ToDoItemDto>().ReverseMap();
        }
    }
}
