using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Business.Services.Marks;

namespace WorkApp.Controllers
{
    [ApiController]
    [Route("marks")]
    public class MarksController : ControllerBase
    {
        MarksService marksService;

        public MarksController(MarksService marksService)
        {
            this.marksService = marksService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<MarkDTO>> Get()
        {
            var marks = await this.marksService.GetAllMarks();
            return marks;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<MarkDTO> Get(int id)
        {
            return await this.marksService.GetMarkById(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var markDTO = JsonConvert.DeserializeObject<MarkDTO>(jsonObject.ToString());
                await this.marksService.CreateNewMark(markDTO);

                return Ok(markDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var markDTO = JsonConvert.DeserializeObject<MarkDTO>(jsonObject.ToString());
                await this.marksService.UpdateMark(markDTO);

                return Ok(markDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.marksService.GetMarkById(id).Result != null)
            {
                await this.marksService.DeleteMarkById(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
