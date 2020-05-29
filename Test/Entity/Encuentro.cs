using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Entity
{
    public class Encuentro
    {
        [Display(Name = "No. Juego")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Equipo Local")]
        public string local { get; set; }

        [Required]
        [Display(Name = "Equipo Visita")]
        public string visitante { get; set; }

        [Display(Name = "Finalizado")]
        public byte jugado { get; set; }

        [Display(Name = "Goles Local")]
        public int marcador_local { get; set; }

        [Display(Name = "Goles Visita")]
        public int marcador_visitante { get; set; }

        [Required]
        [Display(Name = "Probabilidad Local")]
        public float probabilidad_local { get; set; }

        [Required]
        [Display(Name = "Probabilidad Empate")]
        public float probabilidad_empate { get; set; }

        [Required]
        [Display(Name = "Probabilidad Visita")]
        public float probabilidad_visita { get; set; }

        [Required]
        [Display(Name = "No Jornada")]
        public int jornada { get; set; }

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

        public void DecimalOdds() 
        {
            probabilidad_local = 100 / probabilidad_local;
            probabilidad_empate = 100 / probabilidad_empate;
            probabilidad_visita = 100 / probabilidad_visita;
        }

    }//end class
}//end namespace