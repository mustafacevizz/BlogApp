﻿namespace BlogApp.Entity
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? Url { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!; // a post can only have one author
        public List<Tag> Tags { get; set; }=new List<Tag>(); //a post can have a lot Tag
        public List<Comment> Comments { get; set; } = new List<Comment>();  //a post can have a lot comment 
    }
}
