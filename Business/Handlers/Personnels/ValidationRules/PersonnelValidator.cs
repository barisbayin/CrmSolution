
using Business.Handlers.Personnels.Commands;
using FluentValidation;

namespace Business.Handlers.Personnels.ValidationRules
{

	public class CreatePersonnelValidator : AbstractValidator<CreatePersonnelCommand>
	{
		public CreatePersonnelValidator()
		{
			RuleFor(x => x.LastName).NotEmpty();
			RuleFor(x => x.BirthDay).NotEmpty();
			RuleFor(x => x.IdentityNumber).NotEmpty();
			RuleFor(x => x.PhoneNumber).NotEmpty();
			RuleFor(x => x.Email).NotEmpty();
			RuleFor(x => x.FullAddress).NotEmpty();
			RuleFor(x => x.AddressLine).NotEmpty();
			RuleFor(x => x.ZipCode).NotEmpty();
			RuleFor(x => x.ImagePath).NotEmpty();
			RuleFor(x => x.IsUser).NotEmpty();

		}
	}
	public class UpdatePersonnelValidator : AbstractValidator<UpdatePersonnelCommand>
	{
		public UpdatePersonnelValidator()
		{
			RuleFor(x => x.LastName).NotEmpty();
			RuleFor(x => x.BirthDay).NotEmpty();
			RuleFor(x => x.IdentityNumber).NotEmpty();
			RuleFor(x => x.PhoneNumber).NotEmpty();
			RuleFor(x => x.Email).NotEmpty();
			RuleFor(x => x.FullAddress).NotEmpty();
			RuleFor(x => x.AddressLine).NotEmpty();
			RuleFor(x => x.ZipCode).NotEmpty();
			RuleFor(x => x.ImagePath).NotEmpty();
			RuleFor(x => x.IsUser).NotEmpty();

		}
	}
}