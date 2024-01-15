using Contracts.Domains;

namespace Post.Domain.Entities
{
    public class Post : EntityAuditBase<Guid>
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}
