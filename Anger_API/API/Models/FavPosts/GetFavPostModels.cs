using System.Collections.Generic;

using Anger_Library;
using Anger_API.Database.Views.FavPost;

namespace Anger_API.API.Models.FavPosts
{
    public class GetFavPostRequest : APIRequest
    {
        public string Action { get; set; }
        public string MemberID { get; set; }
        public string PostID { get; set; }
        public string StartRowNo { get; set; }
        public int ActionVal;
        public long MemberIDVal;
        public long PostIDVal;
        public int StartRowNoVal;
        public override void Validate()
        {
            APIException.ExRequiredInt(Action, nameof(Action), ref ActionVal);
            if (ActionVal < 1 || ActionVal> 2) APIException.ExInvalidParams(nameof(Action));

            APIException.ExRequiredLong(MemberID, nameof(MemberID), ref MemberIDVal);

            if (ActionVal == (int)GetFavPostAction.GetFavPostList)
            {
                APIException.ExRequiredInt(StartRowNo, nameof(StartRowNo), ref StartRowNoVal);
            }
            else if(ActionVal == (int)GetFavPostAction.GetFavPostByPostIDAndMemberID)
            {
                APIException.ExRequiredLong(PostID, nameof(PostID), ref PostIDVal);
            }
        }
    }
    public class GetFavPostResponse : ResponseBase
    {
        public List<FavPost> FavPosts { get; set; }
    }
    public enum GetFavPostAction
    {
        GetFavPostList = 1,
        GetFavPostByPostIDAndMemberID
    }
}