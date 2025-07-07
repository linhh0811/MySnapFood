﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.Momo
{
    public class MomoCreatePaymentResponseModel
    {
        public string RequestId { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string LocalMessage { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public string PayUrl { get; set; } = string.Empty;
        public string Signature { get; set; } = string.Empty;
        public string QrCodeUrl { get; set; } = string.Empty;
        public string Deeplink { get; set; } = string.Empty;
        public string DeeplinkWebInApp { get; set; } = string.Empty;
    }
}
