using System;
using RealEstate.Entity.Concrete;

namespace RealEstate.Business.DTOs;

public class InquiryDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Message { get; set; } = string.Empty;
    public InquiryStatus Status { get; set; }
    public int PropertyId { get; set; }
    public string? UserId { get; set; }
    public bool IsDeleted { get; set; }
}
