using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Dtos.Footer;
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
            var store = _unitOfWork.StoresRepo.GetAll().First();
            if (store is not null)
            {
                var address = await _unitOfWork.AddressRepo.GetByIdAsync(store.AddressId);
                if (address == null)
                    throw new Exception("Không tìm thấy địa chỉ");

                var thongTinNhanHang = _unitOfWork.ThongTinGiaoHangRepository.GetAll().First();
                if (thongTinNhanHang == null)
                    throw new Exception("Không tìm thấy thông tin nhận hàng");
                StoreDto StoreDto = new StoreDto()
                {
                    Id = store.Id,
                    StoreName = store.StoreName,
                    Status = store.Status,
                    ThoiGianBatDauHoatDong = DateTime.Today.Add(store.ThoiGianBatDauHoatDong.ToTimeSpan()),
                    ThoiGianNgungHoatDong = DateTime.Today.Add(store.ThoiGianNgungHoatDong.ToTimeSpan()),
                    NumberPhone = store.NumberPhone,
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
                        Description = address.Description
                    },
                    ThongTinGiaoHang = new ThongTinGiaoHangDto()
                    {
                        Id = thongTinNhanHang.Id,
                        BanKinhGiaoHang = thongTinNhanHang.BanKinhGiaoHang,
                        PhiGiaoHang = thongTinNhanHang.PhiGiaoHang,
                        DonHangToiThieu = thongTinNhanHang.DonHangToiThieu
                    }

                };

                return StoreDto;
            }
            throw new Exception("Không tìm thấy của hàng");
        }

        public async Task<bool> UpdateAsync(Guid id, StoreDto item)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");


            var store = await _unitOfWork.StoresRepo.GetByIdAsync(id);
            if (store == null)
                throw new Exception("Không tìm thấy người dùng");

            store.StoreName = item.StoreName;
            store.NumberPhone = item.NumberPhone;
            store.ThoiGianBatDauHoatDong = item.ThoiGianBatDauHoatDong.HasValue ? TimeOnly.FromDateTime(item.ThoiGianBatDauHoatDong.Value) : default;
            store.ThoiGianNgungHoatDong = item.ThoiGianNgungHoatDong.HasValue ? TimeOnly.FromDateTime(item.ThoiGianNgungHoatDong.Value) : default;
            store.Status = item.Status;


            _unitOfWork.StoresRepo.Update(store);
            await _unitOfWork.CompleteAsync();
            return true;
        }

       public async Task<FooterDto> GetFooter()
        {
            var store = _unitOfWork.StoresRepo.GetAll().First();
            if (store is not null)
            {
                var address = await _unitOfWork.AddressRepo.GetByIdAsync(store.AddressId);
                if (address == null)
                    throw new Exception("Không tìm thấy địa chỉ");

                var thongTinNhanHang = _unitOfWork.ThongTinGiaoHangRepository.GetAll().First();
                if (thongTinNhanHang == null)
                    throw new Exception("Không tìm thấy thông tin nhận hàng");

                FooterDto FooterDto = new FooterDto()
                {
                    TenCuaHang = store.StoreName,
                    DiaChi = address.FullAddress,
                    DienThoai = store.NumberPhone,
                    ThoiGianHoatDong = $"{store.ThoiGianBatDauHoatDong.ToString(@"HH\:mm")} - {store.ThoiGianNgungHoatDong.ToString(@"HH\:mm")} ",
                    PhiGiaoHangKm = thongTinNhanHang.PhiGiaoHang,
                    GiaoHangToiDa = thongTinNhanHang.BanKinhGiaoHang,
                    GiaTriDonHang = thongTinNhanHang.DonHangToiThieu
                };
                
                return FooterDto;
            }
            throw new Exception("Không tìm thấy của hàng");
        }
    }
}
