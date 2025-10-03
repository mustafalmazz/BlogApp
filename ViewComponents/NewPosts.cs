using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class NewPosts : ViewComponent
    {
        private readonly IPostRepository _postRepository;
        public NewPosts(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _postRepository
                .Posts
                .OrderByDescending(p => p.PublishedOn)
                .Take(5)
                .ToListAsync();
            return View(list);
        }
    }
}
