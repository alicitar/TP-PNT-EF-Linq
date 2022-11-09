using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutoIdioma.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Examenes = new List<UsuarioExamen>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        public string Contrasenia { get; set; }

        public string Email { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        public string Dni { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public virtual List<UsuarioExamen> Examenes { get; set; }

        [EnumDataType(typeof(TipoPerfil))]
        [Display(Name = "Perfil")]
        public TipoPerfil  TipoPerfil { get; set; }

        [EnumDataType(typeof(NivelIdioma))]
        [Display(Name = "Nivel de idioma")]
        public NivelIdioma Nivel { get; set; }

    }
}
