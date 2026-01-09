using System;

namespace RealEstate.Business.DTOs;

public class PropertyImageDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsPrimary { get; set; }
    public int PropertyId { get; set; }
    public bool IsDeleted { get; set; }
}
