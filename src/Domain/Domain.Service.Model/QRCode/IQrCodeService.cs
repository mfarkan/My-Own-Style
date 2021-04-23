using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Model.QRCode
{
    public interface IQrCodeService
    {
        QRCodeResponseDTO GenerateQRCodeAsync(string plainText);
        Task ConfirmQRCodeAsync();
    }
}
