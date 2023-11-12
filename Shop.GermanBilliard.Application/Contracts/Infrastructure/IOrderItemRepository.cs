using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Contracts.Infrastructure
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task DeleteByOrder(int orderId);

        Task<List<OrderItem>> FindByOrder(int orderId);
    }
}
