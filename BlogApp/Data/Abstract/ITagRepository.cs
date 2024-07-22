using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags{ get; } //After receiving all the posts in context, we will be able to continue extra filtering
        //IEnumerable retrieves all posts and filters them. IQueryable filters and shows directly

        void CreateTag(Tag Tag);
        

        
    }
}
