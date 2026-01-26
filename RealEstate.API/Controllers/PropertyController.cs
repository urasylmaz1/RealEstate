using Microsoft.AspNetCore.Mvc;
using RealEstate.Business.Abstract;
using RealEstate.Business.DTOs;
using RealEstate.Business.DTOs.ResponseDtos;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    /// <summary>
    /// Gets a property by ID
    /// </summary>
    /// <param name="id">Property ID</param>
    /// <returns>Property details</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ResponseDto<PropertyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<PropertyDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _propertyService.GetAsync(id);

        if (result.StatusCode == StatusCodes.Status404NotFound)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all properties with optional pagination
    /// </summary>
    /// <param name="paginationQueryDto">Pagination parameters</param>
    /// <returns>Paged list of properties</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDto<PagedResultDto<PropertyDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPaged([FromQuery] PaginationQueryDto paginationQueryDto)
    {
        var result = await _propertyService.GetAllPagedAsync(paginationQueryDto);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new property
    /// </summary>
    /// <param name="propertyCreateDto">Property creation data</param>
    /// <returns>Created property</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto<PropertyDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDto<PropertyDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] PropertyCreateDto propertyCreateDto)
    {
        var result = await _propertyService.CreateAsync(propertyCreateDto);

        if (result.StatusCode == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    /// <summary>
    /// Updates an existing property
    /// </summary>
    /// <param name="id">Property ID</param>
    /// <param name="propertyUpdateDto">Property update data</param>
    /// <returns>Updated property</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ResponseDto<PropertyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<PropertyDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseDto<PropertyDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] PropertyUpdateDto propertyUpdateDto)
    {
        var result = await _propertyService.UpdateAsync(id, propertyUpdateDto);

        if (result.StatusCode == StatusCodes.Status404NotFound)
        {
            return NotFound(result);
        }

        if (result.StatusCode == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Soft deletes a property (marks as deleted)
    /// </summary>
    /// <param name="id">Property ID</param>
    /// <returns>Delete result</returns>
    [HttpDelete("{id:int}/soft")]
    [ProducesResponseType(typeof(ResponseDto<NoContent>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseDto<NoContent>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var result = await _propertyService.SoftDeleteAsync(id);

        if (result.StatusCode == StatusCodes.Status404NotFound)
        {
            return NotFound(result);
        }

        return NoContent();
    }

    /// <summary>
    /// Hard deletes a property (permanently removes)
    /// </summary>
    /// <param name="id">Property ID</param>
    /// <returns>Delete result</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ResponseDto<NoContent>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseDto<NoContent>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> HardDelete(int id)
    {
        var result = await _propertyService.HardDeleteAsync(id);

        if (result.StatusCode == StatusCodes.Status404NotFound)
        {
            return NotFound(result);
        }

        return NoContent();
    }
}