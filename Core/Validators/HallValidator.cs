using BusinessLogic.DTOs;
using FluentValidation;

namespace BusinessLogic.Validations
{
	public class HallValidator : AbstractValidator<HallDTO>
	{
		public HallValidator()
		{
			RuleFor(hall => hall.Name)
				.NotEmpty().WithMessage("Не може бути порожнім")
				.MaximumLength(20).WithMessage("Не більше 20 символів");

			RuleFor(hall => hall.NumbOfRows)
				.InclusiveBetween(1, 20).WithMessage("Кількість рядів повинна бути від 1 до 20.");
			
			RuleFor(hall => hall.NumbOfRows)
				.InclusiveBetween(1, 20).WithMessage("Кількість місць в ряді повинна бути від 1 до 20.");
		}
	}
}
