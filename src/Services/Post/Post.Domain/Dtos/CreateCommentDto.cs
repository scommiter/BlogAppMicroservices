namespace Post.Domain.Dtos
{
    public class CreateCommentDto
    {
        public Guid PostId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public int? AncestorId { get; set; }
    }
}
