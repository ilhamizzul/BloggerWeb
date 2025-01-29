using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels.Tags;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest request)
        {
            ValidateAddTagRequest(request);
            if (!ModelState.IsValid)
            {
                return View();
            }
            var tag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };

            await tagRepository.AddTagAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List(string? searchQuery, string? sortBy, string? sortDirection)
        {
            ViewBag.searchQuery = searchQuery;
            ViewBag.sortBy = sortBy;
            ViewBag.sortDirection = sortDirection;

            var tags = await tagRepository.GetAllTagsAsync(searchQuery, sortBy, sortDirection);

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {

            var tag = await tagRepository.GetTagAsync(Id);

            if (tag == null)
            {
                return RedirectToAction("List");
            }

            var editTagRequest = new EditTagRequests
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };

            //return View(tag);
            return View(editTagRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequests request)
        {
            var tag = new Tag
            {
                Id = request.Id,
                Name = request.Name.Replace(" ", "-"),
                DisplayName = request.DisplayName
            };

            var updatedTag = await tagRepository.UpdateTagAsync(tag);

            if (updatedTag != null)
            {
                return RedirectToAction("List");
            }
            //Show error notification
            return RedirectToAction("Edit", new { request.Id });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequests request)
        {
            var deletedTag = await tagRepository.DeleteTagAsync(request.Id);
            if (deletedTag == null)
            {
                return RedirectToAction("Edit", new { request.Id });
            }

            return RedirectToAction("List");
        }

        private void ValidateAddTagRequest(AddTagRequest request)
        {
            if (request.Name is not null && request.DisplayName is not null)
            {
                if (request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }
    }
}
