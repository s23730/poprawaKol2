using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MembersController : ControllerBase
    {
        private readonly IDBService _dbserv;
        public MembersController(MainDbContext context)
        {
            _dbserv = new DBService(context);
        }
        [HttpPost]
        [Route("{teamID}")]
        public async Task<IActionResult> AddMember([FromBody]Member member,[FromRoute]int teamID)
        {
            if ((await _dbserv.GetTeam(teamID)) != null)
            {
                if (await _dbserv.AddMember(member, teamID))
                    return Ok("Dodano uzytkownika.");
                else
                    return BadRequest("Dodanie uzytkownika nie powiodlo sie.");
            }
            else
                return NotFound("Nie znaleziono zespolu o podanym ID.");
        }
    }
}
