namespace Anger_API.Database.FavPosts
{
    public class FavPostRepository : Repository, IFavPostRepository
    {
        public override string TableName => "Anger_FavPost";
    }
}