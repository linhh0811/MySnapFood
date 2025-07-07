using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.SnapFood.Share.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Service.SnapFood.Share.Model.Momo;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
    }
}
