using FluentValidation;
using WorkBook.DTOs;
using WorkBook.Models;

namespace WorkBook.Validators
{
    public class WorkerSignUpValidator : AbstractValidator<WorkersModelReq>
    {
        public WorkerSignUpValidator()
        {
            RuleFor(w => w.first_name).NotEmpty();
            RuleFor(w => w.last_name).NotEmpty();
            RuleFor(w => w.email).NotEmpty().EmailAddress();
            RuleFor(w => w.password).NotEmpty().MinimumLength(1);
            RuleFor(w => w.phone).NotEmpty().Matches(@"^[0-9]*$");
            RuleFor(w => w.address_1).NotEmpty();
            RuleFor(w => w.date_of_birth).NotEmpty();
            RuleFor(w => w.profession_ids).NotEmpty();

        }
    }

    public class CustomerSignUpValidator : AbstractValidator<CustomerModelReq>
    {
        public CustomerSignUpValidator()
        {
            RuleFor(w => w.first_name).NotEmpty();
            RuleFor(w => w.last_name).NotEmpty();
            RuleFor(w => w.email).NotEmpty().EmailAddress();
            RuleFor(w => w.password).NotEmpty().MinimumLength(1);
            RuleFor(w => w.phone).NotEmpty().Matches(@"^[0-9]*$");
            RuleFor(w => w.address_1).NotEmpty();
            RuleFor(w=>w.date_of_birth).NotEmpty();

        }
    }

    public class SignInValidator : AbstractValidator<SignInReq>
    {
        public SignInValidator()
        {
            
            RuleFor(w => w.email).NotEmpty().EmailAddress();
            RuleFor(w => w.password).NotEmpty().MinimumLength(1);

        }
    }

    
}
