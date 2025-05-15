using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Domain.Interfaces;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.SnapFood.Infrastructure.EF.Contexts;
using Service.SnapFood.Domain.Entitys;

namespace Service.SnapFood.Infrastructure.Repositorys.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;
        public IProductRepository ProductRepo { get; private set; }

        public IComboRepository ComboRepo { get; private set; }

        public IUserRepository UserRepo { get; private set; }

        public IBillRepository BillRepo { get; private set; }

        public IBillDetailsRepository BillDetailsRepo { get; private set; }

        public ICartRepository CartRepo  { get; private set; }

        public ICartItemRepository CartItemRepo { get; private set; }

        public IProductComboRepository ProductComboRepo { get; private set; }

        public IAddressRepository AddressRepo { get; private set; }
        public IStoresRepository StoresRepo { get; private set; }

        public IComboItemsArchiveRepository ComboItemsArchiveRepo { get; private set; }

        public IBillPaymentRepository BillPaymentRepo { get; private set; }

        public IBillNotesRepository BillNotesRepo { get; private set; }

        public IBillDeliveryRepository BillDeliveryRepo { get; private set; }

        public IRolesRepository RolesRepo { get; private set; }

        public IUserRoleRepository UserRoleRepo { get; private set; }

        public ISizesRepository SizesRepo { get; private set; }

        public ICategoriesRepository CategoriesRepo { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            ProductRepo = new ProductRepository(_context);
            ComboRepo = new ComboRepository(_context);
            CartRepo = new CartRepository(_context);
            UserRepo = new UserRepository(_context);
            BillRepo = new BillRepository(_context);
            ProductComboRepo = new ProductComboRepository(_context);
            CartItemRepo = new CartItemRepository(_context);
            BillDetailsRepo = new BillDetailsRepository(_context);
            AddressRepo = new AddressRepository(_context);
            StoresRepo = new StoresRepository(_context);
            ComboItemsArchiveRepo = new ComboItemsArchiveRepository(_context);
            BillPaymentRepo = new BillPaymentRepository(_context);
            BillNotesRepo = new BillNotesRepository(_context);
            BillDeliveryRepo = new BillDeliveryRepository(_context);
            RolesRepo = new RolesRepository(_context);
            UserRoleRepo= new UserRoleRepository(_context);
            SizesRepo = new SizesRepository(_context);
            CategoriesRepo = new CategoriesRepository(_context);
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                var userId = Guid.Parse("f812d8e1-9f1c-4a47-9e47-055243b7551b");
                _context.SaveChanges(userId);
                _transaction?.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                var userId = Guid.Parse("f812d8e1-9f1c-4a47-9e47-055243b7551b");
                await _context.SaveChangesAsync(userId);
                _transaction?.Commit();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public int Complete()
        {
            var userId =Guid.Parse("f812d8e1-9f1c-4a47-9e47-055243b7551b");
            return _context.SaveChanges(userId);
        }

        public async Task<int> CompleteAsync()
        {
            var userId = Guid.Parse("f812d8e1-9f1c-4a47-9e47-055243b7551b");
            return await _context.SaveChangesAsync(userId);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }   
        }
    }
}
