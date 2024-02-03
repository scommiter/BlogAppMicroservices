namespace Post.Domain.Dtos
{
    public class DisplayPostDto
    {
        public List<DisplayCommentDto> Comments { get; set; }
        public List<TreePathDto> TreePaths { get; set; }
    }
}
