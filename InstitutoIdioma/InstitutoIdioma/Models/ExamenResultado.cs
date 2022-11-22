using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoIdioma.Models
{
    public class ExamenResultado
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; }

        public int CantidadPreguntas { get; set; }

        public int RespuestasCorrectas { get; set; }

        public decimal PorcentajeAprobacion { 
            get 
            {
                return (RespuestasCorrectas * 100) / CantidadPreguntas;
            } 
        }

        public bool Aprobado { 
            get 
            {
                return PorcentajeAprobacion >= 60;
            } 
        }
    }
}
