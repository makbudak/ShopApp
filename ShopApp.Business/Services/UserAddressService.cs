using Microsoft.EntityFrameworkCore;
using ShopApp.Data.GenericRepository;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.User;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface IUserAddressService
    {
        List<UserAddressModel> Get();
        UserAddressModel Get(int Id);
        ServiceResult Post(UserAddressModel model);
        ServiceResult Put(UserAddressModel model);
        ServiceResult Delete(int id);
    }

    public class UserAddressService : IUserAddressService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<UserAddressModel> Get()
        {
            return _unitOfWork.Repository<UserAddress>()
                .Where(x => x.UserId == AuthContent.Current.UserId &&
                !x.Deleted)
                .Include(x => x.City)
                .Include(x => x.District)
                .Include(x => x.Neighborhood)
                .Select(x => new UserAddressModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    CityId = x.CityId,
                    CityName = x.City.Name,
                    DistrictId = x.DistrictId,
                    DistrictName = x.District.Name,
                    NameSurname = x.NameSurname,
                    NeighborhoodId = x.NeighborhoodId,
                    NeighborhoodName = x.Neighborhood.Name,
                    Phone = x.Phone,
                    PostCode = x.PostCode,
                    Title = x.Title
                }).ToList();
        }

        public UserAddressModel Get(int id)
        {
            var address = _unitOfWork.Repository<UserAddress>()
                 .Where(x => x.UserId == AuthContent.Current.UserId &&
                 !x.Deleted && x.Id == id)
                 .Include(x => x.City)
                 .Include(x => x.District)
                 .Include(x => x.Neighborhood).FirstOrDefault();

            if (address != null)
            {
                var model = new UserAddressModel
                {
                    Id = address.Id,
                    Address = address.Address,
                    CityId = address.CityId,
                    CityName = address.City.Name,
                    DistrictId = address.DistrictId,
                    DistrictName = address.District.Name,
                    NameSurname = address.NameSurname,
                    NeighborhoodId = address.NeighborhoodId,
                    NeighborhoodName = address.Neighborhood.Name,
                    Phone = address.Phone,
                    PostCode = address.PostCode,
                    Title = address.Title
                };
                return model;
            }
            return null;
        }

        public ServiceResult Post(UserAddressModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            var entity = new UserAddress
            {
                Address = model.Address,
                CityId = model.CityId,
                UserId = AuthContent.Current.UserId,
                Deleted = false,
                DistrictId = model.DistrictId,
                NameSurname = model.NameSurname,
                NeighborhoodId = model.NeighborhoodId,
                Phone = model.Phone,
                PostCode = model.PostCode,
                Title = model.Title
            };
            _unitOfWork.Repository<UserAddress>().Add(entity);
            _unitOfWork.Save();

            return result;
        }

        public ServiceResult Put(UserAddressModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };

            var address = _unitOfWork.Repository<UserAddress>()
               .FirstOrDefault(x => x.UserId == AuthContent.Current.UserId && x.Id == model.Id && !x.Deleted);

            if (address != null)
            {
                address.Address = model.Address;
                address.CityId = model.CityId;
                address.DistrictId = model.DistrictId;
                address.NameSurname = model.NameSurname;
                address.NeighborhoodId = model.NeighborhoodId;
                address.Phone = model.Phone;
                address.PostCode = model.PostCode;
                address.Title = model.Title;

                _unitOfWork.Repository<UserAddress>().Update(address);
                _unitOfWork.Save();
            }
            else
            {
                result.Message = "Adres bulunamadı.";
                result.StatusCode = HttpStatusCode.NotFound;
            }
            return result;
        }

        public ServiceResult Delete(int id)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };

            var address = _unitOfWork.Repository<UserAddress>()
                .FirstOrDefault(x => x.UserId == AuthContent.Current.UserId && x.Id == id && !x.Deleted);
            if (address != null)
            {
                address.Deleted = true;
                _unitOfWork.Repository<UserAddress>().Update(address);
                _unitOfWork.Save();
            }
            else
            {
                result.Message = "Adres bulunamadı.";
                result.StatusCode = HttpStatusCode.NotFound;
            }
            return result;
        }
    }
}
