namespace BlogApp.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }

        public List<Post> Posts { get; set; }=new List<Post>(); //One user can Post more than once
        public List<Comment> Comments { get; set; } = new List<Comment>();  //One user can comment more than once
    }
}
