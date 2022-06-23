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
    [Route("api/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly IDBService _dbserv;
        public TeamsController(MainDbContext context)
        {
            _dbserv = new DBService(context);
        }
        [HttpGet]
        [Route("{teamID}")]
        public async Task<IActionResult> GetTeam([FromRoute]int teamID)
        {
            return Ok(_dbserv.GetTeam(teamID));
        }
    }
}
