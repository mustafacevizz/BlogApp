using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts{ get; } //After receiving all the posts in context, we will be able to continue extra filtering
        //IEnumerable retrieves all posts and filters them. IQueryable filters and shows directly

        void CreatePost(Post post);
        void EditPost(Post post);
        void EditPost(Post post, int[] tagIds);
        

        
    }
}
