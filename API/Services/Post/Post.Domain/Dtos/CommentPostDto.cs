namespace Post.Domain.Dtos
{
    public class CommentPostDto
    {
        public List<DisplayCommentDto> Comments { get; set; }
        public List<TreePathDto> TreePaths { get; set; }
    }

    public class TreeComment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public int? AncestorId { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
