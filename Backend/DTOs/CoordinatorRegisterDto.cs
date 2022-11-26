using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.DTOs
{
    public class CoordinatorRegisterDto : RegisterDto
    {
        public DepartmentInfo Department { get; set; }
    }
}
