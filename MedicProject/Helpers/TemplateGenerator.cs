using MedicProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicProject.Helpers
{
    public static class TemplateGenerator
    {
        [STAThread]
        public static string GetHTMLString(User user)
        {
            var employees = user;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat(@"
                <html>
                    <head>
                   
                    </head>
                <body>
                 <div class='header'>
        <div class='first_line'>
            <p>Judetul: </p><p class='info' style='width:40%'>&nbsp{0}</p>
            <p>luna: </p><p class='info'>&nbsp{1}</p>
            <p>ziua:</p><p class='info'>&nbsp{2}</p>
        </div>
        <div class='first_line'>
            <p>Localitatea: </p><p class='info' style='width:100%'>&nbsp{3}</p>
        </div>
        <div class='first_line'>
            <p>Unit. sanitara: </p><p class='info' style='width:84%'>&nbsp{4}</p>
        </div>
        </div>
        <h2 class='title'>BILET DE TRIMITERE</h2>
        <div class='first_line'>
            <p>catre: </p> <p class='info' style='width:100%'>&nbsp{5}</p>
        </div>
        <div class='name'>
            <p>Numele: </p><p class='info' style='width:50%'>&nbsp{6}</p>
            <p>Prenumele:</p><p class='info' style='width:50%'>&nbsp{7} </p>
        </div>
        <div class='name'>
            <p>Sexul: </p><p class='info' style='width:30%'>&nbsp{8}</p>
            <p>varsta:</p><p class='info' style='width:35%'>&nbsp{9}</p>
            <p>cu domiciliu in:</p>
        </div>
        <div class='name'>
            <p>Judetul:</p><p class='info' style='width:59%'>&nbsp{10}</p>
            <p>Localitatea:</p><p class='info' style='width:100%'>&nbsp{11}</p>
        </div>
        <div class='loc'>
            <p>Strada:</p><p class='info'>&nbsp{12}</p>
                <p>Numarul:</p><p class='info'>&nbsp{13}</p>
                <p>Apartament:</p><p class='info'>&nbsp</p>
                <p>Scara:</p><p class='info'>&nbsp</p>
                </div>
        <div class='name'>
            <p style='width: 190px;'>Diagnostic prezumtiv:</p><p class='info' style='width:74%'>&nbsp{14}</p>
        </div>
        <div class='name'>
            <p style='width: 150px;'>Motivul trimiterii:</p><p class='info' style='width:79%'>&nbsp{15}</p>
        </div>
        <div class='name'>
            <p style='width: 200px;'>Investigatii si tratament:</p><p class='info' style='width:72%'>&nbsp{16}</p>
        </div>
        <br></br>
        <div class='semnatura'>
            <div>Semnatura medic</div>
            <div style='text-align:center;'>{17}</div>
        </div>
        </body>
        </html>
            ", "  " + user.County, "  " + DateTime.Now.Month,  "  " + DateTime.Now.Day,  "  " + user.City, " MedClinic", " specialist",
             "  " + user.lastName,  "  " + user.firstName, "  M", user.Getage(),
              "  " + user.County,  "  " + user.City,  "  " + user.Street,  "  " + user.HomeNumber,
            "  Cancer", "  Test COVID", "-", "Vladutz");


            return stringBuilder.ToString();
        }
    }
}
