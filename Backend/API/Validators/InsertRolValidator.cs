using API.Dto;
using FluentValidation;

namespace API.Validators
{
    public class InsertRolValidator : AbstractValidator<InsertRolCommandDto>
    {
        public InsertRolValidator()
        {
            RuleFor(r => r.RolId)
                .IsInEnum();
        }
    }
}
