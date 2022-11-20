using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    public class ErsmsUserController : BaseApiController
    {
        private readonly DataContext _context;

        public ErsmsUserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErsmsUser>>> GetUsers()
        {
            return await _context.ErsmsUsers.ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ErsmsUser> GetUser(Guid id)
        {
            // SQLite does not support Guid filtering, so we need to convert the Guid to a string
            // see github.com/dotnet/efcore/issues/10662
            return _context.ErsmsUsers.SingleOrDefault(x => x.Id == new Guid(id.ToString()));
        }
    }
}
