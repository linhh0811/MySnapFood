using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos.Pdf;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IConverter _converter;

        public PdfController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpPost("export")]
        public IActionResult ExportPdf([FromBody] HtmlDto dto)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
        new ObjectSettings
        {
            HtmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            font-size: 14px;
                        }}
                    </style>
                </head>
                <body>
                    {dto.Html}
                </body>
                </html>"
        }
    }
            };


            byte[] pdf = _converter.Convert(doc);

            return File(pdf, "application/pdf", "export.pdf");
        }
    }
}
