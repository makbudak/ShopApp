using ShopApp.Data.GenericRepository;
using ShopApp.Extensions;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface IBlogCategoryService
    {
        IQueryable<BlogCategoryModel> GetAll();
        BlogCategoryModel GetById(int id);
        ServiceResult Post(BlogCategoryModel model);
        ServiceResult Put(BlogCategoryModel model);
        ServiceResult Delete(int id);
    }

    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IUnitOfWork _unitofwork;

        public BlogCategoryService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IQueryable<BlogCategoryModel> GetAll()
        {
            return _unitofwork.Repository<BlogCategory>()
                .Where(x => !x.Deleted)
                .OrderByDescending(x => x.Id)
                .Select(x => new BlogCategoryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                });
        }

        public BlogCategoryModel GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public ServiceResult Post(BlogCategoryModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };

            var url = UrlHelper.FriendlyUrl(model.Name);

            var isExistUrl = _unitofwork.Repository<BlogCategory>()
                .Any(x => !x.Deleted && x.Url == url);

            if (isExistUrl)
            {
                result.StatusCode = HttpStatusCode.Found;
                result.Message = "Kategori ile daha önce kayıt mevcuttur.";
                return result;
            }

            var entity = new BlogCategory()
            {
                Deleted = false,
                IsActive = model.IsActive,
                Name = model.Name,
                Url = url
            };

            _unitofwork.Repository<BlogCategory>().Add(entity);
            _unitofwork.Save();

            return result;
        }

        public ServiceResult Put(BlogCategoryModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };

            var entity = _unitofwork.Repository<BlogCategory>()
                .FirstOrDefault(x => !x.Deleted && x.Id == model.Id);

            if (entity == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = "Kategori bulunamadı.";
                return result;
            }
            entity.Name = model.Name;
            entity.IsActive = model.IsActive;
            entity.Url = UrlHelper.FriendlyUrl(model.Name);
            _unitofwork.Repository<BlogCategory>().Update(entity);
            _unitofwork.Save();

            return result;
        }

        public ServiceResult Delete(int id)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };

            var entity = _unitofwork.Repository<BlogCategory>()
               .FirstOrDefault(x => !x.Deleted && x.Id == id);

            if (entity == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = "Kategori bulunamadı.";
                return result;
            }
            entity.Deleted = true;
            _unitofwork.Repository<BlogCategory>().Update(entity);
            _unitofwork.Save();
            return result;
        }
    }
}
