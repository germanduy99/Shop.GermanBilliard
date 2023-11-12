using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>,IOrderRepository
    {
        private readonly BilliardDbContext _dbContext;

        public OrderRepository(BilliardDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
