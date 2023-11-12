using Microsoft.EntityFrameworkCore;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Infrastructure.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly BilliardDbContext _dbContext;
        public OrderItemRepository(BilliardDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteByOrder(int orderId)
        {
            var orderItemsToDelete = await _dbContext.OrderItems
            .Where(item => item.OrderId == orderId)
            .ToListAsync();
            _dbContext.OrderItems.RemoveRange(orderItemsToDelete);

        }

        public async Task<List<OrderItem>> FindByOrder(int orderId)
        {
            var orderItems = await _dbContext.OrderItems
           .Where(item => item.OrderId == orderId)
           .ToListAsync();
            return orderItems;
        }
    }
}
