namespace Anger_API.Database.Tests
{
    public class TestRepository : Repository ,ITestRepository
    {
        public override string TableName => "Anger_Test";
    }
}