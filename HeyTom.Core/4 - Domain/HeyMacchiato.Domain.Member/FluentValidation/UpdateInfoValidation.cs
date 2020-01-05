using FluentValidation;
using HeyMacchiato.Domain.Member.Command;

namespace HeyMacchiato.Domain.Member.FluentValidation
{
	public class UpdateInfoValidation : AbstractValidator<UpdateInfoCommand>
	{
		public UpdateInfoValidation()
		{
			RuleFor(x => x.Id).NotEqual(0);
			RuleFor(x => x.NickName).NotEmpty();
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Birthday).NotEmpty();
			RuleFor(x => x.Sex).InclusiveBetween((short)0,(short)1);
		}
	}
}
