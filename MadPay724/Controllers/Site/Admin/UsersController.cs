using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadPay724.Data.DatabaseContext;
using MadPay724.Repository.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadPay724.Presentation.Controllers.Site.Admin
{
    [ApiExplorerSettings(GroupName = "Site ")]
    [Route("site/admin/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork<MadPayDbContext> _unit;
        public UsersController(IUnitOfWork<MadPayDbContext> unit)
        {
            _unit = unit;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _unit.UserRepository.GetAllAsync();

            return Ok(user);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _unit.UserRepository.GetByIdAsync(id);

            return Ok(user);
        }
    }
}