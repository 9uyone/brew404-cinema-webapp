using BusinessLogic.DTOs;
using FluentValidation;

namespace BusinessLogic.Validations
{
	public class GenreValidator : AbstractValidator<GenreDTO>
	{
		public GenreValidator()
		{
			RuleFor(genre => genre.Name)
				.NotEmpty().WithMessage("назва жарну не може бути порожнім")
				.MaximumLength(50).WithMessage("максимальна довжина - 50 символів");
		}
	}
}
