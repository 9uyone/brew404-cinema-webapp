using FluentValidation;

namespace BusinessLogic.Validations
{
	public static class CustomUrlValidator
	{
		public static IRuleBuilderOptions<T, string?> MustBeValidUrl<T>(this IRuleBuilder<T, string?> ruleBuilder)
		{
			return ruleBuilder.Must(isUrl).WithMessage("не вірний URL");
		}

		private static bool isUrl(string? link)
		{
			if (string.IsNullOrWhiteSpace(link))
			{
				return false;
			}

			Uri? outUri;

			return Uri.TryCreate(link, UriKind.Absolute, out outUri)
				   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
		}
	}
}
