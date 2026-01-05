using System;
using RealEstate.Entity.Abstract;

namespace RealEstate.Entity.Concrete;

public class Property : BaseClass
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? District { get; set; }
    public int Rooms { get; set; }
    public int? Bathrooms { get; set; }
    public decimal Area { get; set; }
    public int Floor { get; set; }
    public int? TotalFloors { get; set; }
    public int YearBuilt { get; set; }
    public PropertyStatus Status { get; set; } = PropertyStatus.Müsait;

    // Relationships
    public int PropertyTypeId { get; set; }
    public PropertyType PropertyType { get; set; } = null!;

    public int AgentId { get; set; }
    public AppUser Agent { get; set; } = null!;

    public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}

public enum PropertyStatus
{
    Müsait,
    Rezerve,
    Satıldı,
    Kiralandı
}
