using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Services.Permissions;
using FlyyAirlines_MVC.Models.FormModels;
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

        public PermissionController(IPermissionService permissionService, IMapper _mapper)
        {
            permission = permissionService;
            mapper = _mapper;
        }

        public IActionResult PermissionList(int page = 1)
        {
            var GetPermissions = permission.GetList();
            var model = PagingList.CreateAsync(GetPermissions, 10, page);
            return View(model);
        }

        public IActionResult EditView(long? id)
        {
            if(id == null)
            {
                return View(new ReservationFormModel());
            }

            var GetPermission = permission.GetById(id.Value);

            var MapToModel = mapper.Map<PermissionFormModel>(GetPermission);

            return View(MapToModel);
        }

        public async Task<IActionResult> Create(PermissionFormModel model)
        {
            if(ModelState.IsValid)
            {
                var MapToPermission = mapper.Map<Permission>(model);
                MapToPermission.Id = permission.PermissionsLength();
                await permission.AddPermissionToTable(MapToPermission);
            }

            return RedirectToAction("PermissionList");
        }

        public async Task<IActionResult> Edit(long id, PermissionFormModel model)
        {
            var GetId = await permission.GetById(id);

            if(GetId == null)
            {
                return NotFound();
            }

            var MapToPermission = mapper.Map(model, GetId);

            await permission.UpdatePermission(MapToPermission);

            return RedirectToAction("PermissionList");
        }
    
        public async Task<IActionResult> Delete(long id)
        {
            var GetPermission = await permission.GetById(id);

            await permission.DeletePermission(GetPermission);

            return RedirectToAction("PermissionList");
        }
    
    }
}
