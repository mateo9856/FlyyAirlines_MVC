using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines.Services.Permissions;
using FlyyAirlines_MVC.Models;
using FlyyAirlines_MVC.Models.FormModels;
using FlyyAirlines_MVC.Models.StaticModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class PermissionController : Controller
    {
        private readonly IPermissionService permission;
        private readonly IMapper mapper;
        private readonly IUserService user;

        public PermissionController(IPermissionService permissionService, IMapper _mapper, IUserService userManager)
        {
            permission = permissionService;
            mapper = _mapper;
            user = userManager;
        }

        public async Task<IActionResult> PermissionList(int page = 1)
        {
            var GetPermissions = permission.GetList();
            var model = await PagingList.CreateAsync(GetPermissions, 10, page);
            return View(model);
        }

        public async Task<IActionResult> EditView(string id)
        {
            if(id == null)
            {
                return View(new PermissionFormModel());
            }

            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            var GetPermission = permission.GetById(id);

            var MapToModel = mapper.Map<PermissionFormModel>(GetPermission);

            return View(MapToModel);
        }

        public async Task<IActionResult> AddToUser()
        {
            var GetUser = await user.GetByClaim(User);

            if(Authorization.Can("ADMIN", GetUser))
            {
                var GetPermissions = await permission.GetAll();
                var GetUsers = user.GetAll();
                return View("PermissionManager", new PermissionManagerModel() { 
                    Operation = "ADD",
                    Permissions = GetPermissions.ToList(),
                    Users = GetUsers.ToList(),
                });
            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
        }

        [HttpPost]
        public async Task<IActionResult> AddToUser(PermissionManagerModel model)
        {
            var GetUser = await user.GetByClaim(User);
            if (Authorization.Can("ADMIN", GetUser))
            {
                var GetPermissions = await permission.GetByMulitpleId(model.SelectedPermissions);
                var GetUsers = user.GetByMulitpleId(model.SelectedUsers);

                if(GetPermissions == null || GetUsers == null)
                {
                    return RedirectToAction("Error", "Home", new { ErrorName = "FormError" });
                }

                foreach(var users in GetUsers)
                {
                    foreach(var permissions in GetPermissions)
                    {
                        if(!users.Permissions.Contains(permissions))
                        {
                            await permission.AddPermissionToUser(users, permissions.Name);
                        }
                    }
                }
            }
            return RedirectToAction("PermissionList");
        }

        public async Task<IActionResult> RemoveFromUser()
        {
            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser))
            {
                var GetPermissions = await permission.GetAll();
                var GetUsers = user.GetAll();
                return View("PermissionManager", new PermissionManagerModel()
                {
                    Operation = "REMOVE",
                    Permissions = GetPermissions.ToList(),
                    Users = GetUsers.ToList(),
                });
            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromUser(PermissionManagerModel model)
        {
            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser))
            {
                var GetPermissions = await permission.GetByMulitpleId(model.SelectedPermissions);
                var GetUsers = user.GetByMulitpleId(model.SelectedUsers);

                if (GetPermissions == null || GetUsers == null)
                {
                    return RedirectToAction("Error", "Home", new { ErrorName = "FormError" });
                }

                foreach (var users in GetUsers)
                {
                    foreach (var permissions in GetPermissions)
                    {
                        if (users.Permissions.Contains(permissions))
                        {
                            await permission.DeleteUserPermission(users, permissions.Name);
                        }
                    }
                }
            }

            return RedirectToAction("PermissionList");
        }

        public async Task<IActionResult> Create(PermissionFormModel model)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            if (ModelState.IsValid)
            {
                var MapToPermission = mapper.Map<Permission>(model);
                MapToPermission.Name = model.Name.ToUpper();
                MapToPermission.Id = Guid.NewGuid().ToString();
                await permission.AddPermissionToTable(MapToPermission);
            }

            return RedirectToAction("PermissionList");
        }

        public async Task<IActionResult> Edit(string id, PermissionFormModel model)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            var GetId = await permission.GetById(id);

            if(GetId == null)
            {
                return NotFound();
            }

            var MapToPermission = mapper.Map(model, GetId);

            MapToPermission.Name = model.Name.ToUpper();

            await permission.UpdatePermission(MapToPermission);

            return RedirectToAction("PermissionList");
        }
    
        public async Task<IActionResult> Delete(string id)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            var GetPermission = await permission.GetById(id);

            await permission.DeletePermission(GetPermission);

            return RedirectToAction("PermissionList");
        }
    
    }
}
