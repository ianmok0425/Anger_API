using System.ComponentModel.DataAnnotations;

namespace Anger_API.Database.Admins
{
    public class Admin : Table
    {
        public string Account { get; set; }
        public string Password { get; set; }
        [EnumDataType(typeof(AdminStatus))]
        public AdminStatus Status { get; set; }
        public enum AdminStatus
        {
            Active = 1,
            Inactive = 2
        }
    }
}