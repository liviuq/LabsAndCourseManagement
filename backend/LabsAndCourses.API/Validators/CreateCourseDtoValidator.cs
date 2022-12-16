using FluentValidation;
using LabsAndCoursesManagement.API.DTOs;

namespace LabsAndCoursesManagement.API.Validators
{
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseDtoValidator()
        {
            RuleFor(item => item.Title).NotEmpty();
            RuleFor(item => item.Semester)
                .NotEmpty()
                .LessThanOrEqualTo(8);
            RuleFor(item => item.Credits)
                .NotEmpty()
                .LessThanOrEqualTo(6);
        }
    }
}
