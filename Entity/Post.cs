namespace BlogApp.Entity
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Url { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<Tag>? Tags { get; set; }
        public List<Comment>? Comments { get; set; }

    }
}
//her postun 1 kullanıcısı olabilir ama bir kullanıcının birden fazla postu olabilir
//o halde bir olan =  user , n olan = post , n tablosuna userId eklenir
//user tabllosunda ise post listesi tutulabilir
//Özetle bir olan çok olana User user şeklinde eklenir , çok olan da tekin içinde koleksiyon veya Liste olarak eklenir
