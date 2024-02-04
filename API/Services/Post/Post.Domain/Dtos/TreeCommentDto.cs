namespace Post.Domain.Dtos
{
    public class TreeCommentDto
    {
        public DisplayCommentDto Comment { get; set; }
        public List<TreeCommentDto>? CommentChilds { get; set; }
    }
}
