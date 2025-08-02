using Service.SnapFood.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IThongTinGiaoHangService
    {
        ThongTinGiaoHangDto GetDuLieu();
        Task<bool> UpdateAsync(Guid id, ThongTinGiaoHangDto item);
    }
}
