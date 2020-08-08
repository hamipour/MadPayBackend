using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadPay724.Common.Helpers;
using MadPay724.Common.Messages;
using MadPay724.Data.DatabaseContext;
using MadPay724.Repository.Infrastructure;
using MadPay724.Services.Interface;
using MadPay724.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MadPay724.Presentation.Controllers.Site.Admin
{
    [ApiExplorerSettings(GroupName = "Site ")]
    [Route("site/admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork<MadPayDbContext> _db;
        private readonly IAuthService _auth;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthService auth,
            IUnitOfWork<MadPayDbContext> db,
            IConfiguration config,
            IMapper mapper)
        {
            _db = db;
            _auth = auth;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            Data.Models.User user = await _auth.Login(loginModel.Username, loginModel.Password);

            if (user == null)
            {
                return Unauthorized(new ReturnMessage()
                {
                    Status = false,
                    Title = "خطا در احراز هویت",
                    Message = "نام کاربری یا کلمه عبور صحیح نمی باشد"
                });
            }
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            SigningCredentials credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = loginModel.RememberMe ? DateTime.Now.AddDays(1) : DateTime.Now.AddHours(2),
                SigningCredentials = credential
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupViewModel signupModel)
        {
            if (!ModelState.IsValid)
                return null;

            var user = _mapper.Map<MadPay724.Data.Models.User>(signupModel);
            await _auth.Signup(user, signupModel.Password);


            return Ok();
        }
    }
}