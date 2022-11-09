using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoIdioma.Models
{
    public class UsuarioExamen
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ExamenId { get; set; }
        public Examen Examen { get; set; }
    }
}
