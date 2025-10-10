using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICommentRepository _commentRepository;
        public PostsController(IPostRepository repository,ITagRepository tagRepository ,ICommentRepository commentRepository)
        {
            _postRepository = repository;
            _tagRepository = tagRepository;
            _commentRepository = commentRepository;
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

        public async Task<IActionResult> Details(/*int id*/ string url)
        {
            var model = await _postRepository.Posts
                .Include(x => x.Tags)
                .Include(x=>x.Comments)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(a=>a.Url == url);
            return View(model);
        }
        public JsonResult AddComment(int PostId, string Text,string Url)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserName = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment
            {
                Text = Text,
                PostId = PostId,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(userId ?? "")
            };
            _commentRepository.CreateComment(entity);
            //return Redirect("/posts/details/" + Url);
            return Json(new
            {
                UserName ,
                Text,
                entity.PublishedOn,
                image = avatar

            });
        }
    }
}

