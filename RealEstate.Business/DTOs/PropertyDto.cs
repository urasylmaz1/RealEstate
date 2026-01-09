using System;
using RealEstate.Entity.Concrete;

namespace RealEstate.Business.DTOs;

public class PropertyDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; } 
    public decimal Price { get; set; }
    public string? Address { get; set; } 
    public string? City { get; set; } 
    public string? District { get; set; }
    public int Rooms { get; set; }
    public int? Bathrooms { get; set; }
    public decimal Area { get; set; }
    public int Floor { get; set; }
    public int? TotalFloors { get; set; }
    public int YearBuilt { get; set; }
    public PropertyStatus Status { get; set; }
    public bool IsDeleted { get; set; }
    public PropertyTypeDto? PropertyType { get; set; }
    public ICollection<PropertyImageDto>? PropertyImages { get; set; }
    
}
