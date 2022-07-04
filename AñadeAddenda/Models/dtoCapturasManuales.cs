using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AñadeAddenda.Models
{
    public class dtoCapturasManuales
    {
        //Nodo ó Atributo [N/A/""]
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public bool MostrarCaptura { get; set; }
        public string Status { get; set; }
        private List<dtoCapturasManuales> lista;
        public List<dtoCapturasManuales> Lista { get => lista; set => lista = value; }
        public IList<dtoCapturasManuales> ValorNew
        {
            get;
            set;
        }
    }
}