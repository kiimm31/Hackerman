using CSharpFunctionalExtensions;
using MediatR;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Commands.QR
{
    public class GenerateQrCommand : IRequest<Result<Bitmap>>
    {
        public string InformationToEncode { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }

    public class GenerateQrCommandHandler : IRequestHandler<GenerateQrCommand, Result<Bitmap>>
    {
        public async Task<Result<Bitmap>> Handle(GenerateQrCommand request, CancellationToken cancellationToken)
        {
            var qrCodeImage = await Task.Run(() =>
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(request.InformationToEncode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                return qrCodeImage;
            });

            if (qrCodeImage != null && !qrCodeImage.PhysicalDimension.IsEmpty)
            {
                return Result.FailureIf(Directory.Exists(request.FilePath), qrCodeImage, "Directory doesn't exist");
            }

            return Result.Failure<Bitmap>("Failed to generate QR Code");
        }
    }
}
