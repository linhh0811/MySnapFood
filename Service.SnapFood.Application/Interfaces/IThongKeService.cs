using Service.SnapFood.Application.Dtos.ThongKe;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IThongKeService
    {
        Task<TTSoLieuDto> GetTTSoLieu();
        TTSoLieuTheoThoiGianDto GetTTSoLieuTheoThoiGian(BaseQuery baseQuery);
        BieuDoSanPhamComboDto GetSanPhamComboDtos(BaseQuery baseQuery);
        List<DanhThuDto> GetDoanhThuDtos(BaseQuery baseQuery);
    }
}
