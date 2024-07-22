﻿namespace BlogApp.Entity
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedOn { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = null!; // one comment is for one post only
        public int UserId { get; set; }
        public User User { get; set; } = null!;     //A comment can only have one author
    }
}
