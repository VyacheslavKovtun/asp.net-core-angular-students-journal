using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Business.Services.Subjects;
using WorkApp.Database.Entities;
using WorkApp.Database.UnitOfWork;

namespace WorkApp.Business.Services.Marks
{
    public class MarksService : IMarksService
    {
        IUnitOfWork unitOfWork;
        SubjectsService subjectsService;

        public MarksService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.subjectsService = new SubjectsService(unitOfWork);
        }

        public async Task CreateNewMark(MarkDTO mark)
        {
            var subject = await unitOfWork.SubjectsRepository.GetAsync(mark.Subject.Id);

            var m = new Mark
            {
                SMark = mark.SMark,
                Subject = subject
            };

            await unitOfWork.MarksRepository.CreateAsync(m);
        }

        public async Task DeleteMarkById(int id)
        {
            await unitOfWork.MarksRepository.DeleteAsync(id);
        }

        public async Task<List<MarkDTO>> GetAllMarks()
        {
            var marks = await unitOfWork.MarksRepository.GetAllAsync();
            List<MarkDTO> marksDTO = new List<MarkDTO>();

            foreach(var mark in marks)
            {
                var subjectDTO = await subjectsService.GetSubjectById(mark.Subject.Id);

                var markDTO = new MarkDTO
                {
                    Id = mark.Id,
                    SMark = mark.SMark,
                    Subject = subjectDTO
                };
                marksDTO.Add(markDTO);
            }

            return marksDTO;
        }

        public async Task<MarkDTO> GetMarkById(int id)
        {
            var mark = await unitOfWork.MarksRepository.GetAsync(id);

            var subjectDTO = await subjectsService.GetSubjectById(mark.Subject.Id);

            return new MarkDTO
            {
                Id = mark.Id,
                SMark = mark.SMark,
                Subject = subjectDTO
            };
        }

        public async Task UpdateMark(MarkDTO mark)
        {
            var subject = new Subject
            {
                Id = mark.Subject.Id,
                Name = mark.Subject.Name
            };

            var m = new Mark
            {
                Id = mark.Id,
                SMark = mark.SMark,
                Subject = subject
            };

            await unitOfWork.MarksRepository.UpdateAsync(m);
        }
    }
}
