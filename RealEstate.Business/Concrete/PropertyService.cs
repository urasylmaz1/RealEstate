using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Http;
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
        private readonly IRepository<PropertyType> _propertyTypeRepository;
        private readonly IRepository<Property> _propertyRepository;

        public PropertyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _propertyRepository = unitOfWork.GetRepository<Property>();
            _propertyTypeRepository = unitOfWork.GetRepository<PropertyType>();
            _mapper = mapper;
        }

        public async Task<ResponseDto<PropertyDto>> CreateAsync(PropertyCreateDto propertyCreateDto)
        {
            try
            {
                if(propertyCreateDto.PropertyType is null)
                {
                    return ResponseDto<PropertyDto>.Fail("Emlak tipi belirtilmeli!", StatusCodes.Status400BadRequest);
                }
                //property type database'de var mı kontrol et
                var propertyType = await _propertyTypeRepository.GetAsync(pt => pt.Id == propertyCreateDto.PropertyType.Id);
                if(propertyType is null)
                {
                    return ResponseDto<PropertyDto>.Fail("Belirtilen emlak tipi bulunamadı!", StatusCodes.Status400BadRequest);
                }
                var property = _mapper.Map<Property>(propertyCreateDto);
                property.PropertyTypeId = propertyType.Id;
                await _propertyRepository.AddAsync(property);
                var result =await _unitOfWork.SaveAsync();
                if (result < 1)
                {
                    return ResponseDto<PropertyDto>.Fail("Emlak oluşturulamadı!", StatusCodes.Status500InternalServerError);
                }
                return ResponseDto<PropertyDto>.Success(_mapper.Map<PropertyDto>(property), StatusCodes.Status201Created);

            }
                
            catch (Exception ex)
        {
            return ResponseDto<PropertyDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
        }
        public async Task<ResponseDto<NoContent>> HardDeleteAsync(int id)
        {
            try
            {
                var deletedProperty = await _propertyRepository.GetAsync(
                    predicate: p => p.Id == id,
                    showIsDeleted: true);

                if(deletedProperty is null)
                {
                    return ResponseDto<NoContent>.Fail("Emlak bulunamadı!", StatusCodes.Status404NotFound);
                }
                _propertyRepository.Remove(deletedProperty);
                var result = await _unitOfWork.SaveAsync();
                if (result < 1)
                {
                    return ResponseDto<NoContent>.Fail("Emlak silinemedi!", StatusCodes.Status500InternalServerError);
                }
                return ResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                
                return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<ResponseDto<NoContent>> SoftDeleteAsync(int id)
        {
            try
            {
                var deletedProperty = await _propertyRepository.GetAsync(
                    predicate: p => p.Id == id,
                    showIsDeleted: true);
                if (deletedProperty is null)
                {
                    return ResponseDto<NoContent>.Fail("Emlak bulunamadı!", StatusCodes.Status404NotFound);
                }
                deletedProperty.IsDeleted = !deletedProperty.IsDeleted;
                deletedProperty.UpdatedAt = DateTime.UtcNow;
                _propertyRepository.Update(deletedProperty);
                var result = await _unitOfWork.SaveAsync();
                if (result < 1)
                {
                    return ResponseDto<NoContent>.Fail("Emlak silinemedi!", StatusCodes.Status500InternalServerError);
                }
                return ResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {

                return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<PropertyDto>> GetAsync(int id)
        {
            try
            {
                var property = await _propertyRepository.GetAsync(
                predicate: x => x.Id == id,
                asExpanded: true);
                if (property is null)
                {
                    return ResponseDto<PropertyDto>.Fail("Emlak bulunamadı!", StatusCodes.Status404NotFound);
                }
                var propertyDto = _mapper.Map<PropertyDto>(property);
                return ResponseDto<PropertyDto>.Success(propertyDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {

                return ResponseDto<PropertyDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<ResponseDto<IEnumerable<PropertyDto>>> GetAllAsync(
            Expression<Func<Property, bool>>? predicate, 
            Func<IQueryable<Property>, IOrderedQueryable<Property>>? orderBy, 
            bool? isDeleted = null)
        {
            try
            {
                if (predicate is null)
                {
                    predicate = PredicateBuilder.New<Property>(true);
                }
                if (isDeleted.HasValue)
                {
                    predicate = predicate.And(x => x.IsDeleted == isDeleted);
                }
                var properties = await _propertyRepository.GetAllAsync(
                    predicate: predicate,
                    orderBy: orderBy!,
                    asExpanded: true);
                if (properties is null || !properties.Any())
                {
                    return ResponseDto<IEnumerable<PropertyDto>>.Fail("Hiç emlak bulunamadı!", StatusCodes.Status404NotFound);
                }
                var propertyDtos = _mapper.Map<IEnumerable<PropertyDto>>(properties);
                return ResponseDto<IEnumerable<PropertyDto>>.Success(propertyDtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {

                return ResponseDto<IEnumerable<PropertyDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<PagedResultDto<PropertyDto>>> GetAllPagedAsync(PaginationQueryDto paginationQueryDto, Expression<Func<Property, bool>>? predicate = null, Func<IQueryable<Property>, IOrderedQueryable<Property>>? orderBy = null, bool? isDeleted = null)
        {
            try
            {
                if (predicate is null)
                {
                    predicate = PredicateBuilder.New<Property>(true);
                }
                if (isDeleted.HasValue)
                {
                    predicate = predicate.And(x => x.IsDeleted == isDeleted);
                }

                var (properties, totalCount) = await _propertyRepository.GetPagedAsync(
                    predicate: predicate,
                    orderBy: orderBy,
                    skip: paginationQueryDto.Skip,
                    take: paginationQueryDto.Take,
                    showIsDeleted: isDeleted ?? false,
                    asExpanded: true);

                if (properties is null || !properties.Any())
                {
                    var emptyResult = PagedResultDto<PropertyDto>.Create(
                        new List<PropertyDto>(),
                        paginationQueryDto.PageNumber,
                        paginationQueryDto.PageSize,
                        0);
                    return ResponseDto<PagedResultDto<PropertyDto>>.Success(emptyResult, StatusCodes.Status200OK);
                }

                var propertyDtos = _mapper.Map<IEnumerable<PropertyDto>>(properties);
                var pagedResult = PagedResultDto<PropertyDto>.Create(
                    propertyDtos,
                    paginationQueryDto.PageNumber,
                    paginationQueryDto.PageSize,
                    totalCount);

                return ResponseDto<PagedResultDto<PropertyDto>>.Success(pagedResult, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<PagedResultDto<PropertyDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<ResponseDto<PropertyDto>> UpdateAsync(int id, PropertyUpdateDto propertyUpdateDto)
        {
            try
            {
                // Check if the property exists
                var existingProperty = await _propertyRepository.GetAsync(
                    predicate: p => p.Id == id,
                    showIsDeleted: true,
                    asExpanded: true);

                if (existingProperty is null)
                {
                    return ResponseDto<PropertyDto>.Fail("Emlak bulunamadı!", StatusCodes.Status404NotFound);
                }

                // Validate property type if provided
                if (propertyUpdateDto.PropertyType is not null)
                {
                    var propertyType = await _propertyTypeRepository.GetAsync(pt => pt.Id == propertyUpdateDto.PropertyType.Id);
                    if (propertyType is null)
                    {
                        return ResponseDto<PropertyDto>.Fail("Belirtilen emlak tipi bulunamadı!", StatusCodes.Status400BadRequest);
                    }
                    existingProperty.PropertyTypeId = propertyType.Id;
                }

                // Update the properties
                existingProperty.Title = propertyUpdateDto.Title ?? existingProperty.Title;
                existingProperty.Description = propertyUpdateDto.Description ?? existingProperty.Description;
                existingProperty.Price = propertyUpdateDto.Price;
                existingProperty.Address = propertyUpdateDto.Address ?? existingProperty.Address;
                existingProperty.City = propertyUpdateDto.City ?? existingProperty.City;
                existingProperty.District = propertyUpdateDto.District ?? existingProperty.District;
                existingProperty.Rooms = propertyUpdateDto.Rooms;
                existingProperty.Bathrooms = propertyUpdateDto.Bathrooms ?? existingProperty.Bathrooms;
                existingProperty.Area = propertyUpdateDto.Area;
                existingProperty.Floor = propertyUpdateDto.Floor;
                existingProperty.TotalFloors = propertyUpdateDto.TotalFloors ?? existingProperty.TotalFloors;
                existingProperty.YearBuilt = propertyUpdateDto.YearBuilt;
                existingProperty.UpdatedAt = DateTime.UtcNow;

                // Update property images if provided
                if (propertyUpdateDto.PropertyImages is not null && propertyUpdateDto.PropertyImages.Any())
                {
                    // Clear existing images and add new ones
                    existingProperty.Images?.Clear();
                    foreach (var imageDto in propertyUpdateDto.PropertyImages)
                    {
                        existingProperty.Images?.Add(_mapper.Map<PropertyImage>(imageDto));
                    }
                }

                _propertyRepository.Update(existingProperty);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<PropertyDto>.Fail("Emlak güncellenemedi!", StatusCodes.Status500InternalServerError);
                }

                // Get the updated property with expanded data
                var updatedProperty = await _propertyRepository.GetAsync(
                    predicate: p => p.Id == id,
                    asExpanded: true);

                var propertyDto = _mapper.Map<PropertyDto>(updatedProperty);
                return ResponseDto<PropertyDto>.Success(propertyDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<PropertyDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

    }
}