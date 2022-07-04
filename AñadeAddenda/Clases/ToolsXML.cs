using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using AñadeAddenda.Models;

public class ToolsXML : dbConection
{
    //PASO 1 CREA DTO Y PARAMETROS DE LA FACTURA
    public dtoFactura GetRegistroAProcesar(string Serie, string Folio)
    {
        dtoFactura dto = new dtoFactura();
        dto = new dtoFactura();

        DataTable dt = QueryDatos("SELECT * FROM PMS2000.CFDAHD WHERE SERIE = '" + Serie + "' AND  FOLIO = '" + Folio + "' ");

        foreach (DataRow registro in dt.Rows)
        {
            dto.NumCia = Convert.ToInt32(registro["NumCia"].ToString());
            dto.NumSuc = Convert.ToInt32(registro["Numsuc"]);
            dto.CveAge = Convert.ToInt32(registro["CveAge"]);
            dto.Numdoe = Convert.ToInt32(registro["Numdoe"]);
            dto.NuClFa = Convert.ToInt32(registro["NuClFa"]);
            dto.Serie = registro["Serie"].ToString().Trim();
            dto.Folio = registro["Folio"].ToString().Trim();
            dto.NumDoo = Convert.ToInt32(registro["NumDoo"].ToString());
            dto.Fecha = ExtreFecha(registro["Fecha"].ToString());
            dto.TipDoe = registro["TipDoe"].ToString().Trim();
            dto.SigDoc = Convert.ToInt32(registro["SigDoc"].ToString());

            dtoParametros parametro;

            foreach (DataColumn columna in registro.Table.Columns)
            {
                parametro = new dtoParametros();

                if (columna.DataType.Name.Substring(0, 1) == "S")
                {
                    //String
                    parametro.Tipo = "S-" + columna.ColumnName;
                    parametro.Parametro = columna.ColumnName + " = '" + registro[columna.ColumnName].ToString().Trim() + "'";
                    parametro.Campo = columna.ColumnName;
                    parametro.Valor = registro[columna.ColumnName].ToString().Trim();
                }
                else
                {
                    if (columna.ColumnName == "FECHA")
                    {
                        parametro.Tipo = "F-" + columna.ColumnName;
                        parametro.Parametro = columna.ColumnName + " = '" + dto.Fecha.ToString("yyyy-MM-dd") + "'";
                        parametro.Campo = columna.ColumnName;
                        parametro.Valor = dto.Fecha.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        //NUMERICS
                        parametro.Tipo = "N-" + columna.ColumnName;
                        parametro.Parametro = columna.ColumnName + " = " + registro[columna.ColumnName].ToString() + "";
                        parametro.Campo = columna.ColumnName;
                        parametro.Valor = registro[columna.ColumnName].ToString().Trim();
                    }
                }
                dto.ListaDeParametros.Add(parametro);
            }
            parametro = new dtoParametros();
            parametro.Tipo = "S-SERIEFOLIO";
            parametro.Parametro = "SERIEFOLIO = '" + dto.Serie + dto.Folio + "'";
            parametro.Campo = "SERIEFOLIO";
            parametro.Valor = dto.Serie + dto.Folio;

            dto.ListaDeParametros.Add(parametro);

            break;
        }
        return dto;
    }
    //Paso 2 Buscar las capturas de la addenda y las manda a pintar en la vista para la captura
    //public Tuple<bool, List<dtoCapturasManuales>> GetCapturasManuales(List<dtoParametros> lista, int idAddenda)
    //{
    //    List<dtoCapturasManuales> ListaDeCapturas = new List<dtoCapturasManuales>();
    //    dtoCapturasManuales dtoCaptura; 

    //    DataTable dt = QueryDatos("SELECT * FROM PMS2000.CFDIANODO WHERE STSNODO = 'A' AND IDADDENDA = " + idAddenda);

    //    foreach (DataRow row in dt.Rows)
    //    {
    //        dtoCaptura = new dtoCapturasManuales();
    //        dtoCaptura.Id = Convert.ToInt32(row["IdNodo"].ToString());
    //        dtoCaptura.Nombre = row["NombreN"].ToString().Trim();
    //        dtoCaptura.Valor = "";
    //        dtoCaptura.Tipo = "NODO";
    //        if (row["Captura"].ToString() == "S")
    //            dtoCaptura.MostrarCaptura = true;
    //        else
    //        {
    //            dtoCaptura.MostrarCaptura = false;
    //        }
    //        dtoCaptura.Lista = CapturaAtributos(dtoCaptura.Id);

