namespace Anger_API.Database.Tests
{
    public class Test : Table
    {
        public override string TableName => "Anger_Test";
        public string Content { get; set; }
    }
}