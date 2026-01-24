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
        }

        public async Task<ResponseDto<List<PropertyDto>>> GetAllAsync()
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                return ResponseDto<List<PropertyDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
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