using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database.Views.FavPost
{
    public class FavPostRepository : Repository, IFavPostRepository
    {
        public override string TableName => "VE_FavPost";
        public async Task<List<FavPost>> RetrieveFavPostList(long memberID, int startRowNo)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var objs = await db.Query(TableName)
                .Where(nameof(FavPost.MemberID), memberID)
                .OrderByDesc(nameof(FavPost.CreatedAt))
                .Limit(10)
                .Offset(startRowNo)
                .GetAsync<FavPost>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}