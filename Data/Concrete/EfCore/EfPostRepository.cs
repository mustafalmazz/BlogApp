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
            var entity = _context.Posts.Include(i => i.Tags).FirstOrDefault(i => i.PostId == post.PostId);

            if (entity != null)
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.IsActive = post.IsActive;

                entity.Tags = _context.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToList();

                _context.SaveChanges();
            }
        }

    }
}
