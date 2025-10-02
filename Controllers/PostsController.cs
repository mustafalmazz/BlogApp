using BlogApp.Data;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            var viewModel = new PostViewModel
            {
                Posts = _postRepository.Posts.ToList(),
                Tags = _tagRepository.Tags.ToList() 
            };
            return View(viewModel);
        }
    }
}
