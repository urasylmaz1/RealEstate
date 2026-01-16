using FluentValidation;
using RealEstate.Business.DTOs;

namespace RealEstate.Business.Validators;

public class InquiryUpdateDtoValidator : AbstractValidator<InquiryUpdateDto>
{
    public InquiryUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Sorgu ID'si 0'dan büyük olmalıdır!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad soyad zorunludur!")
            .MinimumLength(2).WithMessage("Ad soyad en az 2 karakter olmalıdır!")
            .MaximumLength(100).WithMessage("Ad soyad en fazla 100 karakter olabilir!");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta zorunludur!")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz!");

        RuleFor(x => x.Phone)
            .Matches(@"^\d{10,15}$").WithMessage("Telefon numarası 10-15 haneli rakam olmalıdır!")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Mesaj zorunludur!")
            .MinimumLength(10).WithMessage("Mesaj en az 10 karakter olmalıdır!")
            .MaximumLength(1000).WithMessage("Mesaj en fazla 1000 karakter olabilir!");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Geçerli bir sorgu durumu seçiniz!");

        RuleFor(x => x.PropertyId)
            .GreaterThan(0).WithMessage("İlan ID'si 0'dan büyük olmalıdır!");
    }
}