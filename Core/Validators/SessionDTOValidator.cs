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
		}
	}
}
