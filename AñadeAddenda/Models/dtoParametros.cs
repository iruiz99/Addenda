using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class dtoParametros
{
    private string tipo = string.Empty;
    private string parametro = string.Empty;
    private string campo = string.Empty;
    private string valor = string.Empty;

    public string Tipo { get => tipo; set => tipo = value; }
    public string Parametro { get => parametro; set => parametro = value; }
    public string Campo { get => campo; set => campo = value; }
    public string Valor { get => valor; set => valor = value; }
}