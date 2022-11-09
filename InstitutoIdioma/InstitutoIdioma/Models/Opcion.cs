using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutoIdioma.Models
{
    public class Opcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Texto { get; set; }

        public Pregunta Pregunta { get; set; }

        [Display(Name = "Es correcta")]
        public bool EsCorrecta { get; set; }
    }
}
