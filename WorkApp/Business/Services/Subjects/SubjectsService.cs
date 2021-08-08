using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Database.Entities;
using WorkApp.Database.UnitOfWork;

namespace WorkApp.Business.Services.Subjects
{
    public class SubjectsService : ISubjectsService
    {
        IUnitOfWork unitOfWork;

        public SubjectsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewSubject(SubjectDTO subject)
        {
            var sub = new Subject
            {
                Name = subject.Name
            };

            await unitOfWork.SubjectsRepository.CreateAsync(sub);
        }

        public async Task<List<SubjectDTO>> GetAllSubjects()
        {
            var subjects = await unitOfWork.SubjectsRepository.GetAllAsync();

            return subjects.Select(sub => new SubjectDTO
            {
                Id = sub.Id,
                Name = sub.Name
            }).ToList();
        }

        public async Task<SubjectDTO> GetSubjectById(int id)
        {
            var sub = await unitOfWork.SubjectsRepository.GetAsync(id);

            return new SubjectDTO
            {
                Id = sub.Id,
                Name = sub.Name
            };
        }
    }
}
