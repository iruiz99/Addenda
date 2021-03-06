using AñadeAddenda.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace AñadeAddenda.Controllers
{
    public class LeeAddendaController : Controller
    {
        dbConection dbCn = new dbConection();
        public ActionResult TraerDatos()
        {
            string FolioSerie = "75834 MATRIZ";
            string[] dato = FolioSerie.Split(' ');

            string Folio = dato[0];
            string Serie = dato[1];
            Session["Folio"] = Folio;
            Session["Serie"] = Serie;

            Models.dtoFactura dto = new Models.dtoFactura();
            ToolsXML tools = new ToolsXML();
            dto = tools.GetRegistroAProcesar(Serie, Folio);
            #region Addenda
            DataTable dtAddenda = dbCn.QueryDatos("SELECT * FROM PMS2000.CFDIADDENDA WHERE STSADD = 'A'");

            List<SelectListItem> _AddendaItems = new List<SelectListItem>();

            if (dtAddenda != null)
            {
                foreach (DataRow row in dtAddenda.Rows)
                    _AddendaItems.Add(new SelectListItem
                    {
                        Text = row["NOMBREA"].ToString().ToString().ToUpper(),
                        Value = row["IDADDENDA"].ToString(),
                    });
            }
            _AddendaItems.Add(new SelectListItem { Text = "-- Selecciona --", Value = "-1", Selected = true });

            ViewBag.AddendaItems = _AddendaItems;
            #endregion

            return View(dto);
        }
        public ActionResult CapturasManuales(int ddlAddenda)
        {
            int IdAddenda = ddlAddenda;
            List<dtoCapturasManuales> ListaDeCapturas = new List<dtoCapturasManuales>();
            ToolsXML tools = new ToolsXML();
            List<dtoParametros> lista = new List<dtoParametros>();
            //var tuplas = tools.GetCapturasManuales(lista, IdAddenda);
            ListaDeCapturas = tools.GetCapturasManuales(lista, IdAddenda);

            return View(ListaDeCapturas);
        }
        [HttpPost]
        public ActionResult GuardaFormulario()
        {
            dtoCapturasManuales dto = new dtoCapturasManuales();
            var CapturasManuales = dto.Lista;

            var capturas = dto.Lista;
            ViewData["MyData"] = capturas; // Send this list to the view

            return View(capturas);
            //var prueba3 = Session["Model"];
            //var id = prueba3;

            //var prueba1 = Request.Form;
            //var temp = Server.HtmlEncode(prueba1.ToString());
            //return View(prueba3);
            //return Content(prueba3.ToString());
        }
    }
}