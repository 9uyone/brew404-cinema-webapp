using BusinessLogic.DTOs.User;
using FluentValidation;

namespace BusinessLogic.Validators
{
	public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
	{
		public UpdateUserValidator()
		{
			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Ім'я користувача не може бути пустим")
				.MaximumLength(25).WithMessage("Ім'я користувача не може бути більше 25 символів");

			RuleFor(x => x.Email).EmailAddress().WithMessage("Некоректна пошта")
				.NotEmpty().WithMessage("Пошта не може бути пустою");

			RuleFor(x => x.PhoneNumber)
				.Matches(@"^\+?[0-9\-\(\)\s]+$").WithMessage("Номер телефону може містити тільки цифри, дефіси, дужки та пробіли")
				.MinimumLength(10).WithMessage("Номер телефону не може бути менше 10 символів")
				.MaximumLength(25).WithMessage("Номер телефону не може бути більше 25 символів");

			RuleFor(x => x.BirthDate)
				.GreaterThanOrEqualTo(new DateOnly(1900, 1, 1)).WithMessage("Дата народження не може бути менше 1900 року")
					.When(x => x.BirthDate.HasValue && x.BirthDate.Value != DateOnly.MinValue);

		}
	}
}
