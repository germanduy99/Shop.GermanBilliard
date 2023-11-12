using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly BilliardDbContext _dbContext;

        public BrandRepository(BilliardDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
