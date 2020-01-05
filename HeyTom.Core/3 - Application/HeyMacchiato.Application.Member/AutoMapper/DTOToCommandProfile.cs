using AutoMapper;
using HeyMacchiato.Application.Member.DTO;
using HeyMacchiato.Domain.Member.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.AutoMapper
{
	public class DTOToCommandProfile : Profile
	{
		public DTOToCommandProfile()
		{
			CreateMap<RegisterDTO, RegisterCommand>();
			CreateMap<UpdateInfoDTO, UpdateInfoCommand>();
			CreateMap<CreatePostDTO, PostCommand>();
		}
	}
}
