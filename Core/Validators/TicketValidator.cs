using FluentValidation;
using BusinessLogic.DTOs;

namespace WebApp.Validations
{
	public class TicketValidator : AbstractValidator<TicketDTO>
	{
		public TicketValidator()
		{
			RuleFor(t => t.UserId)
				.NotNull().WithMessage("UserId не може бути пустим");

			RuleFor(t => t.SeatId)
				.GreaterThan(0).WithMessage("SeatId має бути більше 0");

			RuleFor(t => t.SessionId)
				.GreaterThan(0).WithMessage("SessionId має бути більше 0");

			/*RuleFor(t => t.PurchaseTime)
				.NotNull().WithMessage("PurchaseTime не може бути пустим")
				.LessThanOrEqualTo(DateTime.UtcNow.ToLocalTime()).WithMessage("PurchaseTime має бути менше поточного часу");*/
		}
	}
}
