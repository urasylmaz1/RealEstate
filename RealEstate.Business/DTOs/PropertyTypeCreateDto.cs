using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Business.DTOs;

public class PropertyTypeCreateDto
{
    [Required(ErrorMessage = "Emlak tipi adı zorunludur!")]
    [MinLength(3, ErrorMessage = "Emlak tipi adı en az 3 karakter olmalıdır!")]
    public string? Name { get; set; }

    public string? Description { get; set; }
}
