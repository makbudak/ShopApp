using ShopApp.Data.GenericRepository;
using ShopApp.Extensions;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using ShopApp.Model.Enum;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Business.Services
{
    public interface ILookupService
    {
        List<City> GetCities();
        List<District> GetDistrictsByCityId(int cityId);
        List<Neighborhood> GetNeighborhoodsByDistrictId(int districtId);
        List<LookupModel> GetUserTypes();
        List<LookupModel> GetAdmins();
    }

    public class LookupService : ILookupService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LookupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<LookupModel> GetAdmins()
        {
            return _unitOfWork.Repository<User>()
               .Where(x => !x.Deleted && x.UserType != UserType.Customer).Select(x => new LookupModel
               {
                   Id = x.Id,
                   Name = $"{x.Name} {x.Surname}"
               }).ToList();
        }

        public List<City> GetCities()
        {
            return _unitOfWork.Repository<City>()
                .Where()
                .OrderBy(x => x.RowNumber).ToList();
        }

        public List<District> GetDistrictsByCityId(int cityId)
        {
            return _unitOfWork.Repository<District>()
                .Where(x => x.CityId == cityId).ToList();
        }

        public List<Neighborhood> GetNeighborhoodsByDistrictId(int districtId)
        {
            return _unitOfWork.Repository<Neighborhood>()
                .Where(x => x.DistrictId == districtId).ToList();
        }

        public List<LookupModel> GetUserTypes()
        {
            var list = EnumHelper.GetEnumValuesAndDescriptions<UserType>();
            return list;
        }
    }
}
