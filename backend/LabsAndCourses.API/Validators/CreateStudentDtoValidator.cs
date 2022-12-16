using FluentValidation;
using LabsAndCoursesManagement.API.DTOs;

namespace LabsAndCoursesManagement.API.Validators
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(item => item.Email)
                .EmailAddress();
            RuleFor(item => item.FirstName)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(item => item.LastName)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(item => item.Semester)
                .NotEmpty()
                .LessThanOrEqualTo(8);
            RuleFor(item => item.Group)
                .NotEmpty();
        }
}
}
