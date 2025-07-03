using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAsync();
        Task<Address> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(AddressDto item);
        Task<bool> UpdateAsync(Guid id, AddressDto item);
        Task<bool> DeleteAsync(Guid id);

       


       
    }
}