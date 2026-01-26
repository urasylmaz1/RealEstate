using System.Linq.Expressions;
using RealEstate.Business.DTOs;
using RealEstate.Business.DTOs.ResponseDtos;
using RealEstate.Entity.Concrete;

namespace RealEstate.Business.Abstract
{
    public interface IPropertyService
    {
        Task<ResponseDto<PropertyDto>> GetAsync(int id);
        Task<ResponseDto<IEnumerable<PropertyDto>>> GetAllAsync(
            Expression<Func<Property, bool>>? predicate,
            Func<IQueryable<Property>, IOrderedQueryable<Property>>? orderBy,
            bool? isDeleted = null);
        Task<ResponseDto<PagedResultDto<PropertyDto>>> GetAllPagedAsync(
            PaginationQueryDto paginationQueryDto,
            Expression<Func<Property, bool>>? predicate = null,
            Func<IQueryable<Property>, IOrderedQueryable<Property>>? orderBy = null,
            bool? isDeleted = null);
 
        Task<ResponseDto<PropertyDto>> CreateAsync(PropertyCreateDto propertyCreateDto);
        Task<ResponseDto<PropertyDto>> UpdateAsync(int id, PropertyUpdateDto propertyUpdateDto);
        Task<ResponseDto<NoContent>> HardDeleteAsync(int id);
        Task<ResponseDto<NoContent>> SoftDeleteAsync(int id);
    }
}