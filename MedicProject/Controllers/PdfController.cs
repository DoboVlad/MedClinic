using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using MedicProject.Data;
using MedicProject.DTO;
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

        [HttpGet("generatePDF")]
        public IActionResult CreatePDF(int id, string reason, string treatment, string sendTo, string diagnostic)
        {
            var user = _context.users.FirstOrDefault(user => user.Id == id);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Trimitere",
                Out = @"C:/Users/Vlad/Desktop/generatePDF.pdf"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(user, sendTo, reason, diagnostic, treatment),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "style.css") },
            };


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            _converter.Convert(pdf);

           globalSettings.Out = null;


            var pdf1 = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf1);

            SendEmail(user.email, "Here is you medical letter", "Medical Letter", @"C:/Users/Vlad/Desktop/generatePDF.pdf");
            
            return File(file, "application/pdf");
        }

        private void SendEmail(string emailAddress, string body, string subject, string filename)
        {
            MailMessage mm = new MailMessage("medclinic121@gmail.com", emailAddress);
            mm.Subject = subject;
            mm.Body = body;
            mm.Attachments.Add(new Attachment(filename));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("medclinic121@gmail.com", "parolamedclinic");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }
}