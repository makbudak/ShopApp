using ShopApp.Data.GenericRepository;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Business.Services
{
    public interface ILookupService
    {
        List<City> GetCities();
        List<District> GetDistrictsByCityId(int cityId);
        List<Neighborhood> GetNeighborhoodsByDistrictId(int districtId);
    }

    public class LookupService : ILookupService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LookupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<City> GetCities()
        {
            return _unitOfWork.Repository<City>()
                .GetAll()
                .OrderBy(x => x.RowNumber).ToList();            
        }

        public List<District> GetDistrictsByCityId(int cityId)
        {
            return _unitOfWork.Repository<District>()
                .GetAll(x => x.CityId == cityId).ToList();
        }

        public List<Neighborhood> GetNeighborhoodsByDistrictId(int districtId)
        {
            return _unitOfWork.Repository<Neighborhood>()
                .GetAll(x => x.DistrictId == districtId).ToList();
        }
    }
}
