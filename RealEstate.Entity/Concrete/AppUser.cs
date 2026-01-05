using System;
using Microsoft.AspNetCore.Identity;

namespace RealEstate.Entity.Concrete;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; }
    public bool IsAgent { get; set; }
    public string? AgencyName { get; set; }
    public string? LicenseNumber { get; set; }

    // Relationships
    public ICollection<Property> Properties { get; set; } = new List<Property>();
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}