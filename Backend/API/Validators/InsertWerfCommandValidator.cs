using API.Dto;
using FluentValidation;

namespace API.Validators
{
    public class InsertWerfCommandValidator : AbstractValidator<InsertWerfCommandDto>
    {
        public InsertWerfCommandValidator()
        {
            RuleFor(w => w.Naam)
                .NotEmpty();

            RuleFor(w => w.StartDatum)
                .NotEmpty();

            RuleFor(w => w.EindDatum)
                .Must(IsGeldigDatum)
                .WithMessage("Eind datum mag niet kleiner zijn dan vandaag.");

            RuleFor(w => w.Status)
                .IsInEnum();

            RuleFor(w => w.Werknemers)
                .Must(ManagerBevatten);

            RuleForEach(w => w.Werknemers)
                .SetValidator(new WithInsertWerknemerValidator());
        }

        private bool IsGeldigDatum(DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }

        public bool ManagerBevatten(List<WithInsertWerknemerDto> werknemers)
        {
            return werknemers.Any(w => w.RolId == Domain.Enums.RolType.Manager);
        }
    }
}
