using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Business.Services.Users;

namespace WorkApp.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;

            if (this.usersService.GetAllUsers().Result.Count == 0)
            {
                this.usersService.CreateNewUser(new Business.DTO.UserDTO
                {
                    Login = "pupkin01",
                    Password = "pupkin0110",
                    Role = Database.Entities.User.AuthRole.User,
                    FirstName = "Vasya",
                    LastName = "Pupkin",
                    Age = 19,
                    Group = "APR19",
                    Course = 2
                });
            }
        }

        /*[Authorize]*/
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            var users = await this.usersService.GetAllUsers();
            return users;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<UserDTO> Get(int id)
        {
            return await this.usersService.GetUserById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                await this.usersService.CreateNewUserAsync(userDTO);

                return Ok(userDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                await this.usersService.UpdateUser(userDTO);

                return Ok(userDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.usersService.GetUserById(id).Result != null)
            {
                await this.usersService.DeleteUserById(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
