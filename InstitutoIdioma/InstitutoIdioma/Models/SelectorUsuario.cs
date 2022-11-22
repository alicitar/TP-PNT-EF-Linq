using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InstitutoIdioma.Models
{
    public class SelectorUsuario
    {
        public int[] Seleccionados { get; set; }
        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public TipoPerfil Perfil { get; set; }

        [Display(Name = "Nivel de idioma")]
        public NivelIdioma NivelIdioma { get; set; }
    }
}
