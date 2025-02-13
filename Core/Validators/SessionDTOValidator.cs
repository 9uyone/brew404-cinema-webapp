using BusinessLogic.DTOs;
using FluentValidation;

namespace BusinessLogic.Validations
{
	public class SessionDTOValidator : AbstractValidator<SessionDTO>
	{
		public SessionDTOValidator()
		{
			RuleFor(dest => dest.StartTime).NotEmpty()
				.WithMessage("Початковий час не може бути порожнім")
				.GreaterThan(DateTime.Now).WithMessage("Дата та час застарілі");

			/*RuleFor(dest => dest.Price)
				.NotEmpty().WithMessage("Ціна не може бути порожньою")
				.GreaterThan(0).WithMessage("Ціна не може бути від'ємною");*/
		}
	}
}
