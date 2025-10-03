using BlogApp.Data;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        public PostsController(IPostRepository repository, ITagRepository tagRepository)
        {
            _postRepository = repository;
            _tagRepository = tagRepository;
        }
        public async Task<IActionResult> Index(string tagName)
        {
            var posts = _postRepository.Posts.Where(i => i.IsActive);

            if (!string.IsNullOrEmpty(tagName))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tagName));
            }

            return View(new PostViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> DetailsAsync(/*int id*/ string url)
        {
            var model = await _postRepository.Posts.FirstOrDefaultAsync(a=>a.Url == url);
            return View(model);
        }
    }
}

