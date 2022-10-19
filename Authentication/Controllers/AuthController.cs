using Authentication.Data;
using Authentication.Models.Auth;
using Authentication.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthenticationDbContext dbContext;
        public AuthController(AuthenticationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> AuthSignup(Signup signup)
        {
            var user = new User()
            {
                fullname = signup.fullname,
                username = signup.username,
                password = signup.password,
                email = signup.email
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Signin(Signin signin)
        {
            var user = dbContext.Users.Where(s => s.username == signin.username).FirstOrDefault();
            if (user != null)
            {
                if (user.password == signin.password)
                {
                    return Ok(user);
                }
                return NotFound();
            }
            return NotFound();
        }
    }
}
