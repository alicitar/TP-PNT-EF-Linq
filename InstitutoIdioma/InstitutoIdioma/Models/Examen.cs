using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoIdioma.Models
{
    public class Examen
    {
        public Examen() 
        {
            Preguntas = new List<Pregunta>();
            Usuarios = new List<UsuarioExamen>();
        }

        // clave primaria de la tabla
        [Key]
        // Autoincremental en base de datos (que se genere solo)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Nombre { get; set; }

        public List<Pregunta> Preguntas { get; set; }

        public virtual List<UsuarioExamen> Usuarios { get; set; }

        [EnumDataType(typeof(NivelIdioma))]
        [Display(Name = "Nivel de idioma")]
        public NivelIdioma Nivel { get; set; }
    }
}
