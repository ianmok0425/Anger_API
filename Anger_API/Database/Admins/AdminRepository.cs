namespace Anger_API.Database.Admins
{
    public class AdminRepository : Repository, IAdminRepository
    {
        public override string TableName => "Anger_Admin";
    }
}