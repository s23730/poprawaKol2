using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.DTO_s.Response;

namespace WebApplication1.Services
{
    interface IDBService
    {
        Task<TeamRes> GetTeam(int teamID);
        Task<bool> AddMember(Member member, int teamID);
        Task<bool> SaveChanges();
    }
}
