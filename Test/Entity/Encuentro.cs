using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Entity
{
    public class Encuentro
    {
        [Display(Name = "No. Encuentro")]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Display(Name = "Equipo Local")]
        public string local { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Display(Name = "Equipo Visita")]
        public string visitante { get; set; }

        [Display(Name = "Finalizado")]
        public byte jugado { get; set; }

        //Dado que jugado es 1 o 0 utilizo el atributo estado para mostrar al usuario 'finalizado' o 'pendiente' segun la variable jugado
        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Display(Name = "Goles Local")]
        public int marcador_local { get; set; }

        [Display(Name = "Goles Visita")]
        public int marcador_visitante { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Display(Name = "Probabilidad Local")]
        public float probabilidad_local { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Display(Name = "Probabilidad Empate")]
        public float probabilidad_empate { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Display(Name = "Probabilidad Visita")]
        public float probabilidad_visita { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Display(Name = "No Jornada")]
        public int jornada { get; set; }

        //convierte las probabilidades porcentuales en US Odds para mostrarlas al usuario
        public void USOdds() 
        {
            if (probabilidad_local > 50)
                probabilidad_local = (-1 * (probabilidad_local / (100 - probabilidad_local))) * 100;
            else
                probabilidad_local = ((100 - probabilidad_local) / probabilidad_local) * 100;

            if (probabilidad_empate > 50)
                probabilidad_empate = (-1 * (probabilidad_empate / (100 - probabilidad_empate))) * 100;
            else
                probabilidad_empate = ((100 - probabilidad_empate) / probabilidad_empate) * 100;

            if (probabilidad_visita > 50)
                probabilidad_visita = (-1 * (probabilidad_visita / (100 - probabilidad_visita))) * 100;
            else
                probabilidad_visita = ((100 - probabilidad_visita) / probabilidad_visita) * 100;
        }

        //convierte las probabilidades porcentuales en Decimal Odds para mostrarlas al usuario
        public void DecimalOdds() 
        {
            probabilidad_local = 100 / probabilidad_local;
            probabilidad_empate = 100 / probabilidad_empate;
            probabilidad_visita = 100 / probabilidad_visita;
        }

        public void actualizarJugado() 
        {
            if (estado.Equals("Finalizado"))
                jugado = 1;
            else
                jugado = 0;
        }

        public void actualizarEstado() 
        {
            if (jugado == 1)
                estado = "Finalizado";
            else
                estado = "Pendiente";
        }

    }//end class
}//end namespace