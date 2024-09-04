using Eventsir.Services.Events.Domain.Repositories.UoW;
using MongoDB.Driver;

namespace Eventsir.Services.Events.Infrastructure.Persistence.Repositories.UoW
{
    public sealed class UnitOfWork : IUnitOfWork
    {
            private IClientSessionHandle session { get; }
            public IDisposable Session => this.session;

            private List<Action> _operations { get; set; }

            public UnitOfWork(IMongoClient client)
            {
                this.session = client.StartSession();

                this._operations = new List<Action>();
            }

            public void AddOperation(Action operation)
            {
                this._operations.Add(operation);
            }

            public void CleanOperations()
            {
                this._operations.Clear();
            }

            public async Task CommitChanges()
            {
                this.session.StartTransaction();

                this._operations.ForEach(o =>
                {
                    o.Invoke();
                });

                await this.session.CommitTransactionAsync();

                this.CleanOperations();
            }
        }

    }

