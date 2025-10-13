using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _context;
        public EfPostRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public void EditPost(Post post)
        {
            var model = _context.Posts.FirstOrDefault(i => i.PostId == post.PostId);
            if (model != null)
            {
                model.Title = post.Title;
                model.Content = post.Content;
                model.Description = post.Description;
                model.Image = post.Image;
                model.IsActive = post.IsActive;
                model.PublishedOn = post.PublishedOn;
                model.Url = post.Url;
                _context.SaveChanges();
            }
        }
        public void EditPost(Post post, int[] tagIds)
        {
            var model = _context.Posts.Include(i => i.Tags).FirstOrDefault(i => i.PostId == post.PostId);
            if (model != null)
            {
                model.Title = post.Title;
                model.Content = post.Content;
                model.Description = post.Description;
                model.Image = post.Image;
                model.IsActive = post.IsActive;
                model.PublishedOn = post.PublishedOn;
                model.Url = post.Url;

                model.Tags.Clear();
                var selectedTags = _context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
                foreach (var tag in selectedTags)
                    model.Tags.Add(tag);

                _context.SaveChanges();
            }
        }

    }
}
