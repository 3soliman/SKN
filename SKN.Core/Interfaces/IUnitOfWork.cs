using System.Threading.Tasks;

namespace SKN.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();
        void Dispose();
    }
}