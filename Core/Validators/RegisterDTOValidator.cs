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
				.NotEmpty().WithMessage("Пароль не може бути пустим");

			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Ім'я користувача не може бути пустим")
				.MaximumLength(25).WithMessage("Ім'я користувача не може бути більше 25 символів");
		}
	}
}
