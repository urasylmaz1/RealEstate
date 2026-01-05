using System;
using RealEstate.Entity.Abstract;

namespace RealEstate.Entity.Concrete;

public class AppUser : BaseClass
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; }
    public bool IsAgent { get; set; }
    public string? AgencyName { get; set; }
    public string? LicenseNumber { get; set; }

    // Relationships
    public ICollection<Property> Properties { get; set; } = new List<Property>();
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
    public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
}