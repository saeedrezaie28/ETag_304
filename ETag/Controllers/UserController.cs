using ETag.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices userServices;

        public UserController(UserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]
        public async Task<IResult> Get(string name, int top)
        {
            var user = await userServices.GetUserByName(name, top);
            return Results.Ok(user);
        }
    }
}
