using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogApp.Data.Concrete.EfCore
{
    public class BlogContextFactory : IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();

            // Migration için kullanılacak connection string
            optionsBuilder.UseSqlite("Data Source=BlogDb.db");

            return new BlogContext(optionsBuilder.Options);
        }
    }
}
