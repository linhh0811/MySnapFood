using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Get dữ liệu
        public async Task<List<Address>> GetAllAsync()
        {
            var addresses = await _unitOfWork.AddressRepo.GetAllAsync();
            return addresses.ToList();
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var address = await _unitOfWork.AddressRepo.GetByIdAsync(id);
            if (address == null)
                throw new Exception("Không tìm thấy địa chỉ");

            return address;
        }
        #endregion

        #region Thêm, sửa, xóa
        public async Task<Guid> CreateAsync(AddressDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Dữ liệu thêm mới không được để trống");

            ValidateAddressInput(item);

            var address = new Address
            {
                UserId = item.UserId,
                FullName = item.FullName,
                NumberPhone = item.NumberPhone,
                Province = item.Province,
                District = item.District,
                Ward = item.Ward,
                SpecificAddress = item.SpecificAddress,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                FullAddress = item.FullAddress,
                AddressType = item.AddressType
            };
            address.FillDataForInsert(Guid.NewGuid()); // Giả định người tạo là một Guid ngẫu nhiên

            _unitOfWork.AddressRepo.Add(address);
            await _unitOfWork.CompleteAsync();
            return address.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, AddressDto item)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Dữ liệu cập nhật không được để trống");

            ValidateAddressInput(item);

            var address = await _unitOfWork.AddressRepo.GetByIdAsync(id);
            if (address == null)
                throw new Exception("Không tìm thấy địa chỉ");

            address.UserId = item.UserId;
            address.FullName = item.FullName;
            address.NumberPhone = item.NumberPhone;
            address.Province = item.Province;
            address.District = item.District;
            address.Ward = item.Ward;
            address.SpecificAddress = item.SpecificAddress;
            address.Latitude = item.Latitude;
            address.Longitude = item.Longitude;
            address.FullAddress = item.FullAddress;
            address.AddressType = item.AddressType;
            address.FillDataForUpdate(Guid.NewGuid()); // Giả định người cập nhật là một Guid ngẫu nhiên

            _unitOfWork.AddressRepo.Update(address);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var address = await _unitOfWork.AddressRepo.GetByIdAsync(id);
            if (address == null)
                throw new Exception("Không tìm thấy địa chỉ");

            _unitOfWork.AddressRepo.Delete(address);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        #endregion

        #region Validate
        private void ValidateAddressInput(AddressDto item)
        {
            if (string.IsNullOrWhiteSpace(item.FullName))
                throw new ArgumentException("Họ tên không được để trống");
            if (string.IsNullOrWhiteSpace(item.NumberPhone))
                throw new ArgumentException("Số điện thoại không được để trống");
            if (!IsValidPhoneNumber(item.NumberPhone))
                throw new ArgumentException("Số điện thoại không hợp lệ (phải bắt đầu bằng 0 và có 10 chữ số)");
            if (string.IsNullOrWhiteSpace(item.Province))
                throw new ArgumentException("Tỉnh không được để trống");
            if (string.IsNullOrWhiteSpace(item.District))
                throw new ArgumentException("Huyện không được để trống");
            if (string.IsNullOrWhiteSpace(item.Ward))
                throw new ArgumentException("Xã không được để trống");
            if (string.IsNullOrWhiteSpace(item.SpecificAddress))
                throw new ArgumentException("Địa chỉ cụ thể không được để trống");
            if (item.Latitude == 0 || item.Longitude == 0)
                throw new ArgumentException("Tọa độ không hợp lệ");
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^0\d{9}$");
        }
        #endregion
    }
}