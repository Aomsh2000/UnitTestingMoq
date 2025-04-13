using Microsoft.AspNetCore.Mvc;
using UnitTestingUsingMoq.Models;
using UnitTestingUsingMoq.Repositories;

namespace UnitTestingUsingMoq.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _userRepo.AddUser(user);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userRepo.GetByUserId(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await _userRepo.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepo.DeleteUser(id);
            return NoContent();
        }
    }

}
