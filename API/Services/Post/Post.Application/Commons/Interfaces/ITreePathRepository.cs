namespace Post.Application.Commons.Interfaces
{
    public interface ITreePathRepository
    {
        Task GetAllChildTreePathComment(int commentId);
        Task CreateTreePath(int commentId);
        Task CreateTreePathChild(int ancestor, int descendant);
        Task DeleteAllChildTreePathComment(int commentId);
    }
}
