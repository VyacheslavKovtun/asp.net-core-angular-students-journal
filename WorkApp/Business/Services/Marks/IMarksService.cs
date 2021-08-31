using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;

namespace WorkApp.Business.Services.Marks
{
    public interface IMarksService
    {
        Task CreateNewMarkAsync(MarkDTO mark);
        Task<List<MarkDTO>> GetAllMarks();
        Task<MarkDTO> GetMarkById(int id);
        Task UpdateMark(MarkDTO mark);
        Task DeleteMarkById(int id);
    }
}
