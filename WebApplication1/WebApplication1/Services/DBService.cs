using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.DTO_s.Response;

namespace WebApplication1.Services
{
    public class DBService : IDBService
    {
        private readonly MainDbContext _context;
        public DBService(MainDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChanges()
        {
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<TeamRes> GetTeam(int teamID)
        {
            Team team = await _context.Teams.SingleAsync(team => team.TeamID == teamID);
            return new TeamRes { Team = team, Members = _context.Memberships.Where(m => m.TeamID == teamID).Select(s => s.Member) };
        }

        public async Task<bool> AddMember(Member member, int teamID)
        {
            Team team = await GetTeam(teamID);
            if (member.OrganizationID == team.OrganizationID)
            {
                Member newMember = new Member
                {
                    OrganizationID = member.OrganizationID,
                    MemberName = member.MemberName,
                    MemberSurname = member.MemberSurname,
                    MemberNickName = member.MemberNickName
                };
                await _context.Members.AddAsync(newMember);
                if (!(await SaveChanges()))
                    return false;

                Membership membership = new Membership
                {
                    MemberID = member.MemberID,
                    TeamID = team.TeamID,
                    MembershipDate = DateTime.Now
                };
                await _context.Memberships.AddAsync(membership);
                if (await SaveChanges())
                    return _context.Memberships.SingleAsync(m => m.MemberID == member.MemberID && m.TeamID == teamID) != null;
            }
            return false;
        }
    }
}
