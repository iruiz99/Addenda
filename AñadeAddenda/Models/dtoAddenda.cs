using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Addendas.Models
{
    public class dtoAddenda
    {
        dbConection dbCn = new dbConection();
        public int IDADDENDA { get; set; }
        public string NOMBREA { get; set; }
        public char STSADD { get; set; }

        //public dtoAddenda()
        //{
        //    this.IDADDENDA = -1;
        //    this.NOMBREA = null;
        //}
        public dtoAddenda() { }
        public dtoAddenda(DataRow row)
        {
            this.IDADDENDA = int.Parse(row["IDADDENDA"].ToString());
            this.NOMBREA = row["NOMBREA"].ToString();
        }
    }
}