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
                .NotNull();
            RuleFor(item => item.IsExamGrade)
                .NotNull();
        }
    }
}
