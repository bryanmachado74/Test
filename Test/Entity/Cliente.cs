using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Entity
{
    public class Cliente
    {
        [Display(Name = "No. Cliente")]
        public int id { get; set; }

        [Display(Name = "Nombre")]
        public int nombre { get; set; }

        [Display(Name = "Balance")]
        public int fondos { get; set; }
    }//end class
}//end namespace