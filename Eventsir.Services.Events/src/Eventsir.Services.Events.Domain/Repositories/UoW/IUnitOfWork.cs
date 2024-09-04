namespace Eventsir.Services.Events.Domain.Repositories.UoW
{
    public interface IUnitOfWork
    {
        IDisposable Session { get; }

        void AddOperation(Action operation);

        void CleanOperations();

        Task CommitChanges();
    }
}
