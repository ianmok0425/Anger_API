using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Anger_API.Database.Views.FavPost
{
    public interface IFavPostRepository : IRepository
    {
        Task<List<FavPost>> RetrieveFavPostList(long MemberID, int startRowNo);
        Task<FavPost> RetrieveFavPostListByPostIDAndMemberID(long memberID, long postID);
    }
}