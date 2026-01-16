using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Business.DTOs;

public class InquiryCreateDto
{
    [Required(ErrorMessage = "Ad soyad zorunludur!")]
    [MinLength(2, ErrorMessage = "Ad soyad en az 2 karakter olmalıdır!")]
    [MaxLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta zorunludur!")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz!")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz!")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Mesaj zorunludur!")]
    [MinLength(10, ErrorMessage = "Mesaj en az 10 karakter olmalıdır!")]
    [MaxLength(1000, ErrorMessage = "Mesaj en fazla 1000 karakter olabilir!")]
    public string Message { get; set; } = string.Empty;

    [Required(ErrorMessage = "İlan ID'si zorunludur!")]
    [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ilan ID'si giriniz!")]
    public int PropertyId { get; set; }
}