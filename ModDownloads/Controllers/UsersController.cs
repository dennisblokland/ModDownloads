using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModDownloads.Server.Models;
using ModDownloads.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModDownloads.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;
        UserManager<IdentityUser> _userManager;
        public UsersController(IJwtTokenService tokenService, UserManager<IdentityUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }
        [Authorize]
        // Registration method to create new Identity users
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] UserViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var result = await _userManager.CreateAsync(new IdentityUser()
            {
                UserName = vm.Email,
                Email = vm.Email
            }, vm.Password);

            if (result.Succeeded == false)
            {
                return StatusCode(500, result.Errors);
            }

            return Ok();
        }


        // Login method to allow Identity user logins
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(vm.Email);
            var correctUser = await _userManager.CheckPasswordAsync(user, vm.Password);

            if (correctUser == false)
            {
                return BadRequest("Username or password is incorrect");
            }
            JwToken token = new JwToken();
            token.Token = GenerateToken(vm.Email);
            return Ok(token);
        }
        [HttpPost]
        [Route("Validate")]
        public async Task<IActionResult> Validate([FromBody] string token)
        {
            return Ok(_tokenService.ValidateToken(token));
        }

        // Generates a token from the token service and returns it as a string
        private string GenerateToken(string email)
        {
            var token = _tokenService.BuildToken(email);

            return token;
        }

    }
}
