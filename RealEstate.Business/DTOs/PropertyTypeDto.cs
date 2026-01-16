using System;

namespace RealEstate.Business.DTOs;

public class PropertyTypeDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
}
