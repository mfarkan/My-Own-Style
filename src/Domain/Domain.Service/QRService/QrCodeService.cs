using Core.Extensions.Configuration;
using Domain.Service.Model.QRCode;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.QRService
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IConfiguration _config;
        public QrCodeService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public Task ConfirmQRCodeAsync()
        {
            throw new NotImplementedException();
        }

        public QRCodeResponseDTO GenerateQRCodeAsync(string plainText)
        {
            var remainingTime = _config.GetQRCodeRemainingTime();
            string base64String = string.Empty;
            using (QRCodeGenerator generator = new QRCodeGenerator())
            using (var qrCodeData = generator.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.M))
            using (var qrCode = new QRCode(qrCodeData))
            using (var qrImage = qrCode.GetGraphic(20))
            using (var stream = new MemoryStream())
            {
                qrImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] byteToImage = stream.ToArray();
                base64String = Convert.ToBase64String(byteToImage);
            }
            return new QRCodeResponseDTO
            {
                Base64StringQRCode = base64String,
                RemainingTime = remainingTime,
            };
        }
    }
}
