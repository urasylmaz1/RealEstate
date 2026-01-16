using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Business.DTOs;

public class PropertyCreateDto
{
    [Required(ErrorMessage = "İlan başlığı zorunludur!")]
    [MinLength(3, ErrorMessage = "İlan başlığı en az 3 karakter olmalıdır!")]
    [MaxLength(200, ErrorMessage = "İlan başlığı en fazla 200 karakter olabilir!")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "İlan açıklaması zorunludur!")]
    [MinLength(10, ErrorMessage = "İlan açıklaması en az 10 karakter olmalıdır!")]
    [MaxLength(5000, ErrorMessage = "İlan açıklaması en fazla 5000 karakter olabilir!")]
    public string? Description { get; set; } 

    [Required(ErrorMessage = "Fiyat zorunludur!")]
    [Range(0.01, 999999999, ErrorMessage = "Fiyat 0'dan büyük ve 999.999.999'dan küçük olmalıdır!")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Adres zorunludur!")]
    [MinLength(5, ErrorMessage = "Adres en az 5 karakter olmalıdır!")]
    [MaxLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir!")]
    public string? Address { get; set; } 

    [Required(ErrorMessage = "Şehir zorunludur!")]
    [MinLength(2, ErrorMessage = "Şehir en az 2 karakter olmalıdır!")]
    [MaxLength(100, ErrorMessage = "Şehir en fazla 100 karakter olabilir!")]
    public string? City { get; set; } 

    [MaxLength(100, ErrorMessage = "İlçe en fazla 100 karakter olabilir!")]
    public string? District { get; set; }

    [Required(ErrorMessage = "Oda sayısı zorunludur!")]
    [Range(1, 20, ErrorMessage = "Oda sayısı 1 ile 20 arasında olmalıdır!")]
    public int Rooms { get; set; }

    [Range(1, 10, ErrorMessage = "Banyo sayısı 1 ile 10 arasında olmalıdır!")]
    public int? Bathrooms { get; set; }

    [Required(ErrorMessage = "Alan zorunludur!")]
    [Range(0.01, 100000, ErrorMessage = "Alan 0'dan büyük ve 100.000'den küçük olmalıdır!")]
    public decimal Area { get; set; }

    [Required(ErrorMessage = "Kat zorunludur!")]
    [Range(-10, 100, ErrorMessage = "Kat -10 ile 100 arasında olmalıdır!")]
    public int Floor { get; set; }

    [Range(1, 200, ErrorMessage = "Toplam kat sayısı 1 ile 200 arasında olmalıdır!")]
    public int? TotalFloors { get; set; }

    [Required(ErrorMessage = "Yapım yılı zorunludur!")]
    [Range(1900, 2100, ErrorMessage = "Yapım yılı 1900 ile 2100 arasında olmalıdır!")]
    public int YearBuilt { get; set; }

    [Required(ErrorMessage = "Emlak tipi zorunludur!")]
    public PropertyTypeDto? PropertyType { get; set; }

    public ICollection<PropertyImageDto>? PropertyImages { get; set; }
}
