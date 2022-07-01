using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class dtoNodosXML
{
    private string estructura = "";
    private string atributos = "";
    private string libreria = "";
    private string campo = "";
    private string parametros = "";
    private bool obligatorio = false;
    private int idServicio = 0;
    public string Estructura { get => estructura; set => estructura = value; }
    public string Atributos { get => atributos; set => atributos = value; }
    public string Libreria { get => libreria; set => libreria = value; }
    public string Campo { get => campo; set => campo = value; }
    public string Parametros { get => parametros; set => parametros = value; }
    public bool Obligatorio { get => obligatorio; set => obligatorio = value; }
    public int IdServicio { get => idServicio; set => idServicio = value; }
}

