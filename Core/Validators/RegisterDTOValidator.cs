using BusinessLogic.DTOs.Auth;
using FluentValidation;

namespace BusinessLogic.Validations
{
	public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
	{
		public RegisterDTOValidator()
		{
			RuleFor(x => x.Password)
				.Equal(x => x.ConfirmPassword).WithMessage("Паролі не співпадають")
				.NotEmpty().WithMessage("Пароль не може бути пустим")
				.MinimumLength(6).WithMessage("Мінімальна довжина паролю 6 символів");

			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Ім'я користувача не може бути пустим")
				.MaximumLength(25).WithMessage("Ім'я користувача не може бути більше 25 символів");

			RuleFor(x => x.Email).EmailAddress().WithMessage("Некоректна пошта")
				.NotEmpty().WithMessage("Пошта не може бути пустою");
		}
	}
}
