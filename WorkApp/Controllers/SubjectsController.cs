using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Business.Services.Subjects;

namespace WorkApp.Controllers
{
    [ApiController]
    [Route("subjects")]
    public class SubjectsController : ControllerBase
    {
        SubjectsService subjectsService;

        public SubjectsController(SubjectsService subjectsService)
        {
            this.subjectsService = subjectsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<SubjectDTO>> Get()
        {
            var subjects = await this.subjectsService.GetAllSubjects();
            return subjects;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<SubjectDTO> Get(int id)
        {
            return await this.subjectsService.GetSubjectById(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var subjectDTO = JsonConvert.DeserializeObject<SubjectDTO>(jsonObject.ToString());
                await this.subjectsService.CreateNewSubject(subjectDTO);

                return Ok(subjectDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var subjectDTO = JsonConvert.DeserializeObject<SubjectDTO>(jsonObject.ToString());
                await this.subjectsService.UpdateSubject(subjectDTO);

                return Ok(subjectDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.subjectsService.GetSubjectById(id).Result != null)
            {
                await this.subjectsService.DeleteSubjectById(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
