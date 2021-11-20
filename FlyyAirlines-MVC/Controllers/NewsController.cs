using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines.Services.News;
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
    public class NewsController : Controller
    {
        private readonly IBaseService<News> newsService;
        private readonly INewsService news;
        private readonly IMapper mapper;
        private readonly IUserService user;

        public NewsController(IBaseService<News> _news, IMapper _mapper, INewsService _newsService, IUserService userManager)
        {
            newsService = _news;
            mapper = _mapper;
            news = _newsService;
            user = userManager;
        }

        public async Task<IActionResult> NewsList(int page = 1)
        {
            var GetAllNews = newsService.GetList();
            var Model = await PagingList.CreateAsync(GetAllNews, 5, page);
            return View(Model);
        }

        public async Task<IActionResult> NewsPanelList(int page = 1)
        {
            var GetAllNews = newsService.GetList();

            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADDNEWS", GetUser) || Authorization.Can("ADMIN", GetUser))
            {
                var Model = await PagingList.CreateAsync(GetAllNews, 5, page);
                return View(Model);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

        }
        public async Task<IActionResult> EditView(string id)
        {
            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser) || Authorization.Can("ADDNEWS", GetUser))
            {
                if (id == null)
                {
                    return View(new NewsFormModel()
                    {
                        PublicDate = DateTime.Now
                    });
                }

                var GetNews = newsService.Get(id);

                var MapToModel = mapper.Map<EmployeeFormModel>(GetNews);

                return View(MapToModel);

            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });

        }
        public async Task<IActionResult> Get(string id)
        {
            var GetNews = await newsService.Get(id);
            return View(GetNews);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsFormModel model)
        {
            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser) || Authorization.Can("ADDNEWS", GetUser))
            {
                if (ModelState.IsValid)
                {
                    var MapToNews = mapper.Map<News>(model);
                    MapToNews.Id = Guid.NewGuid().ToString();
                    MapToNews.ImageUrl = await news.UploadFile(model.Image);

                    if (MapToNews.ImageUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    await newsService.Add(MapToNews);

                    return RedirectToAction("NewsPanelList");

                }

            }
            else
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Not found" });
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, NewsFormModel model)
        {
            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser) || Authorization.Can("ADDNEWS", GetUser))
            {
                if (id == null)
                {
                    return RedirectToAction("Error", "Home", new { ErrorName = "Not found" });
                }

                var GetNews = await newsService.Get(id);

                if (GetNews == null)
                {
                    return RedirectToAction("Error", "Home", new { ErrorName = "Not found" });
                }

                if (model.Image != null)
                {
                    GetNews.ImageUrl = await news.UploadFile(model.Image);
                }

                var MapToNews = mapper.Map(model, GetNews);

                newsService.Update(MapToNews);

                return RedirectToAction("NewsPanelList");

            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
        }
        public async Task<IActionResult> Delete(string id)
        {
            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser) || Authorization.Can("ADDNEWS", GetUser))
            {
                var GetNews = await newsService.Get(id);
                await newsService.Delete(GetNews);
                return RedirectToAction("NewsPanelList");
            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
        }

    }
}
