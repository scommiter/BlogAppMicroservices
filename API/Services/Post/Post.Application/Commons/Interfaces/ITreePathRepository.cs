using Post.Domain.Entities;

namespace Post.Application.Commons.Interfaces
{
    public interface ITreePathRepository
    {
        Task<List<TreePath>> GetAllTreePath(List<int> commentIds);
        Task GetAllChildTreePathComment(int commentId);
        Task CreateTreePath(int commentId);
        Task CreateTreePathChild(int ancestor, int descendant);
        Task DeleteAllChildTreePathComment(int commentId);
    }
}
