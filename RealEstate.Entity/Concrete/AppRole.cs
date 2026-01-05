using System;
using RealEstate.Entity.Abstract;

namespace RealEstate.Entity.Concrete;

public class AppRole : BaseClass
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Relationships
    public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
}

public class AppUserRole : BaseClass
{
    public int UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public int RoleId { get; set; }
    public AppRole Role { get; set; } = null!;
}