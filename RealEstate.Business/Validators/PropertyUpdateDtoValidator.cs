using FluentValidation;
using RealEstate.Business.DTOs;

namespace RealEstate.Business.Validators;

public class PropertyUpdateDtoValidator : AbstractValidator<PropertyUpdateDto>
{
    public PropertyUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("İlan ID'si 0'dan büyük olmalıdır!");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("İlan başlığı zorunludur!")
            .MinimumLength(3).WithMessage("İlan başlığı en az 3 karakter olmalıdır!")
            .MaximumLength(200).WithMessage("İlan başlığı en fazla 200 karakter olabilir!");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("İlan açıklaması zorunludur!")
            .MinimumLength(10).WithMessage("İlan açıklaması en az 10 karakter olmalıdır!")
            .MaximumLength(5000).WithMessage("İlan açıklaması en fazla 5000 karakter olabilir!");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır!")
            .LessThanOrEqualTo(999999999).WithMessage("Fiyat 999.999.999'dan büyük olamaz!");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres zorunludur!")
            .MinimumLength(5).WithMessage("Adres en az 5 karakter olmalıdır!")
            .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir!");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir zorunludur!")
            .MinimumLength(2).WithMessage("Şehir en az 2 karakter olmalıdır!")
            .MaximumLength(100).WithMessage("Şehir en fazla 100 karakter olabilir!");

        RuleFor(x => x.District)
            .MaximumLength(100).WithMessage("İlçe en fazla 100 karakter olabilir!");

        RuleFor(x => x.Rooms)
            .GreaterThan(0).WithMessage("Oda sayısı 0'dan büyük olmalıdır!")
            .LessThanOrEqualTo(20).WithMessage("Oda sayısı 20'den büyük olamaz!");

        RuleFor(x => x.Bathrooms)
            .GreaterThan(0).WithMessage("Banyo sayısı 0'dan büyük olmalıdır!")
            .LessThanOrEqualTo(10).WithMessage("Banyo sayısı 10'dan büyük olamaz!")
            .When(x => x.Bathrooms.HasValue);

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Alan 0'dan büyük olmalıdır!")
            .LessThanOrEqualTo(100000).WithMessage("Alan 100.000'den büyük olamaz!");

        RuleFor(x => x.Floor)
            .InclusiveBetween(-10, 100).WithMessage("Kat -10 ile 100 arasında olmalıdır!");

        RuleFor(x => x.TotalFloors)
            .GreaterThan(0).WithMessage("Toplam kat sayısı 0'dan büyük olmalıdır!")
            .LessThanOrEqualTo(200).WithMessage("Toplam kat sayısı 200'den büyük olamaz!")
            .When(x => x.TotalFloors.HasValue);

        RuleFor(x => x.YearBuilt)
            .InclusiveBetween(1900, 2100).WithMessage("Yapım yılı 1900 ile 2100 arasında olmalıdır!");

        RuleFor(x => x.PropertyType)
            .NotNull().WithMessage("Emlak tipi zorunludur!");

        RuleFor(x => x.PropertyImages)
            .NotNull().WithMessage("Resimler listesi null olamaz!")
            .When(x => x.PropertyImages != null);
    }
}