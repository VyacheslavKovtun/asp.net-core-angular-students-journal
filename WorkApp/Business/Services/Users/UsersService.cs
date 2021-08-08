﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Business.Services.Marks;
using WorkApp.Business.Services.Subjects;
using WorkApp.Database.Entities;
using WorkApp.Database.UnitOfWork;

namespace WorkApp.Business.Services.Users
{
    public class UsersService : IUsersService
    {
        IUnitOfWork unitOfWork;
        SubjectsService subjectsService;
        MarksService marksService;

        public UsersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.subjectsService = new SubjectsService(unitOfWork);
            this.marksService = new MarksService(unitOfWork);
        }

        public async Task CreateNewUser(UserDTO user)
        {
            var u = new User
            {
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Group = user.Group,
                Course = user.Course
            };

            foreach (var markDTO in user.Marks)
            {
                var mark = await unitOfWork.MarksRepository.GetAsync(markDTO.Id);

                u.Marks.Add(mark);
            }

            await unitOfWork.UsersRepository.CreateAsync(u);
        }

        public async Task DeleteUserById(int id)
        {
            await unitOfWork.UsersRepository.DeleteAsync(id);
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await unitOfWork.UsersRepository.GetAllAsync();
            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach(var user in users)
            {
                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    Group = user.Group,
                    Course = user.Course
                };

                foreach(var mark in user.Marks)
                {
                    var markDTO = await marksService.GetMarkById(mark.Id);

                    userDTO.Marks.Add(markDTO);
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
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Group = user.Group,
                Course = user.Course
            };

            foreach(var mark in user.Marks)
            {
                userDTO.Marks.Add(await marksService.GetMarkById(mark.Id));
            }

            return userDTO;
        }

        public async Task UpdateUser(UserDTO user)
        {
            List<Mark> marks = new List<Mark>();
            foreach(var markDTO in user.Marks)
            {
                var subject = await unitOfWork.SubjectsRepository.GetAsync(markDTO.Subject.Id);

                marks.Add(new Mark
                {
                    Id = markDTO.Id,
                    SMark = markDTO.SMark,
                    Subject = subject
                });
            }

            var u = new User
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Group = user.Group,
                Course = user.Course,
                Marks = marks
            };

            await unitOfWork.UsersRepository.UpdateAsync(u);
        }
    }
}
