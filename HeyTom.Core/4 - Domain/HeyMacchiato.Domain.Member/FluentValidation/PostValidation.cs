using FluentValidation;
using HeyMacchiato.Domain.Member.Command;

namespace HeyMacchiato.Domain.Member.FluentValidation
{
	public class PostValidation : AbstractValidator<PostCommand>
	{
		public PostValidation()
		{
			RuleFor(x => x.MemberId).NotEqual(0);
			RuleFor(x => x.Title).NotEmpty();
			RuleFor(x => x.Content).NotEmpty();
			RuleFor(x => x.Type).InclusiveBetween((short)0,(short)1);
		}
	}
}
