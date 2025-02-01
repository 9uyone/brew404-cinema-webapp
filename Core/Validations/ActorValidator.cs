using BusinessLogic.DTOs;
using FluentValidation;

namespace BusinessLogic.Validations
{
	public class ActorValidator : AbstractValidator<ActorDTO>
	{
		public ActorValidator()
		{
			RuleFor(actor => actor.Name)
				.NotEmpty().WithMessage("Ім'я не може бути порожнім")
				.MaximumLength(50).WithMessage("Максимальна довжина імені - 50 символів");

			RuleFor(actor => actor.AvatarUrl)
				.NotEmpty().WithMessage("URL не може бути порожнім")
				.MustBeValidUrl()
				.When(actor => !string.IsNullOrEmpty(actor.AvatarUrl));
		}
	}
}
