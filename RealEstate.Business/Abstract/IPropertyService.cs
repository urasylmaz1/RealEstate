using RealEstate.Business.DTOs;
using RealEstate.Business.DTOs.ResponseDtos;
using RealEstate.Entity.Concrete;

namespace RealEstate.Business.Abstract
{
    public interface IPropertyService
    {
        Task<ResponseDto<List<PropertyDto>>> GetAllAsync();
        Task<ResponseDto<PropertyDto>> GetByIdAsync(int id);
        Task<ResponseDto<PropertyDto>> CreateAsync(PropertyCreateDto propertyCreateDto);
        Task<ResponseDto<PropertyDto>> UpdateAsync(int id, PropertyUpdateDto propertyUpdateDto);
        Task<ResponseDto<NoContent>> DeleteAsync(int id);
        Task<ResponseDto<List<PropertyDto>>> GetMyPropertiesAsync();
        Task<ResponseDto<List<PropertyDto>>> GetFilteredAsync(PropertyFilterDto filter);
    }
}