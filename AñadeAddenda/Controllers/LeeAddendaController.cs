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
            List<Models.dtoCapturasManuales> LstCManual = new List<Models.dtoCapturasManuales>();
            ToolsXML tools = new ToolsXML();
            List<dtoParametros> lista = new List<dtoParametros>();
            var tuplas = tools.GetCapturasManuales(lista, IdAddenda);

            Models.dtoCapturasManuales dto = new Models.dtoCapturasManuales();

            foreach (var nodo in tuplas.Item2)
            {
                if (dto.MostrarCaptura == true)
                {
                    dto.Id = nodo.Id;
                    dto.Tipo = nodo.Tipo;
                    dto.Nombre = nodo.Nombre;
                    dto.Valor = nodo.Valor;
                    dto.MostrarCaptura = nodo.MostrarCaptura;
                    //LstCManual.Add(dto);
                }
                else
                {/*no capturable*/}
                dto.Lista = nodo.Lista;
                foreach (var atr in nodo.Lista)
                {
                    if (dto.Nombre != null)
                    {
                        dto.Id = atr.Id;
                        dto.Tipo = atr.Tipo;
                        dto.Nombre = atr.Nombre;
                        dto.Valor = atr.Valor;
                        dto.MostrarCaptura = atr.MostrarCaptura;

                        LstCManual.Add(dto);
                    }
                    else
                    { /*no capturable*/}
                }
            }
            return View(dto);
        }
    }
}