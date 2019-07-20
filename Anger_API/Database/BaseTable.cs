using System;

namespace Anger_API.Database
{
    public class BaseTable
    {
        public virtual string TableName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}