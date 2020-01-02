using AutoMapper;
using HeyMacchiato.Application.Member.ViewModel;
using HeyMacchiato.Domain.Member.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.AutoMapper
{
	public class VOToDTOProfile : Profile
	{
		public VOToDTOProfile()
		{
			CreateMap<RegisterViewModel, RegisterDTO>();
		}
	}
}
