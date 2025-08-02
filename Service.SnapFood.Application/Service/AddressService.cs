
using Newtonsoft.Json;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class AddressService : IAddressService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _apiKey = "GTzwweyhgu0GBvSH0XJjPkPDwYeFkVV_ok80Oyas_qA";
        private readonly HttpClient _httpClient;

        public AddressService(IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _httpClient= httpClient;
            
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
        public List<AddressDto> GetAddressByUserId(Guid userId)
        {
            var addresses = _unitOfWork.AddressRepo
                .FindWhere(a => a.UserId == userId)
                .Select(a => new AddressDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    FullName = a.FullName,
                    NumberPhone = a.NumberPhone,
                    Province = a.Province,
                    District = a.District,
                    Ward = a.Ward,
                    SpecificAddress = a.SpecificAddress,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    FullAddress = a.FullAddress,
                    AddressType = a.AddressType,
                    ModerationStatus = a.ModerationStatus,
                    Description=a.Description
                })
                .ToList();
            return addresses;
        }
        #endregion

        #region Thêm, sửa, xóa
        public async Task<Guid> CreateAsync(AddressDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Dữ liệu thêm không được để trống");

            var checkAddress = _unitOfWork.AddressRepo.FindWhere(x => x.UserId == item.UserId && x.AddressType == AddressType.Default);
            if (!checkAddress.Any())
            {
                item.AddressType = AddressType.Default;
            }

            if (item.AddressType == AddressType.Default)
            {
                // Lấy mọi địa chỉ của user đang là Default
                var oldDefaults = _unitOfWork.AddressRepo
                                    .FindWhere(a => a.UserId == item.UserId &&
                                                    a.AddressType == AddressType.Default)
                                    .ToList();                 // Tracked entities!

                foreach (var addr in oldDefaults)
                    addr.AddressType = AddressType.Normal;     // Chỉ đổi enum, EF sẽ nhận biết
            }
            var fullAddress = $"{item.SpecificAddress}, {item.Ward}, {item.District}, {item.Province}";
            var coordinates = await GetCoordinatesAsync(fullAddress);

            var entity = new Address
            {
                UserId = item.UserId,
                FullName = item.FullName,
                NumberPhone = item.NumberPhone,
                Province = item.Province,
                District = item.District,
                Ward = item.Ward,
                SpecificAddress = item.SpecificAddress,
                Latitude = coordinates.Latitude,
                Longitude = coordinates.Longitude,
                FullAddress = fullAddress,
                AddressType = item.AddressType,
                Description=item.Description
            };
    

            _unitOfWork.AddressRepo.Add(entity);

            await _unitOfWork.CompleteAsync();                 

            return entity.Id;
        }




        public async Task<bool> UpdateAsync(Guid id, AddressDto dto)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID không hợp lệ");
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var address = await _unitOfWork.AddressRepo.GetByIdAsync(id)
                          ?? throw new Exception("Không tìm thấy địa chỉ");

           
            if (dto.AddressType == AddressType.Default)
            {
                // Lấy các địa chỉ Default cũ dưới dạng tracked entity
                var oldDefaults = await _unitOfWork.AddressRepo
                                      .FindTrackedAsync(a => a.UserId == address.UserId &&
                                                             a.AddressType == AddressType.Default &&
                                                             a.Id != id);

                foreach (var addr in oldDefaults)
                    addr.AddressType = AddressType.Normal;    // hạ cấp

              
                _unitOfWork.AddressRepo.UpdateRange(oldDefaults);
            }
            var fullAddress = $"{dto.SpecificAddress}, {dto.Ward}, {dto.District}, {dto.Province}";
            var coordinates =await GetCoordinatesAsync(fullAddress);


            address.FullName = dto.FullName;
            address.NumberPhone = dto.NumberPhone;
            address.Province = dto.Province;
            address.District = dto.District;
            address.Ward = dto.Ward;
            address.SpecificAddress = dto.SpecificAddress;
            address.Latitude = coordinates.Latitude;
            address.Longitude = coordinates.Longitude;
            address.FullAddress = fullAddress;
            address.AddressType = dto.AddressType;
            address.Description = dto.Description;


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
     

      







        #endregion
        private async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string address)
        {
            string url = $"https://geocode.search.hereapi.com/v1/geocode?q={Uri.EscapeDataString(address)}&apiKey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return (0.0, 0.0);

            var result = await response.Content.ReadAsStringAsync();

            var coordinates = JsonConvert.DeserializeObject<dynamic>(result);
            if (coordinates.items != null && coordinates.items.Count > 0)
            {
                double latitude = coordinates.items[0].position.lat;
                double longitude = coordinates.items[0].position.lng;
                return (latitude, longitude);
            }

            return (0.0, 0.0);
        }

    }
}