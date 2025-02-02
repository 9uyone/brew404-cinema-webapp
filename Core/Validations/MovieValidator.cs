using BusinessLogic.DTOs;
using FluentValidation;

namespace BusinessLogic.Validations
{
	public class MovieValidator : AbstractValidator<MovieDTO>
	{
		public MovieValidator()
		{
			RuleFor(movie => movie.Title)
				.NotEmpty().WithMessage("не може бути порожнім")
				.MaximumLength(100).WithMessage("не більше 100 символів");

			RuleFor(movie => movie.Overview)
				.NotEmpty().WithMessage("не може бути порожнім")
				.MaximumLength(500).WithMessage("не більше 500 символів");

			RuleFor(movie => movie.RunTime)
				.NotNull().WithMessage("Час виконання не може бути порожнім")
				.GreaterThan(0).WithMessage("Час виконання повинен бути більшим за нуль.");

			RuleFor(movie => movie.ImageUrl)
				.NotEmpty().WithMessage("не може бути порожнім")
				.MustBeValidUrl()
				.When(movie => !string.IsNullOrEmpty(movie.ImageUrl));

			RuleFor(movie => movie.BackgroundUrl)
				.NotEmpty().WithMessage("не може бути порожнім")
				.MustBeValidUrl()
				.When(movie => !string.IsNullOrEmpty(movie.BackgroundUrl));

			RuleFor(movie => movie.TrailerUrl)
				.NotEmpty().WithMessage("не може бути порожнім")
				.MustBeValidUrl()
				.When(movie => !string.IsNullOrEmpty(movie.TrailerUrl));

			RuleFor(moie => moie.ReleaseDate)
				.NotEmpty().WithMessage("не може бути порожнім");

		}
	}
}
