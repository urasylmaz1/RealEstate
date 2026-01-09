using System;

namespace RealEstate.Business.DTOs;

public class AppUserDto
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePicture { get; set; }
    public bool IsAgent { get; set; }
    public string? AgencyName { get; set; }
    public string? LicenseNumber { get; set; }
}
