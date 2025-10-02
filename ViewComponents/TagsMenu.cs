using BlogApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
    public class TagsMenu : ViewComponent
    {
        private ITagRepository _tagRepository;
        public TagsMenu(ITagRepository _tagRepository)
        {
            _tagRepository = _tagRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_tagRepository.Tags.ToList());
        }
    }
}
