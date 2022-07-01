using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Data.OleDb;
using DataBasePMS;
using System.Collections.Generic;

public class dbConection
{
    string msgError = "";
    string nombreDelMetodo = "";
    string strSql = "";
    ConexionOleDB cn = new ConexionOleDB(PMStrConnOleDB.PMS2000);

    /// <summary>
    /// Ejectuta una consulta de tipo "Select"  a la Base de Datos
    /// </summary>
    /// <param name="sql">String Tipo Consulta Tipo Select * from Tabla</param>
    /// <returns>Retorna un DataTable con los registros que se consultaron</returns>
    public DataTable QueryDatos(string sql)
    {
        msgError = string.Empty;
        nombreDelMetodo = "QueryDatos";
        strSql = sql;
        DataTable datos = new DataTable();
        try
        {
            cn.AbreConexionOleDB();
            int r = cn.ExecuteQueryOleDB(sql);
            if (r > 0)
            {
                datos = cn.TablaResultadosOleDB;
            }
            else if (r == 0)
            {
                msgError = "No se encontraron registros en la consulta";
            }
            else
            {
                datos = null;
                msgError = cn.ExceptionOleDB.Message;
            }
        }
        catch (Exception ex)
        {
            datos = null;
            msgError = ex.Message;
        }
        finally
        {
            cn.CierraConexionOleDB();
        }
        return datos;
    }

    public DataTable QueryTable(string sql)
    {
        msgError = string.Empty;
        nombreDelMetodo = "QueryDatos";
        strSql = sql;
        DataTable datos = new DataTable();
        try
        {
            cn.AbreConexionOleDB();
            int r = cn.ExecuteQueryOleDB(sql);
            if (r >= 0)
            {
                datos = cn.TablaResultadosOleDB;
            }
            else
            {
                datos = null;
                msgError = cn.ExceptionOleDB.Message;
            }
        }
        catch (Exception ex)
        {
            datos = null;
            msgError = ex.Message;
        }
        finally
        {
            cn.CierraConexionOleDB();
        }
        return datos;
    }

    public string QueryEscalar(string sql)
    {
        msgError = string.Empty;
        nombreDelMetodo = "QueryEscalar";
        strSql = sql;
        string datos = string.Empty;
        try
        {
            cn.AbreConexionOleDB();
            var result = cn.ExecuteScalarOleDB(sql);
            if (result != null)
                datos = result.ToString();

        }
        catch (Exception ex)
        {
            datos = string.Empty;
            msgError = ex.Message;
        }
        finally
        {
            cn.CierraConexionOleDB();
        }
        return datos;
    }


