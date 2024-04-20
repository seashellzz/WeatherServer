using CountryModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WeatherServer.Dto;

namespace WeatherServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class AdminController(UserManager<WorldCityUser> userManager,
        jwtHandler jwtHandler) : ControllerBase
    {
        //get user name, password to generate token
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest) 
        {
            WorldCityUser? user = await userManager.FindByNameAsync(loginRequest.UserName);
            // ? means it could be null
            if(user == null)
            {
                return Unauthorized("Bad username");
            }
            bool success = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if(!success)
            {
                return Unauthorized("Wrong password");
            }
            JwtSecurityToken token = await jwtHandler.GetTokenAsync(user); //generate token for user
            string jwtstring = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new LoginResult
            {
                Success = true,
                Message = "Login Success",
                Token = jwtstring
            });
           
        }
    }
}
