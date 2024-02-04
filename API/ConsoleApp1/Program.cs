using System;
using System.Collections.Generic;
using System.Linq;

public class CommentTreeNode
{
    public int Id { get; set; }
    public string Content { get; set; }
    public List<CommentTreeNode> Children { get; set; } = new List<CommentTreeNode>();
}

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
}

public class Program
{
    public static void Main()
    {
        var comments = new List<Comment>()
        {
            { new Comment { Id = 2, Content = "Hi ae, My name is Linh"} },
            { new Comment { Id = 3, Content = "Hi Linh, this is first"} },
            { new Comment { Id = 5, Content = "Hi Linh, this is second"} },
            { new Comment { Id = 4, Content = "Hi first, this is child first"} }
        };

        var treePaths = new List<Tuple<int, int>>
        {
            Tuple.Create(2, 3),
            Tuple.Create(2, 5),
            Tuple.Create(3, 4)
        };

        var commentTreeNodes = BuildCommentTree(treePaths, comments);

        // In kết quả
        DisplayCommentTree(commentTreeNodes);
    }

    public static List<CommentTreeNode> BuildCommentTree(List<Tuple<int, int>> treePaths, List<Comment> comments)
    {
        var ancestorMap = new Dictionary<int, CommentTreeNode>();

        CommentTreeNode BuildTreeRecursive(int ancestorId)
        {
            if (ancestorMap.ContainsKey(ancestorId))
            {
                return ancestorMap[ancestorId];
            }

            var newNode = new CommentTreeNode { Id = ancestorId };

            var descendants = treePaths.Where(path => path.Item1 == ancestorId).Select(path => path.Item2);

            foreach (var descendantId in descendants)
            {
                var childNode = BuildTreeRecursive(descendantId);
                newNode.Children.Add(childNode);
            }

            ancestorMap[ancestorId] = newNode;
            ancestorMap[ancestorId].Content = comments.Where(c => c.Id == ancestorId).First().Content;

            return newNode;
        }

        foreach (var path in treePaths)
        {
            BuildTreeRecursive(path.Item1);
        }

        return ancestorMap.Values.Where(node => !treePaths.Any(path => path.Item2 == node.Id)).ToList();
    }

    public static void DisplayCommentTree(List<CommentTreeNode> nodes, int indent = 0)
    {
        foreach (var node in nodes)
        {
            Console.WriteLine(new string(' ', indent) + $"Node {node.Id}");
            DisplayCommentTree(node.Children, indent + 2);
        }
    }
}
