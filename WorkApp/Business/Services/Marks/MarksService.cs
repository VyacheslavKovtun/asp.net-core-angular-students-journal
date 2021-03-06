using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Database.Entities;
using WorkApp.Database.UnitOfWork;

namespace WorkApp.Business.Services.Marks
{
    public class MarksService : IMarksService
    {
        IUnitOfWork unitOfWork;

        public MarksService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewMarkAsync(MarkDTO mark)
        {
            var m = new Mark
            {
                SMark = mark.SMark,
                DateTime = mark.DateTime,
                Subject = mark.Subject,
                UserId = mark.UserId
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

            foreach (var mark in marks)
            {
                var markDTO = new MarkDTO
                {
                    Id = mark.Id,
                    SMark = mark.SMark,
                    DateTime = mark.DateTime,
                    Subject = mark.Subject,
                    UserId = mark.UserId
                };

                marksDTO.Add(markDTO);
            }

            return marksDTO;
        }

        public async Task<MarkDTO> GetMarkById(int id)
        {
            var mark = await unitOfWork.MarksRepository.GetAsync(id);

            MarkDTO markDTO = new MarkDTO
            {
                Id = mark.Id,
                SMark = mark.SMark,
                DateTime = mark.DateTime,
                Subject = mark.Subject,
                UserId = mark.UserId
            };

            return markDTO;
        }

        public async Task<List<MarkDTO>> GetMarksByUserId(int userId)
        {
            var marks = await unitOfWork.MarksRepository.GetAllAsync();
            var userMarks = marks.Where(m => m.UserId == userId);

            List<MarkDTO> marksDTO = new List<MarkDTO>();

            foreach (var uMark in userMarks)
            {
                MarkDTO markDTO = new MarkDTO
                {
                    Id = uMark.Id,
                    SMark = uMark.SMark,
                    DateTime = uMark.DateTime,
                    Subject = uMark.Subject,
                    UserId = uMark.UserId
                };

                marksDTO.Add(markDTO);
            }

            return marksDTO;
        }

        public async Task<MarkDTO> GetMarkByDateTime(long dateTime)
        {
            var marks = await unitOfWork.MarksRepository.GetAllAsync();
            var mark = marks.FirstOrDefault(m => m.DateTime == dateTime);

            MarkDTO markDTO = new MarkDTO
            {
                Id = mark.Id,
                SMark = mark.SMark,
                DateTime = mark.DateTime,
                Subject = mark.Subject,
                UserId = mark.UserId
            };

            return markDTO;
        }

        public async Task UpdateMark(MarkDTO mark)
        {
            var m = new Mark
            {
                Id = mark.Id,
                SMark = mark.SMark,
                DateTime = mark.DateTime,
                Subject = mark.Subject,
                UserId = mark.UserId
            };

            await unitOfWork.MarksRepository.UpdateAsync(m);
        }
    }
}
