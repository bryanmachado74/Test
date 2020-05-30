using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Entity
{
    public class Apuesta
    {
        [Display(Name = "No. Apuesta")]
        public int id { get; set; }

        [Display(Name = "No. Cliente")]
        public int cliente { get; set; }

        [Display(Name = "No. Juego")]
        public int encuentro { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una de las tres opciones")]
        [Display(Name = "Apuesta seleccionada")]
        public string eleccion { get; set; }

        [Required(ErrorMessage = "El monto a apostar debe ser entero y mayor a 1")]
        [Display(Name = "Dolares a apostar")]
        public int monto { get; set; }

        [Display(Name = "Dolares a ganar")]
        public float ganancia { get; set; }

        public Boolean calcularGanacia(float probabilidad) 
        {
            probabilidad = 100 / probabilidad; // convierto a decimal odds 
            try {
                ganancia = (probabilidad * monto) - monto;
                return true;
            } catch {
                return false;
            }
        }

    }//end class
}//end namespace