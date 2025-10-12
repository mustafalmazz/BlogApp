using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
                .Include(x => x.User)
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
        [Authorize]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (userId == null)
            //{
            //    return RedirectToAction("Login", "Users");
            //}
            if (ModelState.IsValid)
            {
                _postRepository.CreatePost(new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    Description = model.Description,
                    Url = model.Url,
                    PublishedOn = DateTime.Now,
                    IsActive = false,
                    UserId = int.Parse(userId ?? ""),
                    Image = "1.jpg"
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;
            if (string.IsNullOrEmpty(role))
            {
                posts = posts.Where(i => i.UserId == userId);
            }
            return View(await posts.ToListAsync());
        }
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _postRepository.Posts.FirstOrDefault(i => i.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            var model = new PostCreateViewModel
            {
                PostId = post.PostId,
                IsActive = post.IsActive,
                Title = post.Title,
                Content = post.Content,
                Description = post.Description,
                Url = post.Url,
                Image = post.Image

            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Post
                {
                    PostId = model.PostId,
                    Title = model.Title,
                    Content = model.Content,
                    Description = model.Description,
                    Url = model.Url,
                    Image = model.Image
                };
                if (User.FindFirstValue(ClaimTypes.Role) == "admin")
                {
                    entity.IsActive = model.IsActive;
                }
                _postRepository.EditPost(entity);
                return RedirectToAction("List");
            }
            return View(model);

        }
    }
}

