using Microsoft.EntityFrameworkCore.Storage;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BilliardDbContext _context;
        private IDbContextTransaction _transaction;

        private ICueRepositoty _cueRepositoty;
        private IBrandRepository _brandRepository;
        private IOrderItemRepository _orderItemRepository;
        private IOrderRepository _orderRepository;

        public UnitOfWork(BilliardDbContext context)
        {
            _context = context;
        }

        public ICueRepositoty CueRepositoty => _cueRepositoty ??= new CueRepository(_context);

        public IBrandRepository BrandRepository => _brandRepository ??= new BrandRepository(_context);

        public IOrderItemRepository OrderItemRepository => _orderItemRepository ??= new OrderItemRepository(_context);

        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
