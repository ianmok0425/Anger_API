namespace Anger_API.Database.Tests
{
    public class Test : BaseTable
    {
        public override string TableName => "Anger_Test";
        public string Content { get; set; }
    }
}