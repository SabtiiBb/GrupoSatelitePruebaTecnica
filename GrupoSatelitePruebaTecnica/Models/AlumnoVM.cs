using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GrupoSatelitePruebaTecnica.Models
{
    public class AlumnoVM
    {
        public int idAlumno { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        public string codAlumno { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        public int Edad { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        public int IdGrado { get; set; }
        public string NombreGrado { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        public string Genero { get; set; }
        public string Observacion { get; set; }
        public List<AlumnoVM> ListAlumnos { get; set; }
        public List<SelectListItem> GradosLst { get; set; }

    }
}