using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AñadeAddenda.Models
{
    public class dtoFactura
    {
        public int NumCia { get; set; }
        public int SigDoc { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public int NumSuc { get; set; }
        public int CveAge { get; set; }
        public int Numdoe { get; set; }
        public string UUID { get; set; }
        public string StsCfd { get; set; }
        public DateTime Fecha { get; set; }
        public string TipDoe { get; set; }
        public int NuClFa { get; set; }
        public int NumDoo { get; set; }

        public List<dtoParametros> ListaDeParametros { get => listaDeParametros; set => listaDeParametros = value; }

        private List<dtoParametros> listaDeParametros = new List<dtoParametros>();
    }
}