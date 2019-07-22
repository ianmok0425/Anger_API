using System;

namespace Anger_API.Database
{
    public class Table
    {
        public virtual string TableName { get; set; }
        public long? ID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}