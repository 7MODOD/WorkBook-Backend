using FluentValidation;
using WorkBook.DTOs;
using WorkBook.Models;
using WorkBook.util;

namespace WorkBook.Validators
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectReq>
    {
        public CreateProjectValidator()
        {
            RuleFor(p => p.title).NotEmpty();
            RuleFor(p => p.description).NotEmpty();
            RuleFor(p => p.start_date).NotEmpty();
            RuleFor(p => p.end_date).NotEmpty();
            RuleFor(p => p.duration_days).NotEmpty();
            RuleFor(p => p.address).NotEmpty();
            RuleFor(p=> p.price).NotEmpty();
            RuleFor(p => p.profession_id).NotEmpty();
            RuleFor(p => p.status).NotEmpty().Must(BeAValidStatus).WithMessage("Invalid Status value");
            RuleFor(p => p.worker_id).NotEmpty();

        }

        private bool BeAValidStatus(string statusAsString)
        {
            // Check if the string exists in the Status enum
            return Enum.IsDefined(typeof(Status), statusAsString);
        }
    }

    public class EditProjectValidator : AbstractValidator<EditProjectReq>
    {
        public EditProjectValidator()
        {
            RuleFor(p => p.title).NotEmpty();
            RuleFor(p => p.description).NotEmpty();
            RuleFor(p => p.start_date).NotEmpty();
            RuleFor(p => p.end_date).NotEmpty();
            RuleFor(p => p.duration_days).NotEmpty();
            RuleFor(p => p.address).NotEmpty();
            RuleFor(p => p.price).NotEmpty();
            RuleFor(p => p.profession_id).NotEmpty();
            RuleFor(p => p.status).NotEmpty().Must(BeAValidStatus).WithMessage("Invalid Status value");

        }
        private bool BeAValidStatus(string statusAsString)
        {
            // Check if the string exists in the Status enum
            return Enum.IsDefined(typeof(Status), statusAsString);
        }
    }

}
