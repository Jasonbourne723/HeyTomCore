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
using System.Linq;
using HeyTom.Manage.Model;

namespace HeyMacchiato.Service.Manage.Apps.Controllers
{

    [Route("api/[controller]")]
    public class RoleController : BaseController
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;

        public RoleController(IRoleRepository roleRepository,
                                IRoleMenuRepository roleMenuRepository)
        {
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<List<RoleVModel>>))]
        public IActionResult GetAll()
        {
            var result = new TResultModel<List<RoleVModel>>(1);
            return this.Wrapper(ref result, () =>
            {
                result.TModel = new List<RoleVModel>();
                _roleRepository.GetList(100)?.ForEach(ea =>
                {
                    result.TModel.Add(new RoleVModel()
                    {
                        id = ea.Id,
                        name = ea.Name,
                        remark = ea.Remark,
                        type = ea.Type
                    });
                });

            }, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Add([FromBody] AddRoleVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result = _roleRepository.Add(new HeyTom.Manage.Model.Role()
                {
                    Name = model.name,
                    Type = 0,
                    Remark = model.remark
                });
            }, true);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Remove([FromBody] RoleIdVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result = _roleRepository.Delete(model.roleId);
            }, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Update([FromBody] RoleVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result = _roleRepository.Update(new HeyTom.Manage.Model.Role()
                {
                    Id = model.id,
                    Name = model.name,
                    Type = 0,
                    Remark = model.remark
                });
            }, true);
        }

        /// <summary>
        /// 设置菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult SetMenuPermissions([FromBody] SetMenuPermissionVModel model )
        {
            var result = new ResultModel();
           return this.Wrapper(ref result,()=> {
                _roleMenuRepository.DeleteByRoleId(model.roleId);

                var roleMenus = new List<RoleMenu>();
                model.menuIds?.ForEach(ea=> {
                    roleMenus.Add(new RoleMenu() { 
                        RoleId = model.roleId,
                        MenuId = ea
                    });
                });
                if (roleMenus?.Any() ?? false)
                {
                    _roleMenuRepository.Add(roleMenus);
                }
            },true);
        }
    }
}
