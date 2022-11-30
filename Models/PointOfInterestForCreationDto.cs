using FluentValidation;

namespace FirstApiProject.Models
{
    public class PointOfInterestForCreationDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
    public sealed class PointOfInterestValidator : AbstractValidator<PointOfInterestForCreationDto>
    {
        public PointOfInterestValidator()
        {
            RuleFor(point => point.Name)
                .NotEmpty()
                .WithMessage("نوشتن نام اجباری است.")
                .MaximumLength(20)
                .WithMessage("نام حداکثر باید 20 کاراکتر باشد.");
            RuleFor(point => point.Description)
                .MaximumLength(200)
                .WithMessage("توضیحات باید حداکثر 200 کاراکتر باشد.");
        }
    }
}
