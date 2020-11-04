using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.Manage.Apps.Models;
using HeyTom.Manage.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeyMacchiato.Service.Manage.Apps.Controllers
{

    [Route("api/[controller]")]
    public class MenuController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuRepository _subMenuRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMenuRepository _roleSubMenuRepository;

        public MenuController(IUserRepository userRepository,
                              IMenuRepository menuRepository,
                              IMenuRepository subMenuRepository,
                              IUserRoleRepository userRoleRepository,
                              IRoleRepository roleRepository,
                              IRoleMenuRepository roleSubMenuRepository)
        {
            _userRepository = userRepository;
            _menuRepository = menuRepository;
            _subMenuRepository = subMenuRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _roleSubMenuRepository = roleSubMenuRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<List<MenuVModel>>))]
        public IActionResult GetByEmail([FromBody] EmailVModel model)
        {
            var result = new TResultModel<List<MenuVModel>>(1);
            return this.Wrapper(ref result, () =>
            {

                var user = _userRepository.GetByEmail(model.email);
                if (user == null)
                {
                    result.ResultNo = -2;
                    result.Message = "非管理账号";
                    return;
                }
                if (user.Status == 2)
                {
                    result.ResultNo = -3;
                    result.Message = $"账号已冻结,如有疑问,可以发邮件给管理员";
                    return;
                }
                var role = _roleRepository.GetByUserId(user.Id);
                if (role == null)
                {
                    result.ResultNo = -1;
                    result.Message = "账号未设置角色,请联系管理员";
                    return;
                }
                var allMenus = _subMenuRepository.GetAll<MenuVModel>();
                if (role.Type == 1)
                {
                    result.TModel = allMenus;
                }
                else
                {
                    var thirdMenus = _subMenuRepository.GetByRoleId<MenuVModel>(role.Id);
                    if (thirdMenus?.Any() ?? false)
                    {
                        var secondMenus = allMenus.FindAll(x => thirdMenus.Exists(y => x.id == y.parentId) && !thirdMenus.Exists(y=>x.id == y.id));
                        if (secondMenus?.Any() ?? false)
                        {
                            thirdMenus.AddRange(secondMenus);
                            var firstMenus = allMenus.FindAll(x => secondMenus.Exists(y => x.id == y.parentId) && !secondMenus.Exists(y => x.id == y.id));
                            if (firstMenus?.Any() ?? false)
                            {
                                thirdMenus.AddRange(firstMenus);
                            }
                        }
                    }

                    result.TModel = thirdMenus?.Distinct( )?.ToList();
                }

            }, true);
        }

        /// <summary>
        /// 获取全部菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<List<MenuVModel>>))]
        public IActionResult GetAllMenu()
        {
            var result = new TResultModel<List<MenuVModel>>(1);
            return this.Wrapper(ref result, () =>
            {
                result.TModel = _subMenuRepository.GetAll<MenuVModel>();
            }, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<List<MenuVModel>>))]
        public IActionResult GetByRole([FromBody] RoleIdVModel model)
        {
            var result = new TResultModel<List<MenuVModel>>(1);
            return this.Wrapper(ref result, () =>
            {

                result.TModel = _subMenuRepository.GetByRoleId<MenuVModel>(model.roleId);
            }, true);
        }

        /// <summary>
        /// 新增主菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult AddMainMenu([FromBody] AddMainMenuVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result = _menuRepository.Add(new HeyTom.Manage.Model.Menu()
                {
                    FileName = "",
                    Name = "",
                    ParentId = 0,
                    Path = "",
                    Title = model.title,
                    Icon = model.icon
                });
            }, true);
        }

        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult RemoveMainMenu([FromBody] MenuIdVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result = _menuRepository.Delete(model.id);
            }, true);
        }


        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult UpdateMainMenu([FromBody] UpdateMainMenuVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result = _menuRepository.Update(new HeyTom.Manage.Model.Menu()
                {
                    Id = model.id,
                    FileName = "",
                    Name = "",
                    ParentId = 0,
                    Path = "",
                    Title = model.title,
                    Icon = model.icon
                });
            }, true);
        }

        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult AddSubMenu([FromBody] AddMenuVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result = _subMenuRepository.Add(new HeyTom.Manage.Model.Menu()
                {
                    FileName = model.fileName,
                    Name = model.name,
                    ParentId = model.parentId,
                    Path = model.path,
                    Title = model.title,
                    Icon = model.icon
                });
            }, true);
        }

        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult RemoveSubMenu([FromBody] MenuIdVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                _subMenuRepository.Delete(model.id);
            }, true);
        }


        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult UpdateSubMenu([FromBody] MenuVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                _subMenuRepository.Update(new HeyTom.Manage.Model.Menu()
                {
                    FileName = model.fileName,
                    Name = model.name,
                    ParentId = model.parentId,
                    Path = model.path,
                    Title = model.title,
                    Id = model.id,
                    Icon = model.icon
                });
            }, true);
        }
    }
}
