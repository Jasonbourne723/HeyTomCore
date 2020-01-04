using FluentValidation;
using HeyMacchiato.Domain.Member.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Member.FluentValidation
{
	public class RegisterValidation : AbstractValidator<RegisterCommand>
	{
		public RegisterValidation()
		{
			ValidateEmail();
			ValidateNickName();
			ValidatePassword();
		}

		protected void ValidateNickName()
		{
			RuleFor(x => x.NickName).NotEmpty();
		}

		protected void ValidateEmail()
		{
			RuleFor(x => x.Email).NotEmpty();
			RuleFor(x => x.Email).EmailAddress();
		}

		protected void ValidatePassword()
		{
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