    //        if (dtoCaptura.MostrarCaptura || dtoCaptura.Lista != null)
    //        {
    //            ListaDeCapturas.Add(dtoCaptura);
    //        }
    //    }
    //    if (ListaDeCapturas.Count > 0)
    //    return new Tuple<bool, List<dtoCapturasManuales>>(true, ListaDeCapturas);
    //    else
    //        return new Tuple<bool, List<dtoCapturasManuales>>(false, null);

    //}
    public List<dtoCapturasManuales> GetCapturasManuales(List<dtoParametros> lista, int idAddenda)
    {
        List<dtoCapturasManuales> ListaDeCapturas = new List<dtoCapturasManuales>();
        dtoCapturasManuales dtoCaptura;

        DataTable dt = QueryDatos("SELECT * FROM PMS2000.CFDIANODO WHERE STSNODO = 'A' AND IDADDENDA = " + idAddenda);

        foreach (DataRow row in dt.Rows)
        {
            if (row["ESCONCEPTO"].ToString() == "N")
            {
                dtoCaptura = new dtoCapturasManuales();
                dtoCaptura.Id = Convert.ToInt32(row["IdNodo"].ToString());
                dtoCaptura.Nombre = row["NombreN"].ToString().Trim();
                dtoCaptura.Valor = "";
                dtoCaptura.Tipo = "NODO";
                if (row["Captura"].ToString() == "S")
                    dtoCaptura.MostrarCaptura = true;
                else
                {
                    dtoCaptura.MostrarCaptura = false;
                }
                dtoCaptura.Lista = CapturaAtributos(dtoCaptura.Id);

                if (dtoCaptura.MostrarCaptura || dtoCaptura.Lista != null)
                {
                    ListaDeCapturas.Add(dtoCaptura);
                }
            }
        }
        if (ListaDeCapturas.Count > 0)
            return ListaDeCapturas;
        else
            return null;

    }
    private List<dtoCapturasManuales> CapturaAtributos(int IdNodo )
    {
        List<dtoCapturasManuales> ListaResult = new List<dtoCapturasManuales>();
        DataTable dt = QueryDatos("Select * from PMS2000.CFDIAATR WHERE STSADDA = 'A' AND IDNODO = " + IdNodo);
        dtoCapturasManuales dtoCaptura;
        foreach (DataRow row in dt.Rows)
        {
            dtoCaptura = new dtoCapturasManuales();
            if (row["CAPTURAR"].ToString() == "S")
            {
                dtoCaptura.Id = Convert.ToInt32(row["IDATR"].ToString());
                dtoCaptura.Nombre = row["NATRIBUTO"].ToString().Trim();
                dtoCaptura.Valor = "";
                dtoCaptura.Tipo = "ATRIBUTO";
                dtoCaptura.MostrarCaptura = true;
            }
            else
            {
                dtoCaptura.MostrarCaptura = false;
            }
            ListaResult.Add(dtoCaptura);
        }
        if (dt.Rows.Count > 0)
            return ListaResult;
        else
            return null;
    }

