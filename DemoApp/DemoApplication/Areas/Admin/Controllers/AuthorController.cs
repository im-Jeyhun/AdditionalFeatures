using DemoApplication.Areas.Admin.ViewComponents;
using DemoApplication.Areas.Admin.ViewModels.Author;
using DemoApplication.Areas.Client.ViewComponents;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/author")]
    public class AuthorController : Controller
    {
        private readonly DataContext _dataContext;

        public AuthorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("list", Name = "admin-author-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = _dataContext.Authors
                .Select(a => new ListItemViewModel(a.Id, a.FirstName, a.LastName))
                .ToList();

            return View(model);
        }
        [HttpPost("add-author", Name ="add-author")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var addViewComponent = ViewComponent(nameof(AddModal), model);
                addViewComponent.StatusCode = (int)HttpStatusCode.BadRequest;
                return addViewComponent;
            }

            var author = new Author
            {
                FirstName = model.FirsName,
                LastName = model.LastName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _dataContext.Authors.AddAsync(author);
            await _dataContext.SaveChangesAsync();

            var responeModel = new ListItemViewModel(author.Id, author.FirstName, author.LastName);

            var listPartialView = PartialView("Partials/Author/_ListItem", responeModel);

            listPartialView.StatusCode = (int)HttpStatusCode.Created;

            return listPartialView;
        }

        [HttpDelete("delete-author", Name = "delete-author")]

        public async Task<IActionResult> DeleteAsync (int id )
        {
            var author = await _dataContext.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if(author == null)
            {
                return NotFound();
            }

            _dataContext.Authors.Remove(author);
          await _dataContext.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("update-author/{id}", Name ="update-author")]

        public async Task<IActionResult> GetUpdateVaule(int id)
        {
            var author = await _dataContext.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            return ViewComponent(nameof(UpdateModal), model);
        }


        [HttpPut("update-author/{id}", Name = "update-author")]

        public async Task<IActionResult> UpdateAsync(int id , UpdateViewModel? model)
        {
            if (!ModelState.IsValid)
            {
                var viewComponent = ViewComponent(nameof(UpdateModal), model);
                viewComponent.StatusCode = (int)HttpStatusCode.BadRequest;
                return viewComponent;
            }
            
            var author = await _dataContext.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            author.FirstName = model.FirstName;
            author.LastName = model.LastName;
            author.UpdatedAt = DateTime.Now;

            _dataContext.SaveChanges();


            var responseModel = new ListItemViewModel(author.Id, author.FirstName, author.LastName);
            return PartialView("Partials/Author/_ListItem", responseModel);

        }


    }
}
