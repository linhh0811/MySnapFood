using Service.SnapFood.Application.Dtos.ThongKe;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class ThongKeService : IThongKeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ThongKeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TTSoLieuDto> GetTTSoLieu()
        {
            var SanPhamHoatDong = await _unitOfWork.ProductRepo.CountAsync(x => x.ModerationStatus == ModerationStatus.Approved);
            var TongSanPham = await _unitOfWork.ProductRepo.CountAsync();
            var ComboHoatDong = await _unitOfWork.ComboRepo.CountAsync(x => x.ModerationStatus == ModerationStatus.Approved);
            var TongCombo = await _unitOfWork.ComboRepo.CountAsync();
            var KhuyenMaiHoatDong = await _unitOfWork.PromotionRepository.CountAsync(x => x.ModerationStatus == ModerationStatus.Approved && x.StartDate <= DateTime.Now && DateTime.Now <= x.EndDate);
            var MaGiamGiaHoatDong = await _unitOfWork.DiscountCodeRepo.CountAsync(x => x.ModerationStatus == ModerationStatus.Approved && x.StartDate <= DateTime.Now && DateTime.Now <= x.EndDate);
            var TongNhanVien = await _unitOfWork.UserRepo.CountAsync(x => x.UserType == UserType.Store);
            var TongNguoiDung = await _unitOfWork.UserRepo.CountAsync(x => x.UserType == UserType.User);


            TTSoLieuDto tTSoLieuDto = new TTSoLieuDto()
            {
                SanPhamHoatDong = SanPhamHoatDong,
                TongSanPham = TongSanPham,
                ComboHoatDong = ComboHoatDong,
                TongCombo = TongCombo,
                KhuyenMaiHoatDong = KhuyenMaiHoatDong,
                MaGiamGiaHoatDong = MaGiamGiaHoatDong,
                TongNhanVien = TongNhanVien,
                TongNguoiDung = TongNguoiDung
            };
            return tTSoLieuDto;
        }
        public TTSoLieuTheoThoiGianDto GetTTSoLieuTheoThoiGian(BaseQuery baseQuery)
        {
            var tongBill = _unitOfWork.BillRepo.FindWhere(x => x.Created >= baseQuery.SearchTuNgay && x.Created <= baseQuery.SearchDenNgay);
            var tongHoaDon = tongBill.Count();

            var billDaXuLy = _unitOfWork.BillRepo.FindWhere(x => x.Created >= baseQuery.SearchTuNgay && x.Created <= baseQuery.SearchDenNgay && (x.Status == StatusOrder.Completed|| x.Status == StatusOrder.Cancelled));
            var TongHoaDonThanhCong = billDaXuLy.Count();

            var tongBillBiHuy = _unitOfWork.BillRepo.FindWhere(x => x.Created >= baseQuery.SearchTuNgay && x.Created <= baseQuery.SearchDenNgay && x.Status == StatusOrder.Cancelled);
            var tongHoaDonBiHuy = tongBillBiHuy.Count();

            var billThanhCong = _unitOfWork.BillRepo.FindWhere(x => x.Created >= baseQuery.SearchTuNgay && x.Created <= baseQuery.SearchDenNgay&&x.Status==StatusOrder.Completed);
            var listBillId = billThanhCong.Select(x => x.Id).ToList();
            
            var TienKhuyenMai = billThanhCong.Sum(x => x.TotalAmountEndow);
            var TienMaGiamGia = billThanhCong.Sum(x => x.DiscountAmount);
            var PhiShip =_unitOfWork.BillDeliveryRepo.FindWhere(x=>listBillId.Contains(x.BillId)).Select(x => x.DeliveryFee).Sum();
            var TongDoanhThu = billThanhCong.Sum(x => x.TotalAmount) - TienKhuyenMai - TienMaGiamGia + PhiShip;

            TTSoLieuTheoThoiGianDto tTSoLieuTheoThoiGianDto = new TTSoLieuTheoThoiGianDto()
            {
                TongHoaDonThanhCong=TongHoaDonThanhCong,
                TongHoaDon = tongHoaDon,
                TongHoaDonBiHuy=tongHoaDonBiHuy,
                TienKhuyenMai = TienKhuyenMai,
                TienMaGiamGia = TienMaGiamGia,
                PhiShip = PhiShip,
                TongDoanhThu = TongDoanhThu
            };
            return tTSoLieuTheoThoiGianDto;
        }
        public BieuDoSanPhamComboDto GetSanPhamComboDtos(BaseQuery baseQuery)
        {

            // 1. Lấy danh sách tất cả các ngày trong khoảng
            var allDates = Enumerable.Range(1, (baseQuery.SearchDenNgay!.Value.Date - baseQuery.SearchTuNgay!.Value.Date).Days)
                .Select(offset => baseQuery.SearchTuNgay.Value.Date.AddDays(offset))
                .ToList();

            var billThanhCong = _unitOfWork.BillRepo.FindWhere(x => x.Created >= baseQuery.SearchTuNgay && x.Created <= baseQuery.SearchDenNgay && x.Status == StatusOrder.Completed);
            var listBillId = billThanhCong.Select(x => x.Id).ToList();

            // 2. Lấy dữ liệu bill details theo khoảng ngày
            var billDetails = _unitOfWork.BillDetailsRepo.FindWhere(
                x => listBillId.Contains(x.BillId)
            ).ToList();

            var billDetailIds = billDetails.Select(x => x.Id).ToList();

            // 3. Lấy dữ liệu ComboItemsArchive liên quan
            var comboItemsArchive = _unitOfWork.ComboItemsArchiveRepo.FindWhere(
                x => billDetailIds.Contains(x.BillDetailsId)
            ).ToList();

            // 4. Group dữ liệu theo ngày
            var dataByDate = billDetails
                .GroupBy(x => x.Created.Date)
                .Select(g =>
                {
                    var comboCount = g.Sum(x => x.ItemType == ItemType.Combo ? x.Quantity : 0);
                    var productCount1 = g.Sum(x => x.ItemType == ItemType.Product ? x.Quantity : 0);

                    // Lấy product count từ ComboItemsArchive trong ngày này
                    var billDetailIdsInDay = g.Select(x => x.Id).ToList();
                    var productCount2 = (from c in comboItemsArchive
                                         join b in billDetails on c.BillDetailsId equals b.Id
                                         where billDetailIdsInDay.Contains(c.BillDetailsId)
                                         select c.Quantity * b.Quantity).Sum();

                    return new SanPhamComboDto
                    {
                        Ngay = g.Key,
                        Combo = comboCount,
                        Product = productCount1 + productCount2
                    };
                })
                .ToDictionary(x => x.Ngay, x => x); // để join với allDates nhanh hơn

            // 5. Trả kết quả: nếu ngày không có dữ liệu thì 0
            var sanPhamComboCount = allDates.Select(date =>
                dataByDate.ContainsKey(date)
                    ? dataByDate[date]
                    : new SanPhamComboDto
                    {
                        Ngay = date,
                        Combo = 0,
                        Product = 0
                    }
            ).ToList();


            // 6. Tính top 5 sản phẩm bán chạy
            var productQuantities = billDetails
                .Where(x => x.ItemType == ItemType.Product)
                .GroupBy(x => new { x.ItemId, x.ItemsName, x.ImageUrl })
                .Select(g => new
                {
                    ItemId = g.Key.ItemId,
                    ItemsName = g.Key.ItemsName,
                    ImageUrl = g.Key.ImageUrl,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .ToList();

            var productFromCombos = (from c in comboItemsArchive
                                     join b in billDetails on c.BillDetailsId equals b.Id
                                     where b.ItemType == ItemType.Combo
                                     group new { c, b } by new { c.ProductId, c.ProductName, c.ImageUrl } into g
                                     select new
                                     {
                                         ItemId = g.Key.ProductId,
                                         ItemsName = g.Key.ProductName,
                                         ImageUrl = g.Key.ImageUrl,
                                         Quantity = g.Sum(x => x.c.Quantity * x.b.Quantity)
                                     })
                .ToList();

            // Kết hợp số lượng sản phẩm từ bill details và combo items
            var combinedProductQuantities = productQuantities
                .Concat(productFromCombos)
                .GroupBy(x => new { x.ItemId, x.ItemsName, x.ImageUrl })
                .Select(g => new
                {
                    ItemId = g.Key.ItemId,
                    ItemsName = g.Key.ItemsName,
                    ImageUrl = g.Key.ImageUrl,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToList();

            var sanPhamThongKe = combinedProductQuantities
                .Select(x => new SanPhamThongKeDto
                {
                    Id = x.ItemId,
                    ProductName = x.ItemsName,
                    ImageUrl = x.ImageUrl,
                    Quantity = x.Quantity
                })
                .ToList();

            // 7. Tính top 5 combo bán chạy
            var comboQuantities = billDetails
                .Where(x => x.ItemType == ItemType.Combo)
                .GroupBy(x => new { x.ItemId, x.ItemsName, x.ImageUrl })
                .Select(g => new
                {
                    ItemId = g.Key.ItemId,
                    ItemsName = g.Key.ItemsName,
                    ImageUrl = g.Key.ImageUrl,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToList();

            var comboThongKe = comboQuantities
                .Select(x => new ComboThongKeDto
                {
                    Id = x.ItemId,
                    ComboName = x.ItemsName,
                    ImageUrl = x.ImageUrl,
                    Quantity = x.Quantity
                })
                .ToList();

            // 8. Tạo và trả về kết quả
            var result = new BieuDoSanPhamComboDto
            {
                SanPhamComboCount = sanPhamComboCount,
                SanPhamThongKe = sanPhamThongKe,
                ComboThongKeDto = comboThongKe
            };

            return result;
        }

        public List<DanhThuDto> GetDoanhThuDtos(BaseQuery baseQuery)
        {
            // 1. Lấy danh sách tất cả các ngày trong khoảng
            var allDates = Enumerable.Range(0, (baseQuery.SearchDenNgay!.Value.Date - baseQuery.SearchTuNgay!.Value.Date).Days + 1)
                .Select(offset => baseQuery.SearchTuNgay.Value.Date.AddDays(offset))
                .ToList();

            // 2. Lấy dữ liệu hóa đơn trong khoảng ngày và trạng thái Completed
            var billThanhCong = _unitOfWork.BillRepo.FindWhere(x =>
                x.Created >= baseQuery.SearchTuNgay &&
                x.Created <= baseQuery.SearchDenNgay &&
                x.Status == StatusOrder.Completed)
                .ToList();

            var listBillId = billThanhCong.Select(x => x.Id).ToList();

            // 3. Lấy phí vận chuyển từ BillDelivery
            var billDeliveries = _unitOfWork.BillDeliveryRepo.FindWhere(x => listBillId.Contains(x.BillId))
                .ToList();

            // 4. Group dữ liệu theo ngày để tính tổng doanh thu
            var dataByDate = billThanhCong
                .GroupBy(x => x.Created.Date)
                .Select(g =>
                {
                    var billIdsInDay = g.Select(x => x.Id).ToList();
                    var deliveryFee = billDeliveries
                        .Where(d => billIdsInDay.Contains(d.BillId))
                        .Sum(d => d.DeliveryFee);

                    return new DanhThuDto
                    {
                        Ngay = g.Key,
                        TongDoanhThu = g.Sum(x => x.TotalAmount - x.TotalAmountEndow - x.DiscountAmount) + deliveryFee
                    };
                })
                .ToDictionary(x => x.Ngay, x => x);

            // 5. Trả kết quả: nếu ngày không có dữ liệu thì TongDoanhThu = 0
            var danhThuList = allDates.Select(date =>
                dataByDate.ContainsKey(date)
                    ? dataByDate[date]
                    : new DanhThuDto
                    {
                        Ngay = date,
                        TongDoanhThu = 0
                    })
                .ToList();

            return danhThuList;
        }

    }
}
