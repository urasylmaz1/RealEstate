using System;
using Microsoft.AspNetCore.Identity;

namespace RealEstate.Entity.Concrete;

public class AppRole : IdentityRole
{
    public string? Description { get; set; }
}