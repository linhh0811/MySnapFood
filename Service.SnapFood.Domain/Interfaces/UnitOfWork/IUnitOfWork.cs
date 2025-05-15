﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepo { get; }
        IComboRepository ComboRepo { get; }
        IUserRepository UserRepo { get; }
        IBillRepository BillRepo { get; }
        IBillDetailsRepository BillDetailsRepo { get; }
        ICartRepository CartRepo { get; }
        ICartItemRepository CartItemRepo { get; }
        IProductComboRepository ProductComboRepo { get; }
        IAddressRepository AddressRepo { get; }
        IStoresRepository StoresRepo { get; }
        IComboItemsArchiveRepository ComboItemsArchiveRepo { get; }
        IBillPaymentRepository BillPaymentRepo { get; }
        IBillNotesRepository BillNotesRepo { get; }
        IBillDeliveryRepository BillDeliveryRepo { get; }
        IRolesRepository RolesRepo { get; }
        IUserRoleRepository UserRoleRepo { get; }
        ISizesRepository SizesRepo { get; }
        ICategoriesRepository CategoriesRepo { get; }
        Task<int> CompleteAsync();
        int Complete();
        void BeginTransaction();
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
