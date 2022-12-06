using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    public interface ICTEFormService
    {
        Task<bool> AddCTEFormToStudent(string userName, CTEFormDto cTEForm);
        Task<bool> DeleteCTEForm(Guid id);
    }
}
