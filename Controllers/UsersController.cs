using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Park.Models;
using Park.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/Users")]
    //[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        //tai ta dag su dung authorize o dau nen no se apply vao cac action ben trong nay
        //de ghi de len ta su dung
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate ([FromBody] AuthenticationModel model)
        {
            var user = _userRepo.Authenticate(model.Username, model.Password);
            if (user==null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register ([FromBody] AuthenticationModel model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);            
            if (!ifUserNameUnique)
            {
                return BadRequest(new { message = "Username already exists!" });
            }

            var user = _userRepo.Register(model.Username, model.Password);
            if (user==null)
            {
                return BadRequest(new { message = "Error while registering!" });
            }

            return Ok();
        }
    }
}