    //DA FORMATO A LA FECHA NUMERICA 20220624 => '2022-06-24'
    public DateTime ExtreFecha(string Fecha)
    {
        string valor = Fecha.Substring(0, 4) + "-" + Fecha.Substring(4, 2) +  "-" + Fecha.Substring(6, 2);
        return Convert.ToDateTime(valor);
    }
    //paso 3            
    public string ConsultaAtributos(int idNodo, string nombreNodo, List<dtoParametros> lista, List<dtoCapturasManuales> ListaDeCapturas = null)
    {        
        DataTable dt =  QueryDatos("SELECT * FROM  PMS2000.CFDIAATR WHERE STSADDA = 'A' AND IDNODO = " + idNodo + " ORDER BY IDATR");

        StringBuilder sbAtributos = new StringBuilder();
        foreach (DataRow item in dt.Rows)
        {
            if (item["NATRIBUTO"].ToString().Contains("="))//Nos idica que el valor es total
                sbAtributos.Append(" " + item["NATRIBUTO"].ToString().Trim());

            else if (item["IdFactura"].ToString() != "0")
            {   //Consulta en PMS2000.CFDIADD1 => VALORES GENERALE DE LA FACTURA"
                sbAtributos.Append(" " + item["NATRIBUTO"].ToString().Trim() + "=");
                sbAtributos.Append(" \"" + ConsultaPorParametros("CFDIADD1", "IDFACTURA", Convert.ToInt32(item["IdFactura"].ToString()), lista).Item2 + "\"");
            }
            else if (item["IdConcepto"].ToString() != "0")
            {
                //Consulta en PMS2000.CFDIADD1 => VALORES DEL CONCEPTO"
                sbAtributos.Append(" " + item["NATRIBUTO"].ToString().Trim() + "=");
                sbAtributos.Append(" \"" + ConsultaPorParametros("CFDIADD2", "IDconcepto", Convert.ToInt32(item["idConcepto"].ToString()), lista).Item2 + "\"");
            }
            else if (item["Clase"].ToString().Trim() != "")
            {//Inmporte con Letra                
                sbAtributos.Append(" " + item["NATRIBUTO"].ToString().Trim() + "=");
                sbAtributos.Append(" \"" + ImporteConLetra(lista) + "\"");
            }
            else if (item["CAPTURAR"].ToString().Trim() == "S")
            {
                //ENVIAR MENSAJE DE CREAR CAJA DE TEXTO CON VALOR DEL CAMPO
                sbAtributos.Append(" " + item["NATRIBUTO"].ToString().Trim() + "=");
                sbAtributos.Append(" \"" + ListaDeCapturas.Find(x => x.Nombre == item["NATRIBUTO"].ToString().Trim()).Valor + "\"");
            }
        }
        return "";
    }
    public string ImporteConLetra(List<dtoParametros> lista)
    {
        string valor = ConsultaPorParametros("CFDIADD1", "IDFACTURA", 5, lista).Item2;
        return csNumeroATexto.Invocar(valor);
    }
    public string ObtenerRuta(int numcia, int numsuc, int cveage, int numdoe)
    {
        string ruta = "";

        ruta += String.Format("{0:00}", numcia) + String.Format("{0:00}", numsuc) +
                        String.Format("{0:00}", cveage) + "D" + String.Format("{0:0000000}",
                        numdoe) + "\\";
        return ruta;
    }   
    public Tuple<bool, string> ConsultaPorParametros(string sTabla,  string sCampo,  int iId, List<dtoParametros> Lista)
    {
        Tuple<bool, string> tRespuesta = null;
        StringBuilder sbSql = new StringBuilder();
        sbSql = new StringBuilder();
        StringBuilder sbParametros = new StringBuilder();
        sbSql.AppendLine("Select * From PMS2000." + sTabla  + "  Where " + sCampo + " = " + iId);
        DataTable dt = QueryDatos(sbSql.ToString());

        string[] parametros = dt.Rows[0]["Parametro"].ToString().Trim().Split(',');

        foreach (string param in parametros)
        {            
            foreach (dtoParametros dto in Lista)
            {
                if (param.Contains("="))
                {
                    sbParametros.Append(" AND " + param);
                    break;
                }
                else if (param.Contains("/"))
                {
                    //cambia el valor de consulta
                    if (param.Trim().Split('/')[1] == dto.Campo)
                    {
                        string sParametroDeSustitucion = param.Split('/')[0].Split('-')[1];
                        if (sbParametros.Length == 0)
                        {
                            sbParametros.Append("WHERE " + dto.Parametro.Replace(dto.Campo, sParametroDeSustitucion));
                        }
                        else
                            sbParametros.Append(" AND " + dto.Parametro.Replace(dto.Campo, sParametroDeSustitucion));
                        break;
                    }
                }
                else
                {
                    if (param.Trim() == dto.Tipo)
                    {
                        if (sbParametros.Length == 0)
                        {
                            sbParametros.Append("WHERE " + dto.Parametro);
                        }
                        else
                            sbParametros.Append(" AND " + dto.Parametro);
                        break;
                    }
                }
            }
        }

        sbSql.AppendLine(sbParametros.ToString());

        try
        {
            sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT " + dt.Rows[0]["CAMPO"].ToString().Trim().ToUpper() + " FROM " + dt.Rows[0]["DATBASE"].ToString().Trim().ToUpper() + "." + dt.Rows[0]["TABLE"].ToString().Trim().ToUpper());
            sbSql.AppendLine(sbParametros.ToString());
            string valor = QueryEscalar(sbSql.ToString());

            tRespuesta = new Tuple<bool, string>(true, valor);
            
        }
        catch (Exception ex)
        {
            tRespuesta = new Tuple<bool, string>(false, ex.Message);
        }

        return tRespuesta;

    }
    public string ConsultaParametroDTO(string campo, List<dtoParametros> Lista)
    {
        return Lista.Find(x => x.Campo.ToUpper() == campo.ToUpper()).Valor;
    }
    public DataTable DtAtributos(int IDNODO)
    {
        return QueryDatos("Select * from PMS2000.CFDIAATR WHERE IDNODO = " + IDNODO);
    }
    //private DataTable DtServicio(dtoServicio Servicio, List<dtoParametros> Lista)
    //{
    //    StringBuilder sbSql = new StringBuilder();
    //    sbSql.AppendLine(string.Format(Servicio.Operacion));
    //    return QueryDatos("");
    //}

