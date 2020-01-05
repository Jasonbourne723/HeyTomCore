using AutoMapper;
using HeyMacchiato.Application.Member.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.AutoMapper
{
	public class DOToDTOProfile : Profile
	{
		public DOToDTOProfile()
		{
			CreateMap<Domain.Member.Models.Member, MemberDTO>();
			CreateMap<Domain.Member.Models.Post, PostDTO>();
		}
	}
}
