using Microsoft.EntityFrameworkCore;
using ShopApp.Data.GenericRepository;
using ShopApp.Model.Consts;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface ITodoService
    {
        List<TodoModel> GetAll();
        TodoModel GetById(int id);
        IQueryable<TodoModel> GetUserTodos(int userId);
        ServiceResult Post(TodoModel model);
        ServiceResult Put(TodoModel model);
        ServiceResult Delete(int id);
    }

    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork _unitofwork;

        public TodoService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public List<TodoModel> GetAll()
        {
            var list = _unitofwork.Repository<Todo>()
               .Where(x => !x.Deleted)
               .Include(x => x.TodoStatus)
               .ThenInclude(x => x.TodoCategory)
               .Include(x => x.User)
               .OrderByDescending(x => x.Id)
               .Select(x => new TodoModel
               {
                   Description = x.Description,
                   Id = x.Id,
                   InsertedDate = x.InsertedDate,
                   IsActive = x.IsActive,
                   Title = x.Title,
                   TodoCategoryId = x.TodoStatus.TodoCategoryId,
                   CategoryName = x.TodoStatus.TodoCategory.Name,
                   TodoStatusId = x.TodoStatusId,
                   StatusName = x.TodoStatus.Name,
                   UpdatedDate = x.UpdatedDate,
                   UserId = x.UserId,
                   NameSurname = x.User.Name + " " + x.User.Surname
               }).ToList();
            return list;
        }

        public TodoModel GetById(int id)
        {
            return _unitofwork.Repository<Todo>()
               .Where(x => !x.Deleted && x.Id == id)
               .Include(x => x.TodoStatus)
               .ThenInclude(x => x.TodoCategory)
               .Include(x => x.User)
               .Select(x => new TodoModel
               {
                   Description = x.Description,
                   Id = x.Id,
                   InsertedDate = x.InsertedDate,
                   IsActive = x.IsActive,
                   Title = x.Title,
                   TodoCategoryId = x.TodoStatus.TodoCategoryId,
                   CategoryName = x.TodoStatus.TodoCategory.Name,
                   TodoStatusId = x.TodoStatusId,
                   StatusName = x.TodoStatus.Name,
                   UpdatedDate = x.UpdatedDate,
                   UserId = x.UserId,
                   NameSurname = x.User.Name + " " + x.User.Surname
               }).FirstOrDefault();
        }

        public IQueryable<TodoModel> GetUserTodos(int userId)
        {
            return _unitofwork.Repository<Todo>()
                .Where(x => !x.Deleted && x.IsActive && x.UserId == userId, x => x
                .Include(x => x.TodoStatus)
                .ThenInclude(x => x.TodoCategory)
                .Include(x => x.User))
                .Select(x => new TodoModel
                {
                    Description = x.Description,
                    Id = x.Id,
                    InsertedDate = x.InsertedDate,
                    IsActive = x.IsActive,
                    Title = x.Title,
                    TodoCategoryId = x.TodoStatus.TodoCategoryId,
                    CategoryName = x.TodoStatus.TodoCategory.Name,
                    TodoStatusId = x.TodoStatusId,
                    StatusName = x.TodoStatus.Name,
                    UpdatedDate = x.UpdatedDate,
                    UserId = x.UserId,
                    NameSurname = x.User.Name + " " + x.User.Surname
                })
                .OrderByDescending(x => x.Id)
                .AsQueryable();
        }

        public ServiceResult Post(TodoModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK, Message = AlertMessages.Post };

            var todo = new Todo
            {
                Deleted = false,
                Description = model.Description,
                InsertedDate = DateTime.Now,
                IsActive = model.IsActive,
                Title = model.Title,
                TodoStatusId = model.TodoStatusId,
                UserId = model.UserId
            };
            _unitofwork.Repository<Todo>().Add(todo);
            _unitofwork.Save();

            return result;
        }

        public ServiceResult Put(TodoModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK, Message = AlertMessages.Put };

            var todo = _unitofwork.Repository<Todo>()
                   .FirstOrDefault(x => x.Id == model.Id);

            if (todo == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = AlertMessages.NotFound;
                return result;
            }

            todo.Description = model.Description;
            todo.IsActive = model.IsActive;
            todo.Title = model.Title;
            todo.TodoStatusId = model.TodoStatusId;
            todo.UpdatedDate = DateTime.Now;
            todo.UserId = model.UserId;
            _unitofwork.Save();

            return result;
        }

        public ServiceResult Delete(int id)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK, Message = AlertMessages.Delete };

            var todo = _unitofwork.Repository<Todo>()
                   .FirstOrDefault(x => x.Id == id);

            if (todo == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = AlertMessages.NotFound;
                return result;
            }

            todo.Deleted = true;
            _unitofwork.Save();

            return result;
        }
    }
}