    /// <summary>
    /// Regresa un string con los parametros a consultar
    /// </summary>
    /// <param name="Argumento">Ejemplo 1: N-NUMCIA, N-NUMSUC; EJEMPLO 2: NUMCIA, NUMSUC</param>
    /// <param name="Lista">LISTADO DE PARAMETROS</param>
    /// <param name="tipoDeRetorno">PARAMETROS</param>
    /// <returns></returns>
    private string ParametrosValores(string Argumento, List<dtoParametros> Lista, int tipoDeRetorno)
    {
        string[] parametros = Argumento.Trim().Split(',');
        StringBuilder sbParametros = new StringBuilder();
        foreach (string param in parametros)
        {
            foreach (dtoParametros dto in Lista)
            {
                if (tipoDeRetorno == 1)
                {
                    //Regresa un Where con parametros
                    if (param.Contains("="))
                    {
                        sbParametros.Append(" AND " + param);
                        break;
                    }
                    else if (param.Contains("/"))
                    {
                        //cambia el valor de consulta
                        if (param.Trim().Split('/')[1] == dto.Campo)
                        {
                            string sParametroDeSustitucion = param.Split('/')[0].Split('-')[1];
                            if (sbParametros.Length == 0)
                                sbParametros.Append("WHERE " + dto.Parametro.Replace(dto.Campo, sParametroDeSustitucion));
                            else
                                sbParametros.Append(" AND " + dto.Parametro.Replace(dto.Campo, sParametroDeSustitucion));
                            break;
                        }
                    }
                    else
                    {
                        if (param.Trim() == dto.Tipo)
                        {
                            if (sbParametros.Length == 0)
                                sbParametros.Append("WHERE " + dto.Parametro);
                            else
                                sbParametros.Append(" AND " + dto.Parametro);
                            break;
                        }
                    }                    
                }
                else
                {
                    //Regresa una lista de valores eparadas por comas
                    if (param.Trim() == dto.Campo)
                    {
                        if (sbParametros.Length == 0)
                            sbParametros.Append(dto.Valor);
                        else
                            sbParametros.Append(", " + dto.Valor);
                        break;
                    }
                }
            }
        }

        return sbParametros.ToString();
    }

    /// <summary>
    /// Extrae la ruta física de los archivos descargados
    /// </summary>
    /// <param name="dto">Datos del CFDI</param>
    /// <param name="sExtención"> XML, PDF, Html</param>
    /// <returns></returns>
    public string GetRutaCFDILocalServer(dtoFactura dto, string sExtención)
    {
        return ObtenerRuta(dto.NumCia, dto.NumSuc, dto.CveAge, dto.Numdoe) + dto.Folio + "_" + dto.Serie + "." + sExtención;
    }
}