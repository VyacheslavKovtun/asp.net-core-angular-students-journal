using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Business.Services.Marks;
using WorkApp.Database.Entities;
using WorkApp.Database.UnitOfWork;

namespace WorkApp.Business.Services.Users
{
    public class UsersService : IUsersService
    {
        IUnitOfWork unitOfWork;
        MarksService marksService;

        public UsersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.marksService = new MarksService(unitOfWork);
        }

        public void CreateNewUser(UserDTO user)
        {
            var u = new User
            {
                Login = user.Login,
                Password = user.Password,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Group = user.Group,
                Course = user.Course
            };

            if (user.Marks != null)
            {
                foreach (var markDTO in user.Marks)
                {
                    var mark = unitOfWork.MarksRepository.GetAsync(markDTO.Id).Result;

                    u.Marks.Add(mark);
                }
            }

            unitOfWork.UsersRepository.Create(u);
        }

        public async Task CreateNewUserAsync(UserDTO user)
        {
            var u = new User
            {
                Login = user.Login,
                Password = user.Password,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Group = user.Group,
                Course = user.Course
            };

            if (user.Marks != null)
            {
                foreach (var markDTO in user.Marks)
                {
                    var mark = await unitOfWork.MarksRepository.GetAsync(markDTO.Id);

                    u.Marks.Add(mark);
                }
            }

            await unitOfWork.UsersRepository.CreateAsync(u);
        }

        public async Task DeleteUserById(int id)
        {
            await unitOfWork.UsersRepository.DeleteAsync(id);
        }

        private async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await unitOfWork.UsersRepository.GetAllAsync();
            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (var user in users)
            {
                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    Role = user.Role,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    Group = user.Group,
                    Course = user.Course
                };

                if (user.Marks != null)
                {
                    foreach (var mark in user.Marks)
                    {
                        var markDTO = await marksService.GetMarkById(mark.Id);

                        userDTO.Marks.Add(markDTO);
                    }
                }

                usersDTO.Add(userDTO);
            }

            return usersDTO;
        }

        public async Task<List<UserDTO>> GetStudents()
        {
            var users = await unitOfWork.UsersRepository.GetAllAsync();
            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (var user in users.Where(u => u.Role == User.AuthRole.User))
            {
                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    Role = user.Role,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    Group = user.Group,
                    Course = user.Course
                };

                if (user.Marks != null)
                {
                    foreach (var mark in user.Marks)
                    {
                        var markDTO = await marksService.GetMarkById(mark.Id);

                        userDTO.Marks.Add(markDTO);
                    }
                }

                usersDTO.Add(userDTO);
            }

            return usersDTO;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await unitOfWork.UsersRepository.GetAsync(id);
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Group = user.Group,
                Course = user.Course
            };

            if (user.Marks != null)
            {
                foreach (var mark in user.Marks)
                {
                    userDTO.Marks.Add(await marksService.GetMarkById(mark.Id));
                }
            }

            return userDTO;
        }

        public async Task UpdateUser(UserDTO user)
        {
            List<Mark> marks = new List<Mark>();
            if (user.Marks != null)
            {
                foreach (var markDTO in user.Marks)
                {
                    marks.Add(new Mark
                    {
                        Id = markDTO.Id,
                        SMark = markDTO.SMark,
                        DateTime = markDTO.DateTime,
                        Subject = markDTO.Subject,
                        UserId = markDTO.UserId
                    });
                }
            }

            var u = new User
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Group = user.Group,
                Course = user.Course,
                Marks = marks
            };

            await unitOfWork.UsersRepository.UpdateAsync(u);
        }

        public async Task<UserDTO> GetUserByLoginData(string login, string password)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
        }
    }
}
