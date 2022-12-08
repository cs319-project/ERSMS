using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface ICTEFormRepository
    {
        Task<bool> AddCTEFormToStudent(string userName, CTEForm cTEForm);
        Task<IEnumerable<CTEForm>> GetCTEForms();
        Task<bool> DeleteCTEForm(Guid id);
        Task<CTEForm> GetCTEForm(Guid id);
        Task<bool> UpdateCTEForm(CTEForm cTEForm);
    }
}
