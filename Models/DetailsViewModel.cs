using System;

namespace CodexBit.Models;

public class DetailsViewModel
{
    public PostModel Post { get; set; }
    public List<CommentModel> Comments { get; set; }
    public CommentModel NewComment { get; set; }
}