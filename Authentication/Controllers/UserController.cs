using Authentication.Data;
using Authentication.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private readonly AuthenticationDbContext dbContext;

        public UserController(AuthenticationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await dbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser createUser)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                fullname = createUser.fullname,
                username = createUser.username,
                password = createUser.password,
                email = createUser.email,
                sex = createUser.sex,
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUser updateUser)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                user.fullname = updateUser.fullname;
                user.password = updateUser.password;
                user.email = updateUser.email;
                user.sex = updateUser.sex;
            }

            await dbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            dbContext.Remove(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}
