using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class dtoEmpresa
{
    private int _numCia = 0;
    public int NumCia
    {
        get { return _numCia; }
        set { _numCia = value; }
    }

    private string _nombre = "";
    public string Nombre
    {
        get { return _nombre; }
        set { _nombre = value; }
    }

    private string _rfc = "";
    public string Rfc
    {
        get { return _rfc; }
        set { _rfc = value; }
    }

    private string _directorio = "";
    public string Directorio
    {
        get { return _directorio; }
        set { _directorio = value; }
    }

    private string _dirProgramaFE = "";
    public string DirProgramaFE
    {
        get { return _dirProgramaFE; }
        set { _dirProgramaFE = value; }
    }

    private string _dirProgramaWeb = "";
    public string DirProgramaWeb
    {
        get { return _dirProgramaWeb; }
        set { _dirProgramaWeb = value; }
    }

    private string _usuario = "";

    public string Usuario
    {
        get { return _usuario; }
        set { _usuario = value; }
    }

    private string _requestor = "";
    public string Requestor
    {
        get { return _requestor; }
        set { _requestor = value; }
    }

    private string _version = "";

    public string Version
    {
        get { return _version; }
        set { _version = value; }
    }

    private string _codigoPais = "";

    public string CodigoPais
    {
        get { return _codigoPais; }
        set { _codigoPais = value; }
    }

    private string listaDeSucursalesActivas = "";

    public string ListaDeSucursalesActivas
    {
        get { return listaDeSucursalesActivas; }
        set { listaDeSucursalesActivas = value; }
    }

    private string _paginaWeb = "";

    public string PaginaWeb
    {
        get { return _paginaWeb; }
        set { _paginaWeb = value; }
    }

    private string _tablaDeDatos = "";

    public string TablaDeDatos
    {
        get { return _tablaDeDatos; }
        set { _tablaDeDatos = value; }
    }

    private string _Mail = "";
    public string Mail
    {
        get { return _Mail;}
        set { _Mail = value;}
    }

    private string _Password = "";
    public string Password
    {
        get { return _Password;}
        set { _Password = value;}
    }

    private string _Dominio = "";
    public string Dominio
    {
        get { return _Dominio;}
        set { _Dominio = value;}
    }
    public string MailDestino
    {
        get {return _MailDestino; }
        set { _MailDestino = value; }
    }

    private string _MailDestino = "";

    private string _Mail2 = "";
    public string Mail2
    {
        get {return _Mail2;}
        set { _Mail2 = value;}
    }

    private string _Password2 = "";
    public string Password2
    {
        get{return _Password2; }
        set { _Password2 = value;}
    }

    private string _directorioRefacturacion = "";
    /// <summary>
    /// Directorio de refacturacion
    /// </summary>
    public string DirectorioRefacturacion
    {
        get { return _directorioRefacturacion;}
        set { _directorioRefacturacion = value;}
    }
}