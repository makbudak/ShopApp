using Microsoft.EntityFrameworkCore;
using ShopApp.Data.GenericRepository;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.Customer;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShopApp.Business.Services
{
    public interface ICustomerAddressService
    {
        List<CustomerAddressModel> Get();
        CustomerAddressModel Get(int Id);
        ServiceResult Post(CustomerAddressModel model);
        ServiceResult Put(CustomerAddressModel model);
        ServiceResult Delete(int id);
    }

    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerAddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CustomerAddressModel> Get()
        {
            return _unitOfWork.Repository<CustomerAddress>()
                .GetAll(x => x.CustomerId == CustomerAuthContent.Current.CustomerId &&
                !x.Deleted)
                .Include(x => x.City)
                .Include(x => x.District)
                .Include(x => x.Neighborhood)
                .Select(x => new CustomerAddressModel
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

        public CustomerAddressModel Get(int id)
        {
            var address = _unitOfWork.Repository<CustomerAddress>()
                 .GetAll(x => x.CustomerId == CustomerAuthContent.Current.CustomerId &&
                 !x.Deleted && x.Id == id)
                 .Include(x => x.City)
                 .Include(x => x.District)
                 .Include(x => x.Neighborhood).FirstOrDefault();

            if (address != null)
            {
                var model = new CustomerAddressModel
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

        public ServiceResult Post(CustomerAddressModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };
            var entity = new CustomerAddress
            {
                Address = model.Address,
                CityId = model.CityId,
                CustomerId = CustomerAuthContent.Current.CustomerId,
                Deleted = false,
                DistrictId = model.DistrictId,
                NameSurname = model.NameSurname,
                NeighborhoodId = model.NeighborhoodId,
                Phone = model.Phone,
                PostCode = model.PostCode,
                Title = model.Title
            };
            _unitOfWork.Repository<CustomerAddress>().Add(entity);
            _unitOfWork.Save();

            return result;
        }

        public ServiceResult Put(CustomerAddressModel model)
        {
            var result = new ServiceResult { StatusCode = HttpStatusCode.OK };

            var address = _unitOfWork.Repository<CustomerAddress>()
               .Get(x => x.CustomerId == CustomerAuthContent.Current.CustomerId && x.Id == model.Id && !x.Deleted);

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

                _unitOfWork.Repository<CustomerAddress>().Update(address);
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

            var address = _unitOfWork.Repository<CustomerAddress>()
                .Get(x => x.CustomerId == CustomerAuthContent.Current.CustomerId && x.Id == id && !x.Deleted);
            if (address != null)
            {
                address.Deleted = true;
                _unitOfWork.Repository<CustomerAddress>().Update(address);
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
