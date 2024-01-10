using Contracts.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Domain.Entities
{
    public class Comment : EntityAuditBase<int>
    {
        public Guid PostId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }

        [ForeignKey("PostId")]
        public Post CommentPost { get; set; }

        public ICollection<TreePath> Ancestors { get; set; }
        public ICollection<TreePath> Descendants { get; set; }
    }

    public class TreePath
    {
        [Required]
        [Column("ancestor")]
        public int Ancestor { get; set; }

        [Required]
        [Column("descendant")]
        public int Descendant { get; set; }

        [ForeignKey("Ancestor")]
        public Comment AncestorComment { get; set; }

        [ForeignKey("Descendant")]
        public Comment DescendantComment { get; set; }
    }
}
