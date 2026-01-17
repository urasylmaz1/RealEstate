using AutoMapper;
using LinqKit;
using RealEstate.Business.Abstract;
using RealEstate.Business.DTOs;
using RealEstate.Business.DTOs.ResponseDtos;
using RealEstate.Data.Abstract;
using RealEstate.Entity.Concrete;
using System.Linq.Expressions;

namespace RealEstate.Business.Concrete
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropertyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ResponseDto<PropertyDto>> CreateAsync(PropertyCreateDto propertyCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<NoContent>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<List<PropertyDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<PropertyDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<List<PropertyDto>>> GetFilteredAsync(PropertyFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<List<PropertyDto>>> GetMyPropertiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<PropertyDto>> UpdateAsync(int id, PropertyUpdateDto propertyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}