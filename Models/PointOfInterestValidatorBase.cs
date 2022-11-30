using FluentValidation;

namespace FirstApiProject.Models
{
    public abstract class PointOfInterestValidatorBase<T> : AbstractValidator<T>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
