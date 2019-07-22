namespace Anger_API.Database
{
    public interface IRepository
    {
        void Create(Table table);
        T RetrieveByID<T>(Table table, long ID);
    }
}