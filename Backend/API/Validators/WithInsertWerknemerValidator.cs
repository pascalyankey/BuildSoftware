using API.Dto;
using Domain.DataAccess;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace API.Validators
{
    public class WithInsertWerknemerValidator : AbstractValidator<WithInsertWerknemerDto>
    {
        public WithInsertWerknemerValidator()
        {
            RuleFor(w => w.Voornaam)
                .NotEmpty();

            RuleFor(w => w.Achternaam)
                .NotEmpty();

            RuleFor(w => w.RolId)
                .IsInEnum();
        }
    }
}
