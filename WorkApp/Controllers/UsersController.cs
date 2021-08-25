using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Business.Services.Subjects;
using WorkApp.Business.Services.Users;

namespace WorkApp.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        UsersService usersService;

        public UsersController(UsersService usersService, SubjectsService subjectsService)
        {
            this.usersService = usersService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            var users = await this.usersService.GetStudents();
            return users;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<UserDTO> Get(int id)
        {
            return await this.usersService.GetUserById(id);
        }

        [HttpGet("{login}&{password}")]
        public async Task<UserDTO> Get(string login, string password)
        {
            return await this.usersService.GetUserByLoginData(login, password);
        }

        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var userDTO = JsonConvert.DeserializeObject<UserDTO>(jsonObject.ToString());
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
