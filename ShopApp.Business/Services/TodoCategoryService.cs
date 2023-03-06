using ShopApp.Data.GenericRepository;
using ShopApp.Model.Consts;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface ITodoCategoryService
    {
        IQueryable<TodoCategory> GetAll();
        TodoCategoryModel GetById(int id);
        ServiceResult Post(TodoCategoryModel model);
        ServiceResult Put(TodoCategoryModel model);
        ServiceResult Delete(int id);
    }

    public class TodoCategoryService : ITodoCategoryService
    {
        private readonly IUnitOfWork _unitofwork;

        public TodoCategoryService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IQueryable<TodoCategory> GetAll()
        {
            return _unitofwork.Repository<TodoCategory>()
                .Where(x => !x.Deleted)
                .OrderByDescending(x => x.Id).AsQueryable();
        }

        public TodoCategoryModel GetById(int id)
        {
            return _unitofwork.Repository<TodoCategory>()
                .Where(x => !x.Deleted && x.Id == id)
                .Select(x => new TodoCategoryModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefault();
        }

        public ServiceResult Post(TodoCategoryModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK, Message = AlertMessages.Post };

            var todoCategory = new TodoCategory
            {
                Deleted = false,
                Name = model.Name
            };
            _unitofwork.Repository<TodoCategory>().Add(todoCategory);
            _unitofwork.Save();

            return result;
        }

        public ServiceResult Put(TodoCategoryModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK, Message = AlertMessages.Put };

            var todoCategory = _unitofwork.Repository<TodoCategory>()
                    .FirstOrDefault(x => x.Id == model.Id);

            if (todoCategory == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = "Kategori bulunamadı.";
                return result;
            }
            todoCategory.Name = model.Name;
            _unitofwork.Save();

            return result;
        }

        public ServiceResult Delete(int id)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK, Message = AlertMessages.Delete };

            var todoCategory = _unitofwork.Repository<TodoCategory>()
                   .FirstOrDefault(x => x.Id == id);

            if (todoCategory == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = "Kategori bulunamadı.";
                return result;
            }
            todoCategory.Deleted = true;
            _unitofwork.Save();

            return result;
        }
    }
}
