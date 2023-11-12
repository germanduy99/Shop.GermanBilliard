using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Contracts.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        ICueRepositoty CueRepositoty { get; }
        IBrandRepository BrandRepository { get; } 
        IOrderItemRepository OrderItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        void Rollback();
    }
}
