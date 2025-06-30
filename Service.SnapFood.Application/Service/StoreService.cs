using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Guid> CreateAsync(StoreDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<StoreDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<StoreDto> GetStore()
        {
            var store = _unitOfWork.StoresRepo.FirstOrDefault(x => x.Status == Status.Activity);
            if (store is not null)
            {
                var address = await _unitOfWork.AddressRepo.GetByIdAsync(store.AddressId);
                if (address == null)
                    throw new Exception("Không tìm thấy địa chỉ");

                StoreDto StoreDto = new StoreDto()
                {
                    Id = store.Id,
                    StoreName = store.StoreName,
                    Address = new AddressDto()
                    {
                        Id = address.Id,
                        FullName = address.FullName,
                        NumberPhone = address.NumberPhone,
                        Province = address.Province,
                        District = address.District,
                        Ward = address.Ward,
                        SpecificAddress = address.SpecificAddress,
                        Latitude = address.Latitude,
                        Longitude = address.Longitude,
                        FullAddress = address.FullAddress,
                    }
                };
                return StoreDto;
            }
            throw new Exception("Không tìm thấy của hàng");
        }

        public Task<bool> UpdateAsync(Guid id, StoreDto item)
        {
            throw new NotImplementedException();
        }
    }
}
