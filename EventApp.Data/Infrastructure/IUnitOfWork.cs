namespace EventApp.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}