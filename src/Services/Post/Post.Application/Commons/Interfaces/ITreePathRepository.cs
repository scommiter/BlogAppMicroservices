namespace Post.Application.Commons.Interfaces
{
    public interface ITreePathRepository
    {
        Task CreateTreePath(int ancestor, int descendant);
    }
}
