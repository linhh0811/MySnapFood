using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class ThongTinGiaoHangService : IThongTinGiaoHangService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ThongTinGiaoHangService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ThongTinGiaoHangDto GetDuLieu()
        {
            var thongTinNhanHang = _unitOfWork.ThongTinGiaoHangRepository.GetAll().First();
            if (thongTinNhanHang == null)
            {
                throw new Exception("Không tìm thấy thông tin giao hàng");
            }
            var ThongTinGiaoHang = new ThongTinGiaoHangDto()
            {
                Id = thongTinNhanHang.Id,
                BanKinhGiaoHang = thongTinNhanHang.BanKinhGiaoHang,
                PhiGiaoHang = thongTinNhanHang.PhiGiaoHang,
                DonHangToiThieu = thongTinNhanHang.DonHangToiThieu
            };
            return ThongTinGiaoHang;
        }

        public async Task<bool> UpdateAsync(Guid id, ThongTinGiaoHangDto item)
        {
            var thongTinGiaoHang =await _unitOfWork.ThongTinGiaoHangRepository.GetByIdAsync(id);
            if (thongTinGiaoHang == null)
            {
                throw new Exception("Không tìm thấy thông tin giao hàng");
            }
            thongTinGiaoHang.BanKinhGiaoHang = item.BanKinhGiaoHang;
            thongTinGiaoHang.PhiGiaoHang = item.PhiGiaoHang;
            thongTinGiaoHang.DonHangToiThieu = item.DonHangToiThieu;
            _unitOfWork.ThongTinGiaoHangRepository.Update(thongTinGiaoHang);
            await _unitOfWork.CompleteAsync();
            return true;

        }
    }
}
