using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErsmsUsersController : ControllerBase
    {
        private readonly DataContext _context;

        public ErsmsUsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ErsmsUser>> GetUsers()
        {
            return _context.ErsmsUsers.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ErsmsUser> GetUser(int id)
        {
            return _context.ErsmsUsers.Find(id);
        }
    }
}
