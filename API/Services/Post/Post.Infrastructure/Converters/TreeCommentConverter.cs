using Post.Domain.Dtos;

namespace Post.Infrastructure.Converters
{
    public class TreeCommentConverter
    {
        public static List<TreeComment> CreateTreeComment(List<DisplayCommentDto> comments, List<TreePathDto> treePaths)
        {
            var result = new List<TreeComment>();
            foreach (var comment in comments)
            {
                var treeComment = new TreeComment
                {
                    Id = comment.Id,
                    Author = comment.Author,
                    Content = comment.Content,
                    AncestorId = null,
                    CreateDate = comment.CreatedDate
                };
                var ancestorComment = treePaths.Where(e => e.Descendant == comment.Id && e.Ancestor != comment.Id).FirstOrDefault();
                if(ancestorComment != null)
                {
                    treeComment.AncestorId = ancestorComment.Ancestor;
                }
                result.Add(treeComment);
            }
            return result;
        }
    }
}
