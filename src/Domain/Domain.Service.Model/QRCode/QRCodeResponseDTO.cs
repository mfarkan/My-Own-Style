using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.QRCode
{
    public class QRCodeResponseDTO
    {
        public string Base64StringQRCode { get; set; }
        public int RemainingTime { get; set; }
    }
}
