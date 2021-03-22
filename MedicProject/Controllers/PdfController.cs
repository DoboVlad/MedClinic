using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using MedicProject.Data;
using MedicProject.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController: ControllerBase
    {
        private readonly IConverter _converter;
        private readonly DatabaseContext _context;

        public PdfController(IConverter converter, DatabaseContext _context)
        {
            this._context = _context;
            this._converter = converter;
        }

        [Route("generatePDF/{id}")]
        public IActionResult CreatePDF(int id)
        {
            var user = _context.users.FirstOrDefault(user => user.Id == id);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Trimitere",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(user),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "style.css") },
            };


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            
            return File(file, "application/pdf");
        }
    }
}