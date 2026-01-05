using System;
using RealEstate.Entity.Abstract;

namespace RealEstate.Entity.Concrete;

public class Inquiry : BaseClass
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Message { get; set; } = string.Empty;
    public InquiryStatus Status { get; set; } = InquiryStatus.YeniSorgu;

    // Relationships
    public int PropertyId { get; set; }
    public Property Property { get; set; } = null!;

    public int? UserId { get; set; }
    public AppUser? User { get; set; }
}

public enum InquiryStatus
{
    YeniSorgu,
    İletişimeGeçildi,
    Çözüldü,
    Kapatıldı
}