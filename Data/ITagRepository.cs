using BlogApp.Entity;

namespace BlogApp.Data
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags { get; }
        void  CreateTag(Tag tag);
    }
}
