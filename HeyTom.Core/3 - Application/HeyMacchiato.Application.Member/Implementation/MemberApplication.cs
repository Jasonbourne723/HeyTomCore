using AutoMapper;
using HeyMacchiato.Application.Member.DTO;
using HeyMacchiato.Application.Member.Interface;
using HeyMacchiato.Domain.Member.Command;
using HeyMacchiato.Domain.Member.Repository;
using HeyMacchiato.Domain.Member.Service;
using HeyMacchiato.Infra.CsRedis;
using HeyMacchiato.Infra.Email;
using HeyMacchiato.Infra.Encryption.Md5;
using HeyMacchiato.Infra.Jwt;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace HeyMacchiato.Application.Member.Implementation
{
	public class MemberApplication : IMemberApplication
	{
		private readonly IMemberService _memberService;
		private readonly IMemberRepository _memberRepository;
		private readonly PermissionRequirement _permissionRequirement;
		private readonly IPostRepository _postRepository;
		private readonly IMapper _mapper;

		public MemberApplication(IMemberService memberService,
										  IMemberRepository memberRepository,
										  PermissionRequirement permissionRequirement,
										  IPostRepository postRepository,
										  IMapper mapper)
		{
			this._memberService = memberService;
			this._memberRepository = memberRepository;
			this._permissionRequirement = permissionRequirement;
			this._postRepository = postRepository;
			this._mapper = mapper;
		}

		/// <summary>
		/// 邮箱登录获取token
		/// </summary>
		/// <param name="emailLoginDTO"></param>
		/// <returns></returns>
		public TResultModel<TokenDTO> Login(EmailLoginDTO emailLoginDTO)
		{
			var md5Password = Md5Helper.GenerateMD5(emailLoginDTO.Password);
			var member =  _memberRepository.GetByEmail(emailLoginDTO.Email);
			if (member == null || md5Password != member.Password)
			{
				return new TResultModel<TokenDTO>(-1, "用户名或密码错误");
			}
			else
			{
				var jwtStr = JwtHelper.BuildJwtToken(new Claim[3]{
							new Claim(ClaimTypes.Role,"Admin"),
							new Claim("Name",member.NickName),
							new Claim("Id",member.Id.ToString())
						}, _permissionRequirement);
				return new TResultModel<TokenDTO>(1,"success")
				{
					TModel = new TokenDTO()
					{
						Token = jwtStr
					}
				};
			}
		}
		/// <summary>
		/// 邮箱注册
		/// </summary>
		/// <param name="registerDTO"></param>
		/// <returns></returns>
		public ResultModel Register(RegisterDTO registerDTO)
		{
			var key = $"VerificationCodel:Email:{registerDTO.Email}"; 
			var localVerificationCode = new CsRedisBase().Get(key);
			if (string.IsNullOrWhiteSpace(localVerificationCode))
			{
				return new ResultModel(-1,"未发送验证码或验证码已过期");
			}
			if (localVerificationCode != registerDTO.VerificationCode)
			{
				return new ResultModel(-1, "验证码错误");
			}
			registerDTO.Password = Md5Helper.GenerateMD5(registerDTO.Password);
			var registerCommand = _mapper.Map<RegisterCommand>(registerDTO);
			return _memberService.Register(registerCommand);
		}

		/// <summary>
		/// 发送注册验证码
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public ResultModel SendRegisterVerificaionCode(EmailDTO emailDTO)
		{
			var key = $"VerificationCodel:Email:{emailDTO.Email}";
			var toEmail = new List<string>() { emailDTO.Email };
			var random = new Random();
			var code = random.Next(1000, 10000);

			new CsRedisBase().set(key, code.ToString(), 120);

			var subject = "Hey Macchiato 注册验证码";
			var content = $"您的验证码是{code.ToString()},有效期120秒!";
			return EmailHelper.Send(toEmail, subject, content);
		}

		/// <summary>
		/// 获取会员信息
		/// </summary>
		/// <param name="emailDTO"></param>
		/// <returns></returns>
		public TResultModel<MemberDTO> GetMemberByEmail(string email)
		{
			var member = _memberRepository.GetByEmail(email);
			if (member != null)
			{
				return new TResultModel<MemberDTO>(1, "Success")
				{
					TModel = _mapper.Map<MemberDTO>(member)
				};
			}
			return new TResultModel<MemberDTO>(-1,"未找到会员信息");
		}
		/// <summary>
		/// 更新会员信息
		/// </summary>
		/// <param name="updateInfoDTO"></param>
		/// <returns></returns>
		public ResultModel UpdateMemberInfo(UpdateInfoDTO updateInfoDTO)
		{
			var updateInfoCommand = _mapper.Map<UpdateInfoCommand>(updateInfoDTO);
			return _memberService.UpdateInfo(updateInfoCommand);
		}

		/// <summary>
		/// 发帖
		/// </summary>
		/// <param name="createPostDTO"></param>
		/// <returns></returns>
		public ResultModel CreatePost(CreatePostDTO createPostDTO)
		{
			var postCommand = _mapper.Map<PostCommand>(createPostDTO);
			return _memberService.Post(postCommand);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="memberId"></param>
		/// <returns></returns>
		public TResultModel<List<PostDTO>> GetPost(int memberId)
		{
			var posts = _postRepository.GetByMemberId(memberId);
			return new TResultModel<List<PostDTO>>(1) {
				TModel = _mapper.Map<List<PostDTO>>(posts)
			};
		}
	}
}
