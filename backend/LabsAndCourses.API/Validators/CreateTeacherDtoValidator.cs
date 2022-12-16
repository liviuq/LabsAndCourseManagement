using FluentValidation;
using LabsAndCoursesManagement.API.DTOs;

namespace LabsAndCoursesManagement.API.Validators
{
    public class CreateTeacherDtoValidator : AbstractValidator<CreateTeacherDto>
    {
        public CreateTeacherDtoValidator()
        {
            RuleFor(item => item.Email)
                .EmailAddress();
            RuleFor(item => item.FirstName)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(item => item.LastName)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(item => item.TeachingDegree)
                .NotEmpty();
        }
    }
}
