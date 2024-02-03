namespace Shared.Dtos.Post
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
    }
}
