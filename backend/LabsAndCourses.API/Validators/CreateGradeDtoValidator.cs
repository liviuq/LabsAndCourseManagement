using FluentValidation;
using LabsAndCoursesManagement.API.DTOs;

namespace LabsAndCoursesManagement.API.Validators
{
    public class CreateGradeDtoValidator : AbstractValidator<CreateGradeDto>
    {
        public CreateGradeDtoValidator()
        {
            RuleFor(item => item.Value)
                .NotEmpty()
                .LessThanOrEqualTo(10);
            RuleFor(item => item.GradeDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Grade Date can't be in the future.");
            RuleFor(item => item.IsLabGrade)
                .Must(x => x == false || x == true)
                .WithMessage("IsLabGrade must be true or false.");
            RuleFor(item => item.IsExamGrade)
                .Must(x => x == false || x == true)
                .WithMessage("IsExamGrade must be true or false.");
        }
    }
}
