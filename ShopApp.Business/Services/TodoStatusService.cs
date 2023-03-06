using Microsoft.EntityFrameworkCore;
using ShopApp.Data.GenericRepository;
using ShopApp.Model.Consts;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface ITodoStatusService
    {
        IQueryable<TodoStatus> GetAll();
        List<TodoStatusModel> GetByTodoCategoryId(int categoryId);
        TodoStatusModel GetById(int id);
        ServiceResult Post(TodoStatusModel model);
        ServiceResult Put(TodoStatusModel model);
        ServiceResult Delete(int id);
    }

    public class TodoStatusService : ITodoStatusService
    {
        private readonly IUnitOfWork _unitofwork;

        public TodoStatusService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IQueryable<TodoStatus> GetAll()
        {
            return _unitofwork.Repository<TodoStatus>()
                .Where(x => !x.Deleted)
                .Include(o => o.TodoCategory)
                .OrderBy(x => x.Id).AsQueryable();
        }

        public List<TodoStatusModel> GetByTodoCategoryId(int todoCategoryId)
        {
            return _unitofwork.Repository<TodoStatus>()
                .Where(x => !x.Deleted && x.TodoCategoryId == todoCategoryId)
                .Include(o => o.TodoCategory)
                .Select(x => new TodoStatusModel
                {
                    DisplayOrder = x.DisplayOrder,
                    Id = x.Id,
                    Name = x.Name,
                    TodoCategoryId = x.TodoCategoryId,
                    TodoCategoryName = x.TodoCategory.Name
                }).OrderBy(x => x.DisplayOrder).ToList();
        }

        public TodoStatusModel GetById(int id)
        {
            return _unitofwork.Repository<TodoStatus>()
                .Where(x => !x.Deleted && x.Id == id)
                .Include(x => x.TodoCategory)
                .Select(x => new TodoStatusModel
                {
                    DisplayOrder = x.DisplayOrder,
                    Id = x.Id,
                    Name = x.Name,
                    TodoCategoryId = x.TodoCategoryId,
                    TodoCategoryName = x.TodoCategory.Name
                }).FirstOrDefault();
        }

        public ServiceResult Post(TodoStatusModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK, Message = AlertMessages.Post };

            var todoStatus = new TodoStatus
            {
                Deleted = false,
                TodoCategoryId = model.TodoCategoryId,
                Name = model.Name,
                DisplayOrder = model.DisplayOrder
            };
            _unitofwork.Repository<TodoStatus>().Add(todoStatus);
            _unitofwork.Save();

            return result;
        }

        public ServiceResult Put(TodoStatusModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK, Message = AlertMessages.Put };

            var todoStatus = _unitofwork.Repository<TodoStatus>()
                               .FirstOrDefault(x => x.Id == model.Id);

            if (todoStatus == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = AlertMessages.NotFound;
                return result;
            }
            todoStatus.Name = model.Name;
            todoStatus.TodoCategoryId = model.TodoCategoryId;
            todoStatus.DisplayOrder = model.DisplayOrder;
            _unitofwork.Save();

            return result;
        }

        public ServiceResult Delete(int id)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK, Message = AlertMessages.Delete };

            var todoStatus = _unitofwork.Repository<TodoStatus>()
                   .FirstOrDefault(x => x.Id == id);

            if (todoStatus == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = AlertMessages.NotFound;
                return result;
            }
            todoStatus.Deleted = true;
            _unitofwork.Save();

            return result;
        }
    }
}
