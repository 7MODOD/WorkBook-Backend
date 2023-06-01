using FluentValidation;
using WorkBook.DTOs;

namespace WorkBook.Validators
{
    public class RatingValidator:AbstractValidator<RateReq>
    {
        public RatingValidator()
        {
            RuleFor(r=>r.value).NotEmpty();
            RuleFor(r=>r.description).NotEmpty();
        }

    }
}
