using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Infrastructure.Repositories
{
    public class CueRepository : GenericRepository<Cue>, ICueRepositoty
    {
        private readonly BilliardDbContext _dbContext;
        public CueRepository(BilliardDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
