using FluentValidation;
using WorkBook.DTOs;

namespace WorkBook.Validators
{
    
    public class EditWorkerValidator : AbstractValidator<EditWorkerReq>
    {
        public EditWorkerValidator()
        {
            RuleFor(w => w.first_name).NotEmpty();
            RuleFor(w => w.last_name).NotEmpty();
            RuleFor(w => w.date_of_birth).NotEmpty();
            RuleFor(w => w.profession_ids).NotEmpty();
            RuleFor(w => w.address_1).NotEmpty();

        }
    }




}
