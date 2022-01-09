using ShopApp.Data.GenericRepository;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface IRoleService
    {
        List<Role> GetAll();
        ServiceResult Delete(int id);
        ServiceResult Put(Role model);
        ServiceResult Post(Role model);
    }

    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Role> GetAll()
        {
            return _unitOfWork.Repository<Role>().GetAll(x => !x.Deleted).OrderByDescending(x => x.Id).ToList();
        }

        public ServiceResult Post(Role model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            try
            {
                _unitOfWork.Repository<Role>().Add(model);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
                return result;
            }
            return result;
        }

        public ServiceResult Put(Role model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            try
            {
                var role = _unitOfWork.Repository<Role>().Get(x => x.Id == model.Id && !x.Deleted);
                if (role != null)
                {
                    role.IsActive = model.IsActive;
                    role.Name = model.Name;                    
                    _unitOfWork.Save();
                }
                else
                {
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.Message = "Kayıt bulunamadı";
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
                return result;
            }
            return result;
        }

        public ServiceResult Delete(int id)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            try
            {
                var role = _unitOfWork.Repository<Role>().Get(x => x.Id == id && !x.Deleted);
                if (role != null)
                {
                    role.Deleted = true;
                    _unitOfWork.Save();
                }
                else
                {
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.Message = "Kayıt bulunamadı";
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
                return result;
            }
            return result;
        }
    }
}
