using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutoIdioma.Models
{
    public class Pregunta
    {
        public Pregunta()
        {
            Opciones = new List<Opcion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Enunciado { get; set; }

        public Examen Examen { get; set; }

        public List<Opcion> Opciones { get; set; }
    }
}
