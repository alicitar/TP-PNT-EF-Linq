using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoIdioma.Models
{
    public enum TipoPerfil
    {
        [Description("Sin Asignar")]
        SinAsignar,
        ALUMNO,
        PROFESOR,
        DIRECTOR,
        ADMINISTRADOR = 999
    }


}
