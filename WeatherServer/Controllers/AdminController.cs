using CountryModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WeatherServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class AdminController(UserManager<WorldCityUser> userManager,
        jwtHandler jwtHandler) : ControllerBase
    {
        //get user name, password to generate token
        [HttpPost]
        public void Login() 
        {

        }
    }
}