    /// <summary>
    /// Ejecuta un Query tipo Update, Delete, Insert, etc que modifica los regristros de una tabla
    /// </summary>
    /// <param name="sql">Query tipo: Inser into + Tabla, Update + Tabla, Delete from + Tabla</param>
    /// <returns>Retorna True si el Query se ejecutó correctamente</returns>
    public bool QueryExecute(string sql)
    {
        msgError = "";
        bool respuesta = false;
        nombreDelMetodo = "QueryExecute";
        strSql = sql;
        try
        {
            cn.AbreConexionOleDB();
            int r = cn.ExecuteNonQueryOleDB(sql);

            if (r >= 0)
            {
                respuesta = true;
            }
            else
            {
                msgError = cn.ExceptionOleDB.Message;
            }
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
        return respuesta;
    }

    /// <summary>
    /// Ejecuta un Query tipo Update, Delete, Insert, etc que modifica los regristros de una tabla, usando un OleDBCommand
    /// </summary>
    /// <param name="sql">Query tipo: Inser into + Tabla, Update + Tabla, Delete from + Tabla</param>
    /// <returns>Retorna True si el Query se ejecutó correctamente</returns>
    public bool QueryExecuteCommand(OleDbCommand sql)
    {
        bool respuesta = false;
        msgError = string.Empty;
        nombreDelMetodo = "QueryExecute";

        try
        {
            cn.AbreConexionOleDB();
            int r = cn.ExecuteNonQueryOleDB(sql);

            if (r >= 0)
            {
                respuesta = true;
            }
            else
            {
                msgError = cn.ExceptionOleDB.Message;
            }
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
        return respuesta;
    }

    /// <summary>
    /// Ejecuta un Query tipo Update, Delete, Insert, etc que modifica los regristros de una tabla
    /// </summary>
    /// <param name="sql">Query tipo: Inser into + Tabla, Update + Tabla, Delete from + Tabla</param>
    /// <returns>Número de registros modificados y si es -1 existió un errror en la consulta</returns>
    public int QueryExecute2(string sql)
    {
        int respuesta = 0;
        msgError = string.Empty;
        nombreDelMetodo = "QueryExecute";
        strSql = sql;
        try
        {
            cn.AbreConexionOleDB();
            int r = cn.ExecuteNonQueryOleDB(sql);

            if (r >= 0)
            {
                respuesta = r;
            }
            else
            {
                r = -1;
                msgError = cn.ExceptionOleDB.Message;
            }
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
        return respuesta;
    }


    /// <summary>
    /// Extrae los datos de un archivo de excel para alta o modificación de productos
    /// </summary>
    /// <param name="rutaArchivoSLX">Ruta del archivo</param>
    /// <param name="hojaXLS">Hola a leer</param>
    /// <returns>retorn una tabla de resultados</returns>
    public DataTable DatosExcel(string rutaArchivoSLX, string hojaXLS)
    {
        msgError = string.Empty;
        nombreDelMetodo = "DatosExcel";

        ConexionOleDB cnExcel = new ConexionOleDB(@StrConexionXLS(rutaArchivoSLX));
        DataTable dtsExcel = new DataTable();


        try
        {
            cnExcel.AbreConexionOleDB();
            int totRegistros = 0;

            string sql = "";

            sql = "Select *  from [" + hojaXLS + "$] ORDER BY CVECVE, MODELO ";

            totRegistros = cnExcel.ExecuteQueryOleDB(sql);

            if (totRegistros > 0)
            {
                dtsExcel = cnExcel.TablaResultadosOleDB;
            }
            else
            {
                msgError = "Error al extrar los registros de excel\n" + cnExcel.ExceptionOleDB.Message;
            }

        }
        catch (Exception ex)
        {
            msgError = "Error al extrar los registros de excel\n" + ex.Message;
        }
        finally
        {
            cnExcel.CierraConexionOleDB();
        }
        return dtsExcel;
    }


    /// <summary>
    /// Extrae los datos de un archivo de excel de lista de precios 
    /// </summary>
    /// <param name="rutaArchivoSLX">Ruta del archivo</param>
    /// <param name="hojaXLS">Hola a leer</param>
    /// <returns>retorn una tabla de resultados</returns>
    public DataTable DatosExcel(string rutaArchivoSLX, string hojaXLS, string sql)
    {
        msgError = string.Empty;
        nombreDelMetodo = "DatosExcel";

        ConexionOleDB cnExcel = new ConexionOleDB(@StrConexionXLS(rutaArchivoSLX));
        DataTable dtsExcel = new DataTable();

        try
        {
            cnExcel.AbreConexionOleDB();
            int totRegistros = 0;

            totRegistros = cnExcel.ExecuteQueryOleDB(sql);

            if (totRegistros > 0)
            {
                dtsExcel = cnExcel.TablaResultadosOleDB;
            }
            else
            {
                msgError = "Error al extrar los registros de excel\n" + cnExcel.ExceptionOleDB.Message;
            }

        }
        catch (Exception ex)
        {
            msgError = "Error al extrar los registros de excel\n" + ex.Message;
        }
        finally
        {
            cnExcel.CierraConexionOleDB();
        }
        return dtsExcel;
    }


    /// <summary>
    /// Ejecuta una Actualización o insersión en  excel  
    /// </summary>
    /// <param name="rutaArchivoSLX">Ruta del archivo</param>
    /// <param name="sql">sql que ejecutar</param>
    /// <returns>retorn true si se ejecuto correctamente</returns>
    public bool EjecutaUpdateInsertExcel(string rutaArchivoSLX, string sql)
    {
        msgError = string.Empty;
        nombreDelMetodo = "DatosExcel";

        bool respuesta = false;

        ConexionOleDB cnExcel = new ConexionOleDB(@StrConexionXLS(rutaArchivoSLX));

        try
        {
            cnExcel.AbreConexionOleDB();
            int totRegistros = 0;

            totRegistros = cnExcel.ExecuteNonQueryOleDB(sql);

            if (totRegistros > 0)
            {
                respuesta = true;
            }
            else
            {
                msgError = "Error al extrar los registros de excel\n" + cnExcel.ExceptionOleDB.Message;
            }

        }
        catch (Exception ex)
        {
            msgError = "Error al extrar los registros de excel\n" + ex.Message;
        }
        finally
        {
            cnExcel.CierraConexionOleDB();
        }
        return respuesta;
    }



    /// <summary>
    /// Extrae los datos de un archivo de excel para alta o modificación de productos
    /// </summary>
    /// <param name="rutaArchivoSLX">Ruta del archivo</param>
    /// <param name="hojaXLS">Hola a leer</param>
    /// <returns>retorn una tabla de resultados</returns>
    //public int InsertaExcel(string rutaArchivoSLX, string hojaXLS, dtsCfdaXML dts)
    //{
    //    nombreDelMetodo = "InsertaExcel";
    //    ConexionOleDB cnExcel = new ConexionOleDB(@StrConexionXLS(rutaArchivoSLX));
    //    DataTable dtsExcel = new DataTable();
    //    int totRegistros = 0;

    //    try
    //    {
    //        cnExcel.AbreConexionOleDB();


    //        string sql = "";

    //        sql = "Insert into [" + hojaXLS + "$] values (" + dts.NumCia.ToString() + ", " + dts.NumSuc.ToString() + ", ";
    //        sql += dts.CveAge.ToString() + ", " + dts.CanVen.ToString() +  ", " + dts.CanSup.ToString() + ", ";
    //        sql += "'" + dts.Zona + "', '" + dts.NomSuc + "', " + dts.Fecha.ToString() + ", " + dts.NumDoe.ToString() + ", ";
    //        sql += "'" + dts.UuId + "', '" + dts.Carpeta + "', '" + dts.Folio_Serie + "', '" + dts.RfcRep + "', '" + dts.Sts + "', ";
    //        sql += dts.SubTotal.ToString() + ", " + dts.Iva.ToString() + ", ";
    //        sql += dts.Monto.ToString() + ", '" + dts.Moneda + "', " + dts.Change.ToString() + ", " + dts.Mes.ToString() + ", ";
    //        sql += dts.Anio.ToString() + ")";


    //        totRegistros = cnExcel.ExecuteNonQueryOleDB(sql);

    //        if (totRegistros > 0)
    //        {
    //            dtsExcel = cnExcel.TablaResultadosOleDB;
    //        }
    //        else
    //        {
    //             msgError = "Error al insertar los registros de excel\n" + cnExcel.ExceptionOleDB.Message;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        msgError = "Error al extrar los registros de excel\n" + ex.Message;
    //    }
    //    finally
    //    {
    //        cnExcel.CierraConexionOleDB();
    //    }
    //    return totRegistros;
    //}

    /// <summary>
    /// Genera cadena de conexión para archivos de Excel.
    /// </summary>
    /// <param name="rutaXLS">Ruta del archivo de Excel</param>
    /// <returns>String con la cadena de conexión de Excel</returns>
    public string StrConexionXLS(string rutaXLS)
    {
        nombreDelMetodo = "StrConexionXLS";
        //return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rutaXLS + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=0;'";
        //return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaXLS + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
        return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaXLS + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
    }

    /// <summary>
    /// Optiene el error que genero el método
    /// </summary>
    /// <returns>Optiene el error que genero el método</returns>
    public string GetError()
    {
        string mensaje = "";
        if (!string.IsNullOrEmpty(msgError))
        {
            mensaje = "Nombre del Método: " + nombreDelMetodo + "\n\n";
            mensaje += msgError + "\n\n";
            mensaje += "Query: " + strSql;
        }
        return mensaje;
    }

    /// <summary>
    /// Obtiene una lista del nombre de los campos de una tabla
    /// </summary>
    /// <param name="sql">Debe de indicar el nombre de la tabla</param>
    /// <param name="strConexion">Cadena de conexion de la tabla</param>
    /// <returns>Retorna un Objeto List con los nombres de los campos existentes en la tabla</returns>
    public List<string> NombresDeCamposOleDB(string sql, string strConexion)
    {
        msgError = string.Empty;
        nombreDelMetodo = "NombresDeCamposOleDB";

        List<string> listaCampos = new List<string>();

        OleDbConnection cn = new OleDbConnection();
        cn.ConnectionString = strConexion;

        try
        {
            cn.Open();
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = cn;
            comando.CommandText = sql;

            OleDbDataReader dataRader = comando.ExecuteReader();

            DataTable esquema = new DataTable();
            esquema = dataRader.GetSchemaTable();
            int contador = 0;
            foreach (DataRow campo in esquema.Rows)
            {
                contador++;

                if (campo[0].ToString().ToUpper().Trim() != "F" + contador)
                    listaCampos.Add(campo[0].ToString());
            }

        }
        catch (Exception ex)
        {
            msgError = ex.Message;
        }
        finally
        {
            cn.Close();
        }

        return listaCampos;
    }

}
