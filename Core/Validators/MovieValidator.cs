using BusinessLogic.DTOs;
using FluentValidation;

namespace BusinessLogic.Validations
{
	public class MovieValidator : AbstractValidator<MovieDTO>
	{
		public MovieValidator()
		{
			RuleFor(movie => movie.Title)
				.NotEmpty().WithMessage("Не може бути порожнім")
				.MaximumLength(100).WithMessage("Не більше 100 символів");

			RuleFor(movie => movie.Overview)
				.NotEmpty().WithMessage("Не може бути порожнім")
				.MaximumLength(500).WithMessage("Не більше 500 символів");

			RuleFor(movie => movie.RunTime)
				.GreaterThan(0).WithMessage("Час виконання повинен бути більшим за нуль.");

			RuleFor(movie => movie.ImageUrl)
				.NotEmpty().WithMessage("Не може бути порожнім")
				.MustBeValidUrl().WithMessage("Неправильний URL для фото")
				.When(movie => !string.IsNullOrEmpty(movie.ImageUrl));

			RuleFor(movie => movie.BackgroundUrl)
				.NotEmpty().WithMessage("Не може бути порожнім")
				.MustBeValidUrl().WithMessage("Неправильний URL для тла")
				.When(movie => !string.IsNullOrEmpty(movie.BackgroundUrl));

			RuleFor(movie => movie.TrailerUrl)
				.NotEmpty().WithMessage("Не може бути порожнім")
				.MustBeValidUrl().WithMessage("Неправильний URL для трейлера")
				.When(movie => !string.IsNullOrEmpty(movie.TrailerUrl));

			RuleFor(moie => moie.ReleaseDate)
				.NotEmpty().WithMessage("Не може бути порожнім");

		}
	}
}
